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
    private bool hasHitEnemy = false;
    

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    private void FixedUpdate()
    {
        if (!target) return;
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * bulletSpeed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (hasHitEnemy)
        {
            return;
        }

        other.gameObject.GetComponent<EnemyStats>().DealDamange(damage);
        hasHitEnemy = true;

        Destroy(gameObject);
    }

    private void Awake()
    {
        StartCoroutine(waiter());
        
    }

    IEnumerator waiter()
    {
        yield return new WaitForSeconds(5);
        Destroy(gameObject);
    }
}
