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
        SpawnNewPathChunk(Vector2.zero, startPath);
        GameManager.Instance.SwitchGameState(GameManager.GameState.ChoseNextChunk);
    }

    public void SpawnNextChunk(Vector2 position)
    {
        SpawnNewPathChunk(position, RandomPathTile());
        GameManager.Instance.SwitchGameState(GameManager.GameState.StartNewWave);
    }

    private void SpawnNewPathChunk(Vector2 positionOffset, PathSO chunkToSpawn)
    {
        GameObject chunkContainer = new GameObject();

        Vector2 chunkCord = new Vector2(positionOffset.x / 7, positionOffset.y / 7);
        chunkContainer.name = chunkToSpawn.pathName + " (" + chunkCord.x + "," + chunkCord.y + ")";

        //Spawn Path Tiles
        foreach (Vector2 pathPosition in chunkToSpawn.basePathPositions)
        {
            Vector2 position = pathPosition + positionOffset;
            GameObject spawnedPath = Instantiate(pathPrefab, position, Quaternion.identity);
            spawnedPath.name = chunkToSpawn.pathName + " (" + pathPosition.x + "," + pathPosition.y + ")";
            spawnedPath.transform.parent = chunkContainer.transform;
        }

        //Spawn Waypoints
        foreach (Vector2 waypointPosition in chunkToSpawn.baseWaypointPositions)
        {
            Vector2 position = waypointPosition + positionOffset;
            WaypointManager.Instance.AddNewWaypoint(position, 0, waypointNumber);

            waypointNumber++;
        }

        //Spawn Extra Waypoints (for more routes)
        foreach (Vector2 extraWaypointPosition in chunkToSpawn.extraWaypointPositions)
        {
            Vector2 positionExtraWaypoint = extraWaypointPosition + positionOffset;
            WaypointManager.Instance.AddNewWaypoint(positionExtraWaypoint, 1, waypointNumber - 1);

            totalPathCount = 2;
        }

        //Spawn Next Chunck Locations
        foreach (Vector2 nextChunckPosition in chunkToSpawn.nextChunkPositions)
        {
            Vector2 positionNextChunk = nextChunckPosition + positionOffset;
            GameObject nextChunck = Instantiate(emptyChunkPrefab, positionNextChunk, Quaternion.identity);
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
