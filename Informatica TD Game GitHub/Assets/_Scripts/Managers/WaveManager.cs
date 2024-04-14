using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    public static WaveManager Instance;

    [SerializeField] private WavesSO wavesToSpawn;

    public int waveIndex = 0;
    public int enemiesInThisWave = 0;
    public int enemiesLeftInThisWave = 0;

    private List<Vector2> spawnPositions = new List<Vector2>();
    private List<int> usedWaypointIndexes = new List<int>();
    private List<Waypoint> correspondingWaypoints = new List<Waypoint>();

    private bool waveHasSpawned = true;
    private bool hasWaitedBetweenEnemies = true;

    private WaveSO waveToSpawn = null;
    private int currentIndexAtWave = 0;
    private int currentEnemiesSpawnedAtIndex = 0;

    private float timeWaited;
    private float baseWaitTime = 0.15f;

    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if (waveHasSpawned) return;

        timeWaited += Time.deltaTime;

        if (!hasWaitedBetweenEnemies)
        {
            if (timeWaited < waveToSpawn.correspondingWaitTime[currentIndexAtWave]) return;
            else hasWaitedBetweenEnemies = true;
        }

        if (timeWaited > baseWaitTime)
        {
            if (currentEnemiesSpawnedAtIndex < waveToSpawn.correspondingEnemiesAmount[currentIndexAtWave])
            {
                EnemySO enemyToSpawn = waveToSpawn.enemies[currentIndexAtWave];

                int randomPathIndex = Random.Range(0, spawnPositions.Count);

                Vector2 randomPosition = spawnPositions[randomPathIndex];
                Waypoint waypoint = correspondingWaypoints[randomPathIndex];

                EnemyManager.Instance.SpawnEnemy(enemyToSpawn, randomPosition, waypoint);

                currentEnemiesSpawnedAtIndex++;
            }
            else
            {
                if (currentIndexAtWave + 1 == waveToSpawn.enemies.Count)
                {
                    waveHasSpawned = true;
                }
                else
                {
                    currentIndexAtWave++;
                    currentEnemiesSpawnedAtIndex = 0;
                    hasWaitedBetweenEnemies = false;
                }
            }

            timeWaited = Random.Range(0, 0.05f);
        }
    }

    public void SpawnNewWave()
    {
        if (waveIndex >= wavesToSpawn.waves.Count)
        {
            GameManager.Instance.SwitchGameState(GameManager.GameState.Victory);
            return;
        }

        waveToSpawn = wavesToSpawn.waves[waveIndex];

        currentIndexAtWave = 0;
        currentEnemiesSpawnedAtIndex = 0;
        waveHasSpawned = false;

        enemiesInThisWave = 0;
        int index = 0;
        foreach (int amount in waveToSpawn.correspondingEnemiesAmount)
        {
            enemiesInThisWave += amount;

            foreach (int holdingAmount in waveToSpawn.enemies[index].correspondingAmounts)
            {
                enemiesInThisWave += amount * holdingAmount;
            }

            int i = 0;
            foreach (EnemySO enemy in waveToSpawn.enemies[index].holdingEnemies)
            {
                foreach (int holdingAmount in enemy.correspondingAmounts)
                {
                    enemiesInThisWave += amount * holdingAmount * waveToSpawn.enemies[index].correspondingAmounts[i];
                    i++;
                }
            }
            index++;
        }

        enemiesLeftInThisWave = enemiesInThisWave;

        UIManager.Instance.UpdateEnemysLeftUI(enemiesLeftInThisWave);

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
