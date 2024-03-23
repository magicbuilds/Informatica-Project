using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

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

    [Header("PathSprites")]
    [SerializeField] private Sprite straightPathSprite;
    [SerializeField] private Sprite RTurnPathSprite;
    [SerializeField] private Sprite LTurnPathSprite;
    [SerializeField] private Sprite TJunctionLRPathSprite;
    [SerializeField] private Sprite TJunctionRPathSprite;
    [SerializeField] private Sprite TJunctionLPathSprite;
    [SerializeField] private Sprite crossroadPathSprite;

    public enum Directions
    {
        Up, Left, Down, Right
    }

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnFirstChunk()
    {
        GameObject firstEmptyChunk = new GameObject();

        EmptyChunk startChunkData = firstEmptyChunk.AddComponent<EmptyChunk>();
        startChunkData.rotation = (Directions)Random.Range(0, 4);
        startChunkData.waypoint = null;

        startChunkData.rotation = Directions.Up;

        SpawnNewChunk(startChunkData);

        Destroy(startChunkData.gameObject);
        
    }

    public void SpawnNewChunk(EmptyChunk chunkData)
    {
        Vector2 positionOffset = new Vector2(chunkData.transform.position.x, chunkData.transform.position.y);
        PathSO chunkToSpawn = RandomChunk(positionOffset, chunkData.rotation);

        if (totalChunksSpawned == 0) chunkToSpawn = startChunk;
        if (chunkToSpawn == null) chunkToSpawn = endChunk;

        GameObject chunkContainer = new GameObject();

        Vector2 chunkCord = new Vector2(positionOffset.x / 7, positionOffset.y / 7);
        chunkContainer.name = chunkToSpawn.pathType.ToString() + " (" + chunkCord.x + "," + chunkCord.y + ")";

        spawnedChunks[chunkCord] = chunkToSpawn;

        //Spawn Path Tiles
        List<Vector2> rotatedPathPositions = RotatedPositions(chunkToSpawn.basePathPositions, chunkData.rotation);
        List<GameObject> spawnedPaths = new List<GameObject>();
        foreach (Vector2 pathPosition in rotatedPathPositions)
        {
            Vector2 position = pathPosition + positionOffset;
            
            GameObject spawnedPath = Instantiate(pathPrefab, position, Quaternion.identity, chunkContainer.transform);
            spawnedPath.name = chunkToSpawn.pathType.ToString() + " (" + pathPosition.x + "," + pathPosition.y + ")";

            spawnedPaths.Add(spawnedPath);
        }

        //Spawn Ground Tiles
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
        Waypoint lastWaypointSpawned = null;

        waypointNumber = chunkData.pathNumber;
        List<Vector2> rotatedWaypointPositions = RotatedPositions(chunkToSpawn.baseWaypointPositions, chunkData.rotation);
        foreach (Vector2 waypointPosition in rotatedWaypointPositions)
        {
            Vector2 position = waypointPosition + positionOffset;

            Waypoint spawnedWaypoint = WaypointManager.Instance.AddNewWaypoint(position, chunkData.pathIndex, waypointNumber, chunkData.waypoint);
            if (waypointNumber == chunkData.pathNumber)
            {
                spawnedWaypoint.nextWaypoint = chunkData.waypoint;
            }
            else
            {
                spawnedWaypoint.nextWaypoint = lastWaypointSpawned;
            }
            
            lastWaypointSpawned = spawnedWaypoint;
            waypointNumber++;
        }

        UpdatePathSprites(spawnedPaths, rotatedPathPositions, rotatedWaypointPositions, chunkToSpawn);

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
            chunkScript.rotation = CalculateNextRotation(chunkData.rotation, chunkToSpawn.nextChunckRotations[emptyChunksSpawned]);

            if (lastWaypointSpawned == null)
            {
                chunkScript.waypoint = chunkData.waypoint;
            }
            else
            {
                chunkScript.waypoint = lastWaypointSpawned;
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

    private List<Vector2> RotatedPositions(List<Vector2> positions, Directions rotation)
    {
        List<Vector2> rotatedPositions = new List<Vector2>();

        switch (rotation)
        {
            case Directions.Up:

                return positions;

            case Directions.Right:

                foreach (Vector2 position in positions)
                {
                    Vector2 newposition = new Vector2(-position.y, position.x);
                    rotatedPositions.Add(newposition);
                }

                return rotatedPositions;

            case Directions.Left:

                foreach (Vector2 position in positions)
                {
                    Vector2 newposition = new Vector2(position.y, -position.x);
                    rotatedPositions.Add(newposition);
                }

                return rotatedPositions;

            case Directions.Down:

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

    private Directions CalculateNextRotation(Directions currentRotation, Directions nextRotation)
    {
        switch (nextRotation)
        {
            //4 rotations so 0 <= PathSO.Rotations <= 3
            case Directions.Up:
                return currentRotation;
            case Directions.Right:
                if ((int)currentRotation - 1 >= 0) return currentRotation - 1;
                else return currentRotation + 3;
            case Directions.Left:
                if ((int)currentRotation + 1 <= 3) return currentRotation + 1;
                else return currentRotation - 3;
            default:
                Debug.Log("Error in Rotation");
                return currentRotation;

        }
    }

    private void UpdatePathSprites(List<GameObject> paths, List<Vector2> positions, List<Vector2> waypointPositions, PathSO chunkToSpawn)
    {
        Vector2 targetPosition = Vector2.zero;
        Vector2 endPosition = positions[positions.Count - 1];

        GameObject previousRotatedPath = null;
        GameObject unrotatedPath = null;

        int index = 0; int waypointIndex = 0;
        foreach (Vector2 position in positions)
        {
            Sprite sprite = straightPathSprite;

            if (waypointPositions.Count != 0 && waypointIndex != -1)
            {
                if (position == waypointPositions[waypointIndex])
                {
                    if (waypointIndex <= waypointPositions.Count - 1 && waypointIndex >= 0)
                    {
                        waypointIndex = -1;
                    }
                    else
                    {
                        waypointIndex++;
                        targetPosition = waypointPositions[waypointIndex];
                    }

                    switch (chunkToSpawn.pathType)
                    {
                        case PathSO.Paths.RTurn:
                            sprite = RTurnPathSprite;
                            break;
                        case PathSO.Paths.LTurn:
                            sprite = LTurnPathSprite;
                            break;
                        case PathSO.Paths.TJunktionLR:
                            sprite = TJunctionLRPathSprite;
                            break;
                        case PathSO.Paths.TJunktionR:
                            sprite = TJunctionRPathSprite;
                            break;
                        case PathSO.Paths.TJunktionL:
                            sprite = TJunctionLPathSprite;
                            break;
                        case PathSO.Paths.Crossroad:
                            sprite = crossroadPathSprite;
                            break;
                        case PathSO.Paths.Straight:
                            break;
                        default:
                            Debug.Log("Sprite Not Defined");
                            break;
                    }
                }
            }
            else
            {
                targetPosition = endPosition;
            }

            paths[index].GetComponent<SpriteRenderer>().sprite = sprite;

            if (targetPosition == position && unrotatedPath == null)
            {
                if (index == paths.Count - 1)
                {
                    paths[index].transform.rotation = previousRotatedPath.transform.rotation;
                }
                else
                {
                    unrotatedPath = paths[index];
                }
            }
            if (targetPosition.x - position.x > 0)
            {
                paths[index].transform.Rotate(0, 0, -90);
            }
            if (targetPosition.x - position.x < 0)
            {
                paths[index].transform.Rotate(0, 0, 90);
            }
            if (targetPosition.y - position.y < 0)
            {
                paths[index].transform.Rotate(0, 0, 180);
            }

            if (targetPosition != position && unrotatedPath != null)
            {
                Debug.Log("Called");
                unrotatedPath.transform.rotation = paths[index].transform.rotation;
                unrotatedPath = null;
            }

            Debug.Log("Position : " + position.ToString() +
                "\n Target Position : " + targetPosition.ToString());

            previousRotatedPath = paths[index];

            index++;
        }


        /*List<Vector2> positionsToCheck = positions;
        List<Directions> pathRotations = chunkToSpawn.basePathRotations;

        paths[0].GetComponent<SpriteRenderer>().sprite = straightPathSprite;
        switch (pathRotations[0])
        {
            case Directions.Down:
                paths[0].transform.Rotate(0, 0, 180);
                break;
            case Directions.Right:
                paths[0].transform.Rotate(0, 0, 90);
                break;
            case Directions.Left:
                paths[0].transform.Rotate(0, 0, -90);
                break;
        }


        paths[paths.Count - 1].GetComponent<SpriteRenderer>().sprite = straightPathSprite;
        switch (pathRotations[paths.Count - 1])
        {
            case Directions.Down:
                paths[paths.Count - 1].transform.Rotate(0, 0, 180);
                break;
            case Directions.Right:
                paths[paths.Count - 1].transform.Rotate(0, 0, 90);
                break;
            case Directions.Left:
                paths[paths.Count - 1].transform.Rotate(0, 0, -90);
                break;
        }

        int index = 0;
        foreach (Vector2 position in positionsToCheck)
        {
            if (index >= paths.Count) break;

            bool tempUp = false; bool up = false;
            bool tempRight = false; bool right = false;
            bool tempDown = false; bool down = false;
            bool tempLeft = false; bool left = false;

            int xPos = (int)position.x; int yPos = (int)position.y;

            if(positionsToCheck.Contains(new Vector2(xPos, yPos + 1)))
            {
                tempUp = true;
            }
            if(positionsToCheck.Contains(new Vector2(xPos + 1, yPos)))
            {
                tempRight = true;
            }
            if (positionsToCheck.Contains(new Vector2(xPos, yPos - 1)))
            {
                tempDown = true;
            }
            if (positionsToCheck.Contains(new Vector2(xPos - 1, yPos)))
            {
                tempLeft = true;
            }

            switch (CalculateNextRotation(rotation, pathRotations[index]))
            {
                case Directions.Up:
                    paths[index].transform.Rotate(0, 0, 180);

                    up = tempUp;
                    right = tempRight;
                    down = tempDown;
                    left = tempLeft;
                    break;
                case Directions.Right:
                    paths[index].transform.Rotate(0, 0, 90);

                    up = tempLeft;
                    right = tempUp;
                    down = tempRight;
                    left = tempDown;
                    break;
                case Directions.Down:
                    up = tempDown;
                    right = tempLeft;
                    down = tempUp;
                    left = tempRight;
                    break;
                 case Directions.Left:
                    paths[index].transform.Rotate(0, 0, -90);

                    up = tempRight;
                    right = tempDown;
                    down = tempLeft;
                    left = tempUp;
                    break;
            }

            if (up && !right && down && !left)
            {
                paths[index].GetComponent<SpriteRenderer>().sprite = straightPathSprite;
            }
            if (up && right && !down && !left)
            {
                paths[index].GetComponent<SpriteRenderer>().sprite = RTurnPathSprite;
            }
            if (up && !right && !down && left)
            {
                paths[index].GetComponent<SpriteRenderer>().sprite = LTurnPathSprite;
            }
            if (up && right && !down && left)
            {
                paths[index].GetComponent<SpriteRenderer>().sprite = TJunctionLRPathSprite;
            }
            if (up && right && down && !left)
            {
                paths[index].GetComponent<SpriteRenderer>().sprite = TJunctionRPathSprite;
            }
            if (up && !right && down && left)
            {
                paths[index].GetComponent<SpriteRenderer>().sprite = TJunctionLPathSprite;
            }
            if (up && right && down && left)
            {
                paths[index].GetComponent<SpriteRenderer>().sprite = crossroadpathSprite;
            }
            Debug.Log("GameObject: " + paths[index].name + "\nup: " + tempUp + "\nright: " + tempRight + "\ndown: " + tempDown + "\nleft: " + tempLeft + "\nrotation: " + rotation);

            index++;
        }*/
    }

    private PathSO RandomChunk(Vector2 position, Directions rotation)
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

    private List<PathSO> SpawnableChunks(Vector2 currentPosition, Directions currentRotation)
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
                    case Directions.Right:
                        spawnableChunks = RemoveChunks(spawnableChunks, Directions.Right);
                        break;
                    case Directions.Left:
                        spawnableChunks = RemoveChunks(spawnableChunks, Directions.Left);
                        break;
                    case Directions.Up:
                        spawnableChunks = RemoveChunks(spawnableChunks, Directions.Up);
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

    private List<PathSO> RemoveChunks(List<PathSO> spawnableChunks, Directions rotationToCheck)
    {
        List<PathSO> unspawnableChunks = new List<PathSO>();
        foreach (PathSO chunk in spawnableChunks)
        {
            foreach (Directions rotation in chunk.nextChunckRotations)
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
