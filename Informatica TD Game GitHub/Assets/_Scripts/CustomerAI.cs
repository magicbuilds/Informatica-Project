using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAI : MonoBehaviour
{
    [SerializeField] private Transform holdingEnemyTransform;

    public Tower tower;
    public Transform target;

    public float customerPickUpRange = 0.5f;

    public EnemyStats holdingEnemy = null;

    public float timeWaiting = 0;
    private float totalWaitTime = 5f;

    private void Start()
    {
        SetNewTarget();
    }

    private void Update()
    {
        if (target == null || target == tower.firingPoint.transform)
        {
            if (target != tower.firingPoint.transform) target = tower.firingPoint.transform;
            if (timeWaiting >= totalWaitTime)
            {
                SetNewTarget();
                timeWaiting = 0;
            }
        }

        if (Vector2.Distance(transform.position, target.position) < customerPickUpRange)
        {
            if (tower.firingPoint.transform != target)
            {
                TakeCurrentTarget();
                target = tower.firingPoint.transform;
            }
            else
            {
                timeWaiting += Time.deltaTime;

                if (holdingEnemy != null)
                {
                    KillEnemy();
                }

            }
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, tower.currentBulletSpeed * Time.deltaTime);
    }
    public void SetNewTarget()
    {
        if (tower.hasTarget)
        {
            target = tower.target.transform;

            holdingEnemy = target.GetComponent<EnemyStats>();
            holdingEnemy.isTarget = true;

            timeWaiting = 0;
        }
    }

    private void TakeCurrentTarget()
    {
        Destroy(holdingEnemy.enemyAI);
        holdingEnemy.transform.position = holdingEnemyTransform.position;
        holdingEnemy.transform.parent = transform;
    }

    private void KillEnemy()
    {
        holdingEnemy.DealDamange(holdingEnemy.health + 1);

        holdingEnemy = null;
    }
}
