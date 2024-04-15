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

    private Transform stopPosition;

    private void Start()
    {
        SetNewTarget();
        stopPosition = tower.firingPoints[0].transform;
    }

    private void Update()
    {
        //Check For Delay
        if (timeWaiting < tower.currentFireRate) 
        {
            timeWaiting += Time.deltaTime;
            return;
        }

        if (target == null)
        {
            SetNewTarget();
            return;
        }

        // Can Pickup Target Or is At Checkout
        if (Vector2.Distance(transform.position, target.position) < customerPickUpRange)
        {
            //At Checkout
            if (target == stopPosition.transform)
            {
                KillEnemy();
                timeWaiting = 0;
                SetNewTarget();
            }

            //At Enemy
            else
            {
                TakeCurrentTarget();
                target = stopPosition.transform;
            }
        }

        transform.position = Vector2.MoveTowards(transform.position, target.position, tower.currentBulletSpeed * Time.deltaTime);
        RotateTowardsTarget();
    }

    public void SetNewTarget()
    {
        if (EnemyManager.Instance.enemiesLeft.Count <= 0)
        {
            target = stopPosition;
            return;
        }

        int randomIndex = Random.Range(0, EnemyManager.Instance.enemiesLeft.Count);
        EnemyStats targetEnemy = EnemyManager.Instance.enemiesLeft[randomIndex];

        if (targetEnemy.currentEnemy.isBoss == true)
        {
            targetEnemy = null;
        }

        if (targetEnemy.isTarget || targetEnemy.isDead)
        {
            targetEnemy = null;
        }

        if (targetEnemy != null)
        {
            target = targetEnemy.transform;
            targetEnemy.isTarget = true;
        }
        holdingEnemy = targetEnemy;
    }

    private void TakeCurrentTarget()
    {
        Destroy(holdingEnemy.enemyAI);
        holdingEnemy.transform.position = holdingEnemyTransform.position;
        holdingEnemy.transform.parent = transform;
    }

    private void KillEnemy()
    {
        if (holdingEnemy != null)
        {
            holdingEnemy.DealDamange(holdingEnemy.health + 1);

            holdingEnemy = null;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.transform.position.y - transform.position.y, target.transform.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 360f);
    }
}
