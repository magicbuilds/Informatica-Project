using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    [SerializeField] private Transform holdingEnemyTransform;

    public Tower tower;
    public Transform target;

    public float customerPickUpRange = 0.5f;

    private EnemyStats holdingEnemy = null;

    public bool hasSoldCurrentTarget = false;

    public void SetNewTarget(Transform newTarget)
    {
        target = newTarget;

        holdingEnemy = newTarget.GetComponent<EnemyStats>();
        holdingEnemy.isTarget = true;

        hasSoldCurrentTarget = false;
    }

    private void Update()
    {
        if (target == null)
        {
            target = tower.firingPoint;
        }

        if (Vector2.Distance(transform.position, target.position) < customerPickUpRange)
        {
            if (tower.firingPoint.transform != target)
            {
                Destroy(holdingEnemy.enemyAI);
                holdingEnemy.transform.position = holdingEnemyTransform.position;
                holdingEnemy.transform.parent = transform;
                target = tower.firingPoint.transform;
            }
            else
            {
                if (!hasSoldCurrentTarget)
                {
                    holdingEnemy.DealDamange(holdingEnemy.health + 1);

                    holdingEnemy = null;
                }
                hasSoldCurrentTarget = true;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, tower.currentBulletSpeed * Time.deltaTime);
    }
}
