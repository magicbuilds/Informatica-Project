using UnityEngine;


public class BulletScript : MonoBehaviour
{ 
    [Header("References")] 
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Transform bulletRotationPoint;
    [SerializeField] private float rotateSpeed = 200f;
    
    private EnemyStats target;
    private bool hasHitEnemy = false;

    public Tower tower;

    private void Start()
    {
        target = tower.target;
        if (target == null) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (target == null || !IsInRange())
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (target.transform.position - transform.position).normalized;
        rigidBody.velocity = direction * tower.currentBulletSpeed;

        if (Vector2.Distance(transform.position, tower.transform.position) > 0.1f)
        {
            RotateTowardsTarget();
        } 
    }

    private void OnCollisionEnter2D(Collision2D enemy)
    {
        if (hasHitEnemy) return;

        if (enemy.gameObject.TryGetComponent<EnemyStats>(out EnemyStats enemyStats))
        {
            enemyStats.DealDamange(tower.currentDamage);
            hasHitEnemy = true;
            Destroy(gameObject);
        }
    }

    private bool IsInRange()
    {
        return Vector2.Distance(transform.position, tower.transform.position) < tower.currentRange;
    }
    
    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        bulletRotationPoint.rotation = Quaternion.RotateTowards(bulletRotationPoint.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }
}
