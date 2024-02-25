using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager Instance;

    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private GameObject emptyChunkPrefab;

    [SerializeField] private PathSO startChunk;
    [SerializeField] private PathSO endChunk;
    [SerializeField] private List<PathSO> pathChunks;

    private Dictionary<Vector2, PathSO> spawnedChunks = new Dictionary<Vector2, PathSO>();

    [SerializeField]  private PositionsToCheckSO positionsToCheckSO;

    private int baseChunkXMin = -3;
    private int baseChunkXMax = 3;

    public int totalPathCount = 1;
    private int waypointNumber;

    private int totalChunksSpawned = 0;

    public enum Rotations
    {
        None, Right, Backwards, Left
    }

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnFirstChunk()
    {
        GameObject firstEmptyChunk = new GameObject();

        EmptyChunk startChunkData = firstEmptyChunk.AddComponent<EmptyChunk>();
        startChunkData.rotation = (Rotations)Random.Range(0, 4);
        startChunkData.waypoint = null;

        SpawnNewChunk(startChunkData);
        
    }

    public void SpawnNewChunk(EmptyChunk chunkData)
    {
        Vector2 positionOffset = new Vector2(chunkData.transform.position.x, chunkData.transform.position.y);
        PathSO chunkToSpawn = RandomChunk(positionOffset, chunkData.rotation);

        if (totalChunksSpawned == 0) chunkToSpawn = startChunk;
        if (chunkToSpawn == null) chunkToSpawn = endChunk;

        GameObject chunkContainer = new GameObject();

        Vector2 chunkCord = new Vector2(positionOffset.x / 7, positionOffset.y / 7);
        chunkContainer.name = chunkToSpawn.pathName + " (" + chunkCord.x + "," + chunkCord.y + ")";

        spawnedChunks[chunkCord] = chunkToSpawn;

        //Spawn Path Tiles
        List<Vector2> rotatedPathPositions = RotatedPositions(chunkToSpawn.basePathPositions, chunkData.rotation);
        foreach (Vector2 pathPosition in rotatedPathPositions)
        {
            Vector2 position = pathPosition + positionOffset;
            GameObject spawnedPath = Instantiate(pathPrefab, position, Quaternion.identity, chunkContainer.transform);
            spawnedPath.name = chunkToSpawn.pathName + " (" + pathPosition.x + "," + pathPosition.y + ")";
        }

        //Spawn Ground Prefabs
        for (int x = baseChunkXMin; x <= baseChunkXMax; x++)
        {
            for (int y = baseChunkXMin; y <= baseChunkXMax; y++)
            {
                Vector2 groundPosition = new Vector2(x, y);
                if (!rotatedPathPositions.Contains(groundPosition))
                {
                    Vector2 groundSpawnPosition = groundPosition + positionOffset;
                    GameObject spawnedGround = Instantiate(groundPrefab, groundSpawnPosition, Quaternion.identity, chunkContainer.transform);
                    spawnedGround.name = "Ground (" + x + "," + y + ")";
                }
            }
        }

        //Spawn Waypoints
        Waypoint latestWaypoint = null;

        waypointNumber = chunkData.pathNumber;
        List<Vector2> rotatedWaypointPositions = RotatedPositions(chunkToSpawn.baseWaypointPositions, chunkData.rotation);
        foreach (Vector2 waypointPosition in rotatedWaypointPositions)
        {
            Vector2 position = waypointPosition + positionOffset;
            latestWaypoint = WaypointManager.Instance.AddNewWaypoint(position, chunkData.pathIndex, waypointNumber, chunkData.waypoint);

            waypointNumber++;
        }

        //Spawn Next Chunck Positions + Update Enemy Spawn Positions
        int emptyChunksSpawned = 0;
        List<Vector2> rotatedEmptyChunkPositions = RotatedPositions(chunkToSpawn.nextChunkPositions, chunkData.rotation);
        List<Vector2> rotatedEnemySpawnPositions = RotatedPositions(chunkToSpawn.baseEnemySpawnPositions, chunkData.rotation);
        foreach (Vector2 nextChunckPosition in rotatedEmptyChunkPositions)
        {
            Vector2 position = nextChunckPosition + positionOffset;
            Vector2 enemySpawnPosition = rotatedEnemySpawnPositions[emptyChunksSpawned] + positionOffset;

            Vector2 newChunkCord = new Vector2(position.x / 7, position.y / 7);
            spawnedChunks[newChunkCord] = null;

            GameObject nextChunck = Instantiate(emptyChunkPrefab, position, Quaternion.identity);

            EmptyChunk chunkScript = nextChunck.GetComponent<EmptyChunk>();

            chunkScript.pathNumber = waypointNumber;
            chunkScript.rotation = CalculateNextChunkRotation(chunkData.rotation, chunkToSpawn.nextChunckRotations[emptyChunksSpawned]);

            if (latestWaypoint == null)
            {
                chunkScript.waypoint = chunkData.waypoint;
            }
            else
            {
                chunkScript.waypoint = latestWaypoint;
            }

            if (emptyChunksSpawned == 0)
            {
                chunkScript.pathIndex = chunkData.pathIndex;
                EnemyManager.Instance.UpdateSpawnLocations(chunkScript.waypoint, enemySpawnPosition, chunkData.pathIndex);
            }
                
            else
            {
                chunkScript.pathIndex = totalPathCount;
                EnemyManager.Instance.UpdateSpawnLocations(chunkScript.waypoint, enemySpawnPosition, totalPathCount);

                totalPathCount++;
            }
            
            

            emptyChunksSpawned++;
        }

        totalChunksSpawned++;
        GameManager.Instance.SwitchGameState((GameManager.GameState)((int)GameManager.Instance.gameState + 1));
    }

    private List<Vector2> RotatedPositions(List<Vector2> positions, Rotations rotation)
    {
        List<Vector2> rotatedPositions = new List<Vector2>();

        switch (rotation)
        {
            case Rotations.None:

                return positions;

            case Rotations.Left:

                foreach (Vector2 position in positions)
                {
                    Vector2 newposition = new Vector2(-position.y, position.x);
                    rotatedPositions.Add(newposition);
                }

                return rotatedPositions;

            case Rotations.Right:

                foreach (Vector2 position in positions)
                {
                    Vector2 newposition = new Vector2(position.y, -position.x);
                    rotatedPositions.Add(newposition);
                }

                return rotatedPositions;

            case Rotations.Backwards:

                foreach (Vector2 position in positions)
                {
                    Vector2 newposition = new Vector2(-position.x, -position.y);
                    rotatedPositions.Add(newposition);
                }

                return rotatedPositions;
            default:
                Debug.Log("Not a valid rotation");
                return null;
        }

    }

    private Rotations CalculateNextChunkRotation(Rotations currentRotation, Rotations nextRotation)
    {
        switch (nextRotation)
        {
            //4 rotations so 0 <= PathSO.Rotations <= 3
            case Rotations.None:
                return currentRotation;
            case Rotations.Left:
                if ((int)currentRotation - 1 >= 0) return currentRotation - 1;
                else return currentRotation + 3;
            case Rotations.Right:
                if ((int)currentRotation + 1 <= 3) return currentRotation + 1;
                else return currentRotation - 3;
            default:
                Debug.Log("Error in Rotation");
                return currentRotation;

        }
    }

    private PathSO RandomChunk(Vector2 position, Rotations rotation)
    {
        List<PathSO> spawnableChunks = SpawnableChunks(position, rotation);

        var totalWeight = 0;
        foreach (PathSO chunk in spawnableChunks)
        {
            totalWeight += chunk.weight;
        }
        var randomWeightValue = Random.Range(1, totalWeight + 1);

        var processedWeight = 0;
        foreach (PathSO chunk in spawnableChunks)
        {
            processedWeight += chunk.weight;

            if (randomWeightValue <= processedWeight)
            {
                return chunk;
            }
        }
        return null;
    }

    private List<PathSO> SpawnableChunks(Vector2 currentPosition, Rotations currentRotation)
    {
        List<PathSO> spawnableChunks = new List<PathSO>();

        foreach (PathSO chunk in pathChunks)
        {
            spawnableChunks.Add(chunk);
        }

        int index = 0;

        List<Vector2> rotatedPositions = RotatedPositions(positionsToCheckSO.positionsToCheck, currentRotation);
        foreach (Vector2 rotatedPosition in rotatedPositions)
        {
            Vector2 positionToCheck = rotatedPosition + currentPosition / 7;

            if (spawnedChunks.ContainsKey(positionToCheck))
            {
                switch (positionsToCheckSO.correspondingRotations[index])
                {
                    case Rotations.Left:
                        spawnableChunks = RemoveChunks(spawnableChunks, Rotations.Left);
                        break;
                    case Rotations.Right:
                        spawnableChunks = RemoveChunks(spawnableChunks, Rotations.Right);
                        break;
                    case Rotations.None:
                        spawnableChunks = RemoveChunks(spawnableChunks, Rotations.None);
                        break;
                    default:
                        Debug.Log("Corresponding Rotation doesn't exist");
                        break;
                }
            }

            index++;
        }
        return spawnableChunks;
    }

    private List<PathSO> RemoveChunks(List<PathSO> spawnableChunks, Rotations rotationToCheck)
    {
        List<PathSO> unspawnableChunks = new List<PathSO>();
        foreach (PathSO chunk in spawnableChunks)
        {
            foreach (Rotations rotation in chunk.nextChunckRotations)
            {
                if (rotation == rotationToCheck)
                {
                    unspawnableChunks.Add(chunk);
                }
            }
        }

        foreach (PathSO unspawnableChunk in unspawnableChunks)
        {
            spawnableChunks.Remove(unspawnableChunk);
        }

        return spawnableChunks;
    }
}
