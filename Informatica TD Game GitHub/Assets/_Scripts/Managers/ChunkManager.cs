using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkManager : MonoBehaviour
{
    public static ChunkManager Instance;

    [SerializeField] private GameObject pathPrefab;
    [SerializeField] private GameObject emptyChunkPrefab;

    [SerializeField] private PathSO startPath;
    [SerializeField] private List<PathSO> pathChunks;

    public int totalPathCount = 1;
    private int waypointNumber;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnFirstChunk()
    {
        SpawnNewPathChunk(Vector2.zero, PathSO.Rotations.None, startPath);
        GameManager.Instance.SwitchGameState(GameManager.GameState.ChoseNextChunk);
    }

    public void SpawnNextChunk(Vector2 position, PathSO.Rotations rotation)
    {
        SpawnNewPathChunk(position, rotation, RandomPathTile());
        GameManager.Instance.SwitchGameState(GameManager.GameState.StartNewWave);
    }

    private void SpawnNewPathChunk(Vector2 positionOffset, PathSO.Rotations rotation, PathSO chunkToSpawn)
    {
        GameObject chunkContainer = new GameObject();

        Vector2 chunkCord = new Vector2(positionOffset.x / 7, positionOffset.y / 7);
        chunkContainer.name = chunkToSpawn.pathName + " (" + chunkCord.x + "," + chunkCord.y + ")";

        //Spawn Path Tiles
        List<Vector2> rotatedPathPositions = RotatedPositions(chunkToSpawn.basePathPositions, rotation);
        foreach (Vector2 pathPosition in rotatedPathPositions)
        {
            Vector2 position = pathPosition + positionOffset;
            GameObject spawnedPath = Instantiate(pathPrefab, position, Quaternion.identity);
            spawnedPath.name = chunkToSpawn.pathName + " (" + pathPosition.x + "," + pathPosition.y + ")";
            spawnedPath.transform.parent = chunkContainer.transform;
        }

        //Spawn Waypoints
        List<Vector2> rotatedWaypointPositions = RotatedPositions(chunkToSpawn.baseWaypointPositions, rotation);
        foreach (Vector2 waypointPosition in rotatedWaypointPositions)
        {
            Vector2 position = waypointPosition + positionOffset;
            WaypointManager.Instance.AddNewWaypoint(position, 0, waypointNumber);

            waypointNumber++;
        }

        //Spawn Extra Waypoints (for more routes)
        List<Vector2> rotatedExtraWaypointPositions = RotatedPositions(chunkToSpawn.extraWaypointPositions, rotation);
        foreach (Vector2 extraWaypointPosition in rotatedExtraWaypointPositions)
        {
            Vector2 positionExtraWaypoint = extraWaypointPosition + positionOffset;
            WaypointManager.Instance.AddNewWaypoint(positionExtraWaypoint, 1, waypointNumber - 1);

            totalPathCount = 2;
        }

        //Spawn Next Chunck Locations
        int emptyChunksSpawned = 0;
        List<Vector2> rotatedEmptyChunkPositions = RotatedPositions(chunkToSpawn.nextChunkPositions, rotation);
        foreach (Vector2 nextChunckPosition in rotatedEmptyChunkPositions)
        {
            Vector2 positionNextChunk = nextChunckPosition + positionOffset;
            GameObject nextChunck = Instantiate(emptyChunkPrefab, positionNextChunk, Quaternion.identity);

            EmptyChunk chunkScript = nextChunck.GetComponent<EmptyChunk>();
            chunkScript.rotation = CalculateNextChunkRotation(rotation, chunkToSpawn.nextChunckRotations[emptyChunksSpawned]);
            Debug.Log("NextChunkRotation: " + chunkScript.rotation);

            emptyChunksSpawned++;
        }
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
        Debug.Log("CurrentRotation: " + currentRotation);
        Debug.Log("NextRotaton: " + nextRotation);
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

    private PathSO RandomPathTile()
    {
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
