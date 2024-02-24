using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<EnemySO> enemies;

    private Dictionary<int, Vector2> spawnPositions = new Dictionary<int, Vector2>();

    public int currentWave = 1;

    public int currentEnemyNumber;
    
    private void Awake()
    {
        Instance = this; 
    }

    public void UpdateSpawnLocations(int newPathIndex, Vector2 newPosition)
    {
        spawnPositions[newPathIndex] = newPosition;
    }

    public void SpawnEnemies()
    {
        int enemiesToSpawn = 100;
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
            spawnedEnemyAIScript.targetPathIndex = randomPathIndex;

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
            GameManager.Instance.SwitchGameState(GameManager.GameState.ChoseNextChunk);
        }

        UIManager.Instance.UpdateEnemysLeftUI(currentEnemyNumber);
    }
}
