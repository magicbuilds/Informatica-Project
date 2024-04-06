using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/TowerSO")]
public class TowerSO : ScriptableObject
{
    public int baseDamage;
    public int baseRange;
    public float baseFireRate;

    public float baseBulletSpeed;
}
