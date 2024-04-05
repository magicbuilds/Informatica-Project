using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Object = System.Object;

public class BulletScript : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Rigidbody2D rb;


    [Header("Attributes")] 
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private float damage = 10f;
    
    private Transform target;
    public Transform towerPos;
    public float targetingRange;
    private float distance;
    
<<<<<<< HEAD

=======
>>>>>>> ab26deaa70ba89db85f0ac6b309002c75cba3f36
    public void SetTarget(Transform _target)
    {
        target = _target;
    }
    private void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, towerPos.position);
        Debug.Log(targetingRange);
        Debug.Log(distance);
        if (distance > targetingRange)
        {
            Destroy(gameObject);
        }
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;

    }

    private void FixedUpdate()
    {
        distance = Vector2.Distance(transform.position, towerPos.position);
        
        if (distance > targetingRange)
        {
            Destroy(gameObject);
        }
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
<<<<<<< HEAD
=======
        if (hasHitEnemy)
        {
            return;
        }

>>>>>>> ab26deaa70ba89db85f0ac6b309002c75cba3f36
        other.gameObject.GetComponent<EnemyStats>().DealDamange(damage);
        hasHitEnemy = true;

        Destroy(gameObject);
    }



}
