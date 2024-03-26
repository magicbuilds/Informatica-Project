using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<EnemySO> enemies;

    private List<Vector2> spawnPositions = new List<Vector2>();
    private List<int> usedWaypointIndexes = new List<int>();
    private List<Waypoint> correspondingWaypoints = new List<Waypoint>();

    public int currentWave = 1;

    public int currentEnemyNumber;
    
    private void Awake()
    {
        Instance = this; 
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

    public void SpawnEnemies()
    {
        int enemiesToSpawn = 5;
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            EnemySO randomEnemy = enemies[Random.Range(0, enemies.Count)];

            int randomPathIndex = Random.Range(0, spawnPositions.Count);
            Vector2 randomPosition = spawnPositions[randomPathIndex];

            GameObject spawnedEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            spawnedEnemy.name = randomEnemy.enemyName;
            spawnedEnemy.GetComponent<SpriteRenderer>().sprite = randomEnemy.sprite;

            EnemyAI spawnedEnemyAIScript = spawnedEnemy.AddComponent<EnemyAI>();
            spawnedEnemyAIScript.currentEnemy = randomEnemy;
            spawnedEnemyAIScript.target = correspondingWaypoints[randomPathIndex];

            EnemyStats spawnedEnemyStatsScript = spawnedEnemy.GetComponent<EnemyStats>();
            spawnedEnemyStatsScript.currentEnemy = randomEnemy;
        }
        currentEnemyNumber = enemiesToSpawn;
        UIManager.Instance.UpdateEnemysLeftUI(enemiesToSpawn);
    }

    public void ReduceEnemyCount()
    {
        currentEnemyNumber--;

        if (currentEnemyNumber <= 0) 
        {
            GameManager.Instance.SwitchGameState(GameManager.GameState.EndOfWave);
        }

        UIManager.Instance.UpdateEnemysLeftUI(currentEnemyNumber);
    }
}
