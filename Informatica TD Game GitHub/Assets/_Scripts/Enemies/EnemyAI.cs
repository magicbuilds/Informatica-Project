using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemySO currentEnemy;
    public int targetPathIndex;

    public Waypoint target;

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);

            EnemyManager.Instance.ReduceEnemyCount();

            Debug.Log("Tower Reached");

            return;
        }

        transform.position = Vector2.MoveTowards(transform.position, target.transform.position, currentEnemy.speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, target.transform.position) < 0.1f)
        {
            target = target.nextWaypoint;
        }
    }
}
