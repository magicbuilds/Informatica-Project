using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public EnemySO currentEnemy;
    public int targetPathIndex;

    public Vector2 positionOffset = Vector2.zero;

    public Waypoint target;

    private Vector2 lastPosition;

    private SpriteRenderer spriteRenderer;

    

    private void Start()
    {
        lastPosition = (Vector2)transform.position;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = currentEnemy.xSprite;

        if (currentEnemy.ySprite == null)
        {
            positionOffset = new Vector2(Random.Range(-0.2f, 0.2f), Random.Range(-0.2f, 0.2f));
        }
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);

            EnemyManager.Instance.ReduceEnemyCount();

            return;
        }

        Vector2 targetVector = (Vector2)target.transform.position + positionOffset;
        transform.position = Vector2.MoveTowards(transform.position, targetVector, currentEnemy.speed * Time.deltaTime);

        float deltaX = (transform.position.x - lastPosition.x);
        spriteRenderer.flipX = false;

        if (currentEnemy.ySprite != null)
        {
            float deltaY = transform.position.y - lastPosition.y;
            if (Mathf.Abs(deltaY) > 0.01f)
            {
                spriteRenderer.sprite = currentEnemy.ySprite;
                spriteRenderer.flipY = false;
                if (deltaY < 0.01f) spriteRenderer.flipY = true;
            }
            else
            {
                spriteRenderer.sprite = currentEnemy.xSprite;
            }
        }
        if (deltaX < -0.01f) spriteRenderer.flipX = true;

        if (Vector2.Distance(transform.position, targetVector) < 0.1f)
        {
            target = target.nextWaypoint;
        }

        lastPosition = (Vector2)transform.position;
    }
}
