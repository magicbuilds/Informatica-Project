using UnityEngine;


public class BulletScript : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Rigidbody2D rb;


    [Header("Attributes")] 
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float damage = 10f;
    
    private Transform target;
    private bool hasHitEnemy = false;
    

    public Vector2 towerPosition;
    public float towerRange;

    private void Start()
    {
        if (target == null) Destroy(gameObject);
    }

    private void FixedUpdate()
    {
        if (!IsInRange()) Destroy(gameObject);
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }
    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (hasHitEnemy) return;
        else hasHitEnemy = true;

        other.gameObject.GetComponent<EnemyStats>().DealDamange(damage);
        Destroy(gameObject);
    }

    private bool IsInRange()
    {
        return Vector2.Distance(transform.position, towerPosition) < towerRange;
    }
}
