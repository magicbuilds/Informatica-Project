using UnityEngine;

public class TempWaypointSpawner : MonoBehaviour
{
    public static TempWaypointSpawner Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void CreateWaypoints()
    {
        for (float i = 0; i < 10; i++)
        {
            Vector2 randomPosition = new Vector2(Random.Range(-5, 5), Random.Range(-5, 5));
            WaypointManager.Instance.AddNewWaypoint(randomPosition);
        }

        GameManager.Instance.SwitchGameState(GameManager.GameState.StartNewWave);
    }
}
