using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private CircleCollider2D bombCollider;

    private EnemyStats target;
    private bool hasExploded = false;
    private List<EnemyStats> enemiesInRange = new List<EnemyStats>();

    public Tower tower;

    private void Start()
    {
        target = tower.target;

        if (target != null)
        {
            bombCollider.radius = tower.bombTargetingRange;
        }
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

        if (Vector2.Distance(transform.position, target.transform.position) <= 0.1f)
        {
            Explode();
        }
    }

    private void Explode()
    {
        hasExploded = true;

        foreach (EnemyStats enemy in enemiesInRange)
        {
            Debug.Log(enemy);
            enemy.DealDamange(tower.currentDamage);
        }
    }

    private bool IsInRange()
    {
        return Vector2.Distance(transform.position, tower.transform.position) < tower.currentRange;
    }

    private void OnCollisionEnter2D(Collision2D enemy)
    {
        if (hasExploded) return;

        if (enemy.gameObject.TryGetComponent<EnemyStats>(out EnemyStats enemyStats))
        {
            enemiesInRange.Add(enemyStats);
        }
    }

    private void OnCollisionExit2D(Collision2D enemy)
    {
        if (hasExploded) return;

        if (enemy.gameObject.TryGetComponent<EnemyStats>(out EnemyStats enemyStats))
        {
            enemiesInRange.Remove(enemyStats);
        }
    }
}
