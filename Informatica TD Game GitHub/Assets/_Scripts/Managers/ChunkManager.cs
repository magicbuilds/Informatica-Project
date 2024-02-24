using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager Instance;

    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private GameObject emptyChunkPrefab;

    [SerializeField] private PathSO startPath;
    [SerializeField] private List<PathSO> pathChunks;

    private Dictionary<Vector2, PathSO> spawnedChunks;

    private int baseChunkXMin = -3;
    private int baseChunkXMax = 3;

    public int totalPathCount = 1;
    private int waypointNumber;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        spawnedChunks = new Dictionary<Vector2, PathSO>();
    }

    public void SpawnFirstChunk()
    {
        PathSO.Rotations randomRotation = (PathSO.Rotations)Random.Range(0, 4);
        SpawnNewChunk(Vector2.zero, randomRotation, 0, 0, startPath);
        
    }

    public void SpawnNextChunk(Vector2 position, PathSO.Rotations rotation, int pathIndex, int pathNumber)
    {
        SpawnNewChunk(position, rotation, pathIndex, pathNumber, RandomChunk(position, rotation));
    }

    private void SpawnNewChunk(Vector2 positionOffset, PathSO.Rotations rotation, int pathIndex, int pathNumber, PathSO chunkToSpawn)
    {
        

        GameObject chunkContainer = new GameObject();

        Vector2 chunkCord = new Vector2(positionOffset.x / 7, positionOffset.y / 7);
        chunkContainer.name = chunkToSpawn.pathName + " (" + chunkCord.x + "," + chunkCord.y + ")";

        spawnedChunks[chunkCord] = chunkToSpawn;

        //Spawn Path Tiles
        List<Vector2> rotatedPathPositions = RotatedPositions(chunkToSpawn.basePathPositions, rotation);
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
        waypointNumber = pathNumber;
        List<Vector2> rotatedWaypointPositions = RotatedPositions(chunkToSpawn.baseWaypointPositions, rotation);
        foreach (Vector2 waypointPosition in rotatedWaypointPositions)
        {
            Vector2 position = waypointPosition + positionOffset;
            WaypointManager.Instance.AddNewWaypoint(position, pathIndex, waypointNumber);

            waypointNumber++;
        }

        //Spawn Next Chunck Locations
        int emptyChunksSpawned = 0;
        List<Vector2> rotatedEmptyChunkPositions = RotatedPositions(chunkToSpawn.nextChunkPositions, rotation);
        List<Vector2> rotatedEnemySpawnPositions = RotatedPositions(chunkToSpawn.baseEnemySpawnPositions, rotation);
        foreach (Vector2 nextChunckPosition in rotatedEmptyChunkPositions)
        {
            Vector2 position = nextChunckPosition + positionOffset;
            Vector2 enemySpawnPosition = rotatedEnemySpawnPositions[emptyChunksSpawned] + positionOffset;

            GameObject nextChunck = Instantiate(emptyChunkPrefab, position, Quaternion.identity);

            EmptyChunk chunkScript = nextChunck.GetComponent<EmptyChunk>();

            chunkScript.pathNumber = waypointNumber;
            chunkScript.rotation = CalculateNextChunkRotation(rotation, chunkToSpawn.nextChunckRotations[emptyChunksSpawned]);

            if (emptyChunksSpawned == 0)
            {
                chunkScript.pathIndex = pathIndex;
                EnemyManager.Instance.UpdateSpawnLocations(pathIndex, enemySpawnPosition);
            }
                
            else
            {
                chunkScript.pathIndex = totalPathCount;
                EnemyManager.Instance.UpdateSpawnLocations(totalPathCount, enemySpawnPosition);
                totalPathCount++;
            }
            emptyChunksSpawned++;
        }

        GameManager.Instance.SwitchGameState((GameManager.GameState)((int)GameManager.Instance.gameState + 1));
    }

    private List<Vector2> RotatedPositions(List<Vector2> positions, PathSO.Rotations rotation)
    {
        List<Vector2> rotatedPositions = new List<Vector2>();

        switch (rotation)
        {
            case PathSO.Rotations.None:

                return positions;

            case PathSO.Rotations.Left:

                foreach (Vector2 position in positions)
                {
                    Vector2 newposition = new Vector2(-position.y, position.x);
                    rotatedPositions.Add(newposition);
                }

                return rotatedPositions;

            case PathSO.Rotations.Right:

                foreach (Vector2 position in positions)
                {
                    Vector2 newposition = new Vector2(position.y, -position.x);
                    rotatedPositions.Add(newposition);
                }

                return rotatedPositions;

            case PathSO.Rotations.Backwards:

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

    private PathSO.Rotations CalculateNextChunkRotation(PathSO.Rotations currentRotation, PathSO.Rotations nextRotation)
    {
        switch (nextRotation)
        {
            //4 rotations so 0 <= PathSO.Rotations <= 3
            case PathSO.Rotations.None:
                return currentRotation;
            case PathSO.Rotations.Left:
                if ((int)currentRotation - 1 >= 0) return currentRotation - 1;
                else return currentRotation + 3;
            case PathSO.Rotations.Right:
                if ((int)currentRotation + 1 <= 3) return currentRotation + 1;
                else return currentRotation - 3;
            default:
                Debug.Log("Error in Rotation");
                return currentRotation;

        }
    }

    private PathSO RandomChunk(Vector2 position, PathSO.Rotations rotation)
    {
        //List<PathSO> spawnableChunks = SpawnableChunks(position, rotation);

        var totalWeight = 0;
        foreach (PathSO path in pathChunks)
        {
            totalWeight += path.weight;
        }
        var randomWeightValue = Random.Range(1, totalWeight + 1);

        var processedWeight = 0;
        foreach (PathSO path in pathChunks)
        {
            processedWeight += path.weight;

            if (randomWeightValue <= processedWeight)
            {
                return path;
            }
        }
        return null;
    }
}
