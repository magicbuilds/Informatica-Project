using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave000", menuName = "ScriptableObjects/WaveSO")]
public class WaveSO : ScriptableObject
{
    [Header("Stats")]
    public float baseHealthMultiplier;
    public float baseCoinsMultiplier;

    [Header("Enemies")]
    public List<EnemySO> enemies;
    public List<int> correspondingEnemiesAmount;
}
