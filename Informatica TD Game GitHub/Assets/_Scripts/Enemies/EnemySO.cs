using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "ScriptableObjects/EnemySO")]
public class EnemySO : ScriptableObject
{
    public string enemyName;

    [Header("Stats")]
    public float speed;
    public int baseHealth;
    public float baseCoins;

    [Header("Sprite(s)")]
    public Sprite xSprite;
    public Sprite ySprite;

    [Header("IfHoldingEnemies")]
    public List<EnemySO> holdingEnemies;
    public List<int> correspondingAmounts;
    public bool showHoldingEnemy;
}
