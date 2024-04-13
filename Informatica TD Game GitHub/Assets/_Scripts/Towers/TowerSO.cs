using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Tower", menuName = "ScriptableObjects/TowerSO")]
public class TowerSO : ScriptableObject
{
    public float baseDamage;
    public float baseRange;
    public float baseFireRate;
    public float baseSpecial;

    public float baseBulletSpeed;

    public towerTypes towerType;

    public GameObject towerPrefab;

    public List<UpgradeSO.UpgradeType> upgradeTypesToBuy;
    public List<float> correspondingUpgradePowers;
    public List<float> correspondigCosts;


    public enum towerTypes
    {
        KnifeThrower,
        DiscountGun,
        Blade,
        DustShooter,
        Railgun,
        Speaker,
        Shelf,
        AirConditioner,
        Checkout
    }
}
