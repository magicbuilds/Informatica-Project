using System.Collections.Generic;
using UnityEngine;

public class TempEnemySpawner : MonoBehaviour
{
    public static TempEnemySpawner Instance;

    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private List<EnemySO> enemies;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnEnemy()
    {
        int enemiesToSpawn = 100 * (EnemyManager.Instance.currentWave + 1);
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemyIndex = Random.Range(0, enemies.Count);
            EnemySO randomEnemy = enemies[randomEnemyIndex];

            Vector2 randomPosition = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));

            GameObject spawnedEnemy = Instantiate(enemyPrefab, randomPosition, Quaternion.identity);

            spawnedEnemy.name = randomEnemy.enemyName;
            spawnedEnemy.GetComponent<SpriteRenderer>().sprite = randomEnemy.sprite;

            EnemyAI spawnedEnemyAIScript = spawnedEnemy.GetComponent<EnemyAI>();
            spawnedEnemyAIScript.currentEnemy = randomEnemy;

            EnemyStats spawnedEnemyStatsScript = spawnedEnemy.GetComponent<EnemyStats>();
            spawnedEnemyStatsScript.currentEnemy = randomEnemy;
        }
        EnemyManager.Instance.currentEnemies = enemiesToSpawn;
        UIManager.Instance.UpdateEnemysLeftUI(enemiesToSpawn);
    }
}
