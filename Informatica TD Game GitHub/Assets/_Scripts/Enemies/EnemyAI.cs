using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemySO currentEnemy;

    private (int pathIndex, int waypointNumber) currentWaypointID;

    private void Start()
    {
        currentWaypointID.pathIndex = Random.Range(0, ChunckManager.Instance.totalPathCount);

        currentWaypointID.waypointNumber = 0;
        foreach ((int,int) waypointID in WaypointManager.Instance.waypoints.Keys)
        {
            if(waypointID.Item2 > currentWaypointID.waypointNumber && waypointID.Item1 == currentWaypointID.pathIndex)
            {
                currentWaypointID.waypointNumber = waypointID.Item2;    
            }
        }
    }

    private void Update()
    {
        if (currentWaypointID.waypointNumber < 0 || currentWaypointID.pathIndex < 0)
        {
            Destroy(gameObject);

            EnemyManager.Instance.ReduceEnemyCount();

            Debug.Log("Tower Reached");

            return;
        }

        Vector2 targetPosition = WaypointManager.Instance.waypoints[currentWaypointID].transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentEnemy.speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            if (!WaypointManager.Instance.waypoints.ContainsKey((currentWaypointID.pathIndex, currentWaypointID.waypointNumber -1)))
            {
                currentWaypointID.pathIndex--;
            }
            currentWaypointID.waypointNumber--;

        }
    }
}
