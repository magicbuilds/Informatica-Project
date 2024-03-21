using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Tower : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private Transform turretRotationPoint;
    
    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;

    private void OnDrawGizmos()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
