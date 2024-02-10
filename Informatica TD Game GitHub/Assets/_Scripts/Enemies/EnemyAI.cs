using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemySO currentEnemy;

    private int currentWaypointID;

    private void Start()
    {
        currentWaypointID = WaypointManager.Instance.waypoints.Count - 1;
    }

    private void Update()
    {
        if (currentWaypointID < 0)
        {
            Destroy(gameObject);

            Debug.Log("Tower Reached");

            return;
        }
        
        Vector2 targetPosition = WaypointManager.Instance.waypoints[currentWaypointID].transform.position;
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentEnemy.enemySpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentWaypointID--;
        }
    }
}
