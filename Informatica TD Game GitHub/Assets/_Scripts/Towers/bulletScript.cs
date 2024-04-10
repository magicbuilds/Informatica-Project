using UnityEngine;


public class BulletScript : MonoBehaviour
{ 
    [Header("References")] 
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform bulletRotationPoint;
    [SerializeField] private float rotateSpeed = 200f;
    
    private Transform target;
    private bool hasHitEnemy = false;

    public Tower tower;

    private void Start()
    {
        if (target == null) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (!IsInRange()) Destroy(gameObject);
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * tower.currentBulletSpeed;
        if (Vector2.Distance(transform.position, tower.transform.position) < tower.currentRange &&
            Vector2.Distance(transform.position, tower.transform.position) > 0.1f)
        {
            RotateTowardsTarget();
        }
       
        
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void OnCollisionEnter2D(Collision2D enemy)
    {
        if (hasHitEnemy) return;
        else hasHitEnemy = true;

        enemy.gameObject.GetComponent<EnemyStats>().DealDamange(tower.currentRange);
        Destroy(gameObject);
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
