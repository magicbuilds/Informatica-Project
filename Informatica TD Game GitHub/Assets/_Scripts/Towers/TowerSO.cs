using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/TowerSO")]
public class TowerSO : ScriptableObject
{
    public string towerName;
    public Sprite sprite;

    public int baseDamage;
    public float fireRate;
}
