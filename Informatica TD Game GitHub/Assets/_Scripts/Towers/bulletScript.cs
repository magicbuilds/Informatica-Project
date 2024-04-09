using UnityEngine;


public class BulletScript : MonoBehaviour
{ 
    [Header("References")] [SerializeField]
    private Rigidbody2D rb;
    
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

        rb.velocity = direction * tower.currentFireRate;
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
}
