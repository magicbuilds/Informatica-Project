using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpamBulletScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rigidBody;

    private bool hasHitEnemy = false;

    public Tower tower;

    private void FixedUpdate()
    {
        if (!IsInRange())
        {
            Destroy(gameObject);
            return;
        }

        rigidBody.velocity = transform.up * tower.currentBulletSpeed;
    }
    private bool IsInRange()
    {
        return Vector2.Distance(transform.position, tower.transform.position) < tower.currentRange;
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
}
