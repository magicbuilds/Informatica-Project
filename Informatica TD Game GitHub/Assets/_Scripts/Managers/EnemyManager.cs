using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;

    public readonly int currentWave = 1;

    public int maxEnemies;
    [SerializeField] private int currentEnemies;

    [SerializeField] private float timeBetweenWaves;
    

    private void Awake()
    {
        Instance = this; 
    }
}
