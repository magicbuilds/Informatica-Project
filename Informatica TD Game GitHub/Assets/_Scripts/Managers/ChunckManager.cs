using System.Collections.Generic;
using UnityEngine;

public class ChunckManager : MonoBehaviour
{
    public static ChunckManager Instance;

    [SerializeField] private GameObject pathPrefab;

    [SerializeField] private PathSO startPath;
    [SerializeField] private List<PathSO> pathChunks;

    public int totalPathCount = 1;
    private int waypointNumber;

    private void Awake()
    {
        Instance = this;
    }

    public void TempPathSpawner()
    {
        SpawnNewPathChunk(Vector2.zero, startPath);
        SpawnNewPathChunk(new Vector2(-7, 0), RandomPathTile());
        SpawnNewPathChunk(new Vector2(-7, -7), RandomPathTile());
        SpawnNewPathChunk(new Vector2(7, 0), RandomPathTile());
        SpawnNewPathChunk(new Vector2(7, 7), RandomPathTile());
        SpawnNewPathChunk(new Vector2(0, 7), pathChunks[3]);
        GameManager.Instance.SwitchGameState(GameManager.GameState.StartNewWave);
    }

    public void SpawnNewPathChunk(Vector2 positionOffset, PathSO pathToSpawn)
    {
        GameObject chunkContainer = new GameObject();

        Vector2 chunkCord = new Vector2(positionOffset.x / 7, positionOffset.y / 7);
        chunkContainer.name = pathToSpawn.pathName + " (" + chunkCord.x + "," + chunkCord.y + ")";

        foreach (Vector2 pathPosition in pathToSpawn.basePathPositions)
        {
            Vector2 position = pathPosition + positionOffset;
            GameObject spawnedPath = Instantiate(pathPrefab, position, Quaternion.identity);
            spawnedPath.name = pathToSpawn.pathName + " (" + pathPosition.x + "," + pathPosition.y + ")";
            spawnedPath.transform.parent = chunkContainer.transform;
        }

        int waypointsLeft = pathToSpawn.baseWaypointPositions.Count;
        foreach (Vector2 waypointPosition in pathToSpawn.baseWaypointPositions)
        {
            Vector2 position = waypointPosition + positionOffset;
            WaypointManager.Instance.AddNewWaypoint(position, 0, waypointNumber);

            if (waypointsLeft == 1)
            {
                foreach (Vector2 extraWaypointPosition in pathToSpawn.extraWaypointPositions)
                {
                    Vector2 positionExtraWaypoint = extraWaypointPosition + positionOffset;
                    WaypointManager.Instance.AddNewWaypoint(positionExtraWaypoint, 1, waypointNumber);

                    totalPathCount = 2;
                }
            }
            waypointNumber++;
            waypointsLeft--;
        }
    }

    private PathSO RandomPathTile()
    {
        return pathChunks[Random.Range(0, pathChunks.Count - 1)];



        /*int totalWeight = 0;
        foreach (PathSO path in pathChunks)
        {
            totalWeight += path.weight;
        }

        float randomNumber = Random.Range(0, totalWeight);
        for (int i = 0; i < pathChunks.Count; i++)
        {
            totalWeight -= pathChunks[i].weight;
            if (totalWeight <= randomNumber)
            {
                return pathChunks[i];
            }
        }
        Debug.Log("No Path Found !!!!!");
        return null;
        */
    }
}
