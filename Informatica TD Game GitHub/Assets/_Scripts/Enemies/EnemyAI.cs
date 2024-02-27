using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemySO currentEnemy;
    public int targetPathIndex;

    private Vector2 positionOffset = Vector2.zero;

    public Waypoint target;

    private void Start()
    {
        positionOffset = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);

            EnemyManager.Instance.ReduceEnemyCount();

            Debug.Log("Tower Reached");

            return;
        }

        Vector2 targetVector = (Vector2)target.transform.position + positionOffset;
        transform.position = Vector2.MoveTowards(transform.position, targetVector, currentEnemy.speed * Time.deltaTime);

        if(Vector2.Distance(transform.position, targetVector) < 0.1f)
        {
            target = target.nextWaypoint;
        }
    }
}
