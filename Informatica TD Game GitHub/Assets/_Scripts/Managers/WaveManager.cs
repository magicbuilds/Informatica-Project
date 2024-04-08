using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private WavesSO wavesToSpawn;

    public int waveIndex = 0;
    public int enemiesInThisWave = 0;

    private List<Vector2> spawnPositions = new List<Vector2>();
    private List<int> usedWaypointIndexes = new List<int>();
    private List<Waypoint> correspondingWaypoints = new List<Waypoint>();

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnNewWave()
    {
        WaveSO waveToSpawn = wavesToSpawn.waves[waveIndex];

        enemiesInThisWave = 0;
        int index = 0;
        foreach (int amount in waveToSpawn.correspondingEnemiesAmount)
        {
            EnemySO enemyToSpawn = waveToSpawn.enemies[index];

            for (int i = 0; i < amount; i++)
            {
                enemiesInThisWave++;

                int randomPathIndex = Random.Range(0, spawnPositions.Count);

                Vector2 randomPosition = spawnPositions[randomPathIndex];
                Waypoint waypoint = correspondingWaypoints[randomPathIndex];

                EnemyManager.Instance.SpawnEnemy(enemyToSpawn, randomPosition, waypoint);
            }

            index++;
        }

        waveIndex++;
    }

    public void UpdateSpawnLocations(Waypoint waypoint, Vector2 newPosition, int waypointIndex)
    {
        if (usedWaypointIndexes.Contains(waypointIndex))
        {
            spawnPositions.RemoveAt(waypointIndex);
            correspondingWaypoints.RemoveAt(waypointIndex);
        }
        else usedWaypointIndexes.Add(waypointIndex);

        spawnPositions.Insert(waypointIndex, newPosition);
        correspondingWaypoints.Insert(waypointIndex, waypoint);
    }
}
