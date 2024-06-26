using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    [SerializeField] private GameObject enemyPrefab;

    public List<EnemyStats> enemiesLeft = new List<EnemyStats>();
    
    private void Awake()
    {
        Instance = this; 
    }

    public void SpawnEnemy(EnemySO enemy, Vector2 position, Waypoint target)
    {
        GameObject spawnedEnemy = Instantiate(enemyPrefab, position, Quaternion.identity);
        spawnedEnemy.name = enemy.enemyName;

        EnemyAI spawnedEnemyAIScript = spawnedEnemy.AddComponent<EnemyAI>();
        spawnedEnemyAIScript.currentEnemy = enemy;
        spawnedEnemyAIScript.target = target;

        EnemyStats spawnedEnemyStatsScript = spawnedEnemy.GetComponent<EnemyStats>();
        spawnedEnemyStatsScript.enemyAI = spawnedEnemyAIScript;
        spawnedEnemyStatsScript.currentEnemy = enemy;

        spawnedEnemyAIScript.enemyStats = spawnedEnemyStatsScript;

        enemiesLeft.Add(spawnedEnemyStatsScript);
    }

    public void ReduceEnemyCount(EnemyStats killedEnemy)
    {
        WaveManager.Instance.enemiesLeftInThisWave--;

        enemiesLeft.Remove(killedEnemy);

        if (WaveManager.Instance.enemiesLeftInThisWave <= 0) 
        {
            GameManager.Instance.SwitchGameState(GameManager.GameState.EndOfWave);

        }

        UIManager.Instance.UpdateEnemysLeftUI(WaveManager.Instance.enemiesLeftInThisWave);
    }
}
