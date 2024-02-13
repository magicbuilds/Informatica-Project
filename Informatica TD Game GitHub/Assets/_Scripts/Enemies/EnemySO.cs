using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public string enemyName;
    public Sprite sprite;

    public int baseHealth;
    public float speed;
}
