using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "ScriptableObjects/UpgradeSO")]
public class UpgradeSO : ScriptableObject
{
    public TowerSO.towers correspondigTower;
    public UpgradeType upgradeType;

    public float upgradePower;

    public enum UpgradeType
    {
        Range,
        Damage,
        FireRate,
        Special
    }
}
