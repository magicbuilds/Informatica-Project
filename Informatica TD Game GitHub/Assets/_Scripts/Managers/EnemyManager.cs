using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public readonly int currentWave = 1;

    public int currentEnemies;

    [SerializeField] private float timeBetweenWaves;
    
    private void Awake()
    {
        Instance = this; 
    }

    public void ReduceEnemyCount()
    {
        currentEnemies--;

        if (currentEnemies <= 0) 
        {
            GameManager.Instance.SwitchGameState(GameManager.GameState.EndOfWave);
        }

        UIManager.Instance.UpdateEnemysLeftUI(currentEnemies);
    }
}
