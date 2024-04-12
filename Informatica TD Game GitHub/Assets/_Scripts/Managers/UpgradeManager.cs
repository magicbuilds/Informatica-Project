using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public List<Tower> placedTowers = new List<Tower>();

    [Header("Checkout")]
    public float maxPeopleAtCheckout = 1;

    [Header("Shelf")]
    public float baseShelfBombTargetingRange;
    public float baseShelfTargetingRange;
    public float baseShelfFireRate;
    public float baseShelfDamage;

    [Header("DiscountGun")]
    public float baseDiscountGunTargetingRange;
    public float baseDiscountGunFireRate;
    public float baseDiscountGunDamage;

    [Header("KnifeThrower")]
    public float baseKnifeThrowerTargetingRange;
    public float baseKnifeThrowerFireRate;
    public float baseKnifeThrowerDamage;

    [Header("DustShooter")]
    public float baseDustShooterTargetingRange;
    public float baseDustShooterFireRate;
    public float baseDustShooterDamage;

    [Header("AirConditioner")]
    public float baseAirConditionerTargetingRange;
    public float baseAirConditionerFireRate;
    public float baseAirConditionerDamage;

    [Header("Speaker")]
    public float baseSpeakerTargetingRange;
    public float baseSpeakerFireRate;
    public float baseSpeakerDamage;

    private void Awake()
    {
        Instance = this;
    }

    public void UpgradeMain(UpgradeSO upgradeStats)
    {
        switch (upgradeStats.correspondigTower) 
        {
            case TowerSO.towers.Checkout:
                UpgradeCheckout(upgradeStats);
                break;
            case TowerSO.towers.Shelf:
                UpgradeShelf(upgradeStats);
                break;
            case TowerSO.towers.DiscountGun:
                UpgradeDiscountGun(upgradeStats);
                break;
            case TowerSO.towers.KnifeThrower:
                UpgradeKnifeThrower(upgradeStats);
                break;
            case TowerSO.towers.DustShooter:
                UpgradeDustShooter(upgradeStats);
                break;
            case TowerSO.towers.AirConditioner:
                UpgradeAirConditioner(upgradeStats);
                break;
            case TowerSO.towers.Railgun:
                UpgradeRailGun(upgradeStats);
                break;
            case TowerSO.towers.Speaker:
                UpgradeSpeaker(upgradeStats);
                break;
        
        } 
    }

    private void UpgradeCheckout(UpgradeSO upgradeStats)
    {
        switch (upgradeStats.upgradeType)
        {
            case UpgradeSO.UpgradeType.Special:
                maxPeopleAtCheckout += upgradeStats.upgradePower;
                break;
        }
    }

    private void UpgradeShelf(UpgradeSO upgradeStats)
    {
        switch (upgradeStats.upgradeType)
        {
            case UpgradeSO.UpgradeType.Special:
                baseShelfBombTargetingRange += upgradeStats.upgradePower;
                break;
            case UpgradeSO.UpgradeType.Range:
                break;
            case UpgradeSO.UpgradeType.FireRate:
                break;
            case UpgradeSO.UpgradeType.Damage:
                break;
        }

        foreach (Tower tower in placedTowers)
        {
            if (tower.currentTower.towerType == upgradeStats.correspondigTower)
            {
                switch (upgradeStats.upgradeType)
                {
                    case UpgradeSO.UpgradeType.Special:
                        tower.bombTargetingRange += upgradeStats.upgradePower;
                        break;
                    case UpgradeSO.UpgradeType.Range:
                        break;
                    case UpgradeSO.UpgradeType.FireRate:
                        break;
                    case UpgradeSO.UpgradeType.Damage:
                        break;
                }
            }
        }
    }

    private void UpgradeDiscountGun(UpgradeSO upgradeStats)
    {
        switch (upgradeStats.upgradeType)
        {
            case UpgradeSO.UpgradeType.Special:
                break;
            case UpgradeSO.UpgradeType.Range:
                break;
            case UpgradeSO.UpgradeType.FireRate:
                break;
            case UpgradeSO.UpgradeType.Damage:
                break;
        }

        foreach (Tower tower in placedTowers)
        {
            if (tower.currentTower.towerType == upgradeStats.correspondigTower)
            {
                switch (upgradeStats.upgradeType)
                {
                    case UpgradeSO.UpgradeType.Special:
                        break;
                    case UpgradeSO.UpgradeType.Range:
                        break;
                    case UpgradeSO.UpgradeType.FireRate:
                        break;
                    case UpgradeSO.UpgradeType.Damage:
                        break;
                }
            }
        }
    }

    private void UpgradeKnifeThrower(UpgradeSO upgradeStats)
    {
        switch (upgradeStats.upgradeType)
        {
            case UpgradeSO.UpgradeType.Special:
                break;
            case UpgradeSO.UpgradeType.Range:
                break;
            case UpgradeSO.UpgradeType.FireRate:
                break;
            case UpgradeSO.UpgradeType.Damage:
                break;
        }

        foreach (Tower tower in placedTowers)
        {
            if (tower.currentTower.towerType == upgradeStats.correspondigTower)
            {
                switch (upgradeStats.upgradeType)
                {
                    case UpgradeSO.UpgradeType.Special:
                        break;
                    case UpgradeSO.UpgradeType.Range:
                        break;
                    case UpgradeSO.UpgradeType.FireRate:
                        break;
                    case UpgradeSO.UpgradeType.Damage:
                        break;
                }
            }
        }
    }

    private void UpgradeDustShooter(UpgradeSO upgradeStats)
    {
        switch (upgradeStats.upgradeType)
        {
            case UpgradeSO.UpgradeType.Special:
                break;
            case UpgradeSO.UpgradeType.Range:
                break;
            case UpgradeSO.UpgradeType.FireRate:
                break;
            case UpgradeSO.UpgradeType.Damage:
                break;
        }

        foreach (Tower tower in placedTowers)
        {
            if (tower.currentTower.towerType == upgradeStats.correspondigTower)
            {
                switch (upgradeStats.upgradeType)
                {
                    case UpgradeSO.UpgradeType.Special:
                        break;
                    case UpgradeSO.UpgradeType.Range:
                        break;
                    case UpgradeSO.UpgradeType.FireRate:
                        break;
                    case UpgradeSO.UpgradeType.Damage:
                        break;
                }
            }
        }
    }

    private void UpgradeAirConditioner(UpgradeSO upgradeStats)
    {
        switch (upgradeStats.upgradeType)
        {
            case UpgradeSO.UpgradeType.Special:
                break;
            case UpgradeSO.UpgradeType.Range:
                break;
            case UpgradeSO.UpgradeType.FireRate:
                break;
            case UpgradeSO.UpgradeType.Damage:
                break;
        }

        foreach (Tower tower in placedTowers)
        {
            if (tower.currentTower.towerType == upgradeStats.correspondigTower)
            {
                switch (upgradeStats.upgradeType)
                {
                    case UpgradeSO.UpgradeType.Special:
                        break;
                    case UpgradeSO.UpgradeType.Range:
                        break;
                    case UpgradeSO.UpgradeType.FireRate:
                        break;
                    case UpgradeSO.UpgradeType.Damage:
                        break;
                }
            }
        }
    }

    private void UpgradeRailGun(UpgradeSO upgradeStats)
    {
        switch (upgradeStats.upgradeType)
        {
            case UpgradeSO.UpgradeType.Special:
                break;
            case UpgradeSO.UpgradeType.Range:
                break;
            case UpgradeSO.UpgradeType.FireRate:
                break;
            case UpgradeSO.UpgradeType.Damage:
                break;
        }

        foreach (Tower tower in placedTowers)
        {
            if (tower.currentTower.towerType == upgradeStats.correspondigTower)
            {
                switch (upgradeStats.upgradeType)
                {
                    case UpgradeSO.UpgradeType.Special:
                        break;
                    case UpgradeSO.UpgradeType.Range:
                        break;
                    case UpgradeSO.UpgradeType.FireRate:
                        break;
                    case UpgradeSO.UpgradeType.Damage:
                        break;
                }
            }
        }
    }

    private void UpgradeSpeaker(UpgradeSO upgradeStats)
    {
        switch (upgradeStats.upgradeType)
        {
            case UpgradeSO.UpgradeType.Special:
                break;
            case UpgradeSO.UpgradeType.Range:
                break;
            case UpgradeSO.UpgradeType.FireRate:
                break;
            case UpgradeSO.UpgradeType.Damage:
                break;
        }

        foreach (Tower tower in placedTowers)
        {
            if (tower.currentTower.towerType == upgradeStats.correspondigTower)
            {
                switch (upgradeStats.upgradeType)
                {
                    case UpgradeSO.UpgradeType.Special:
                        break;
                    case UpgradeSO.UpgradeType.Range:
                        break;
                    case UpgradeSO.UpgradeType.FireRate:
                        break;
                    case UpgradeSO.UpgradeType.Damage:
                        break;
                }
            }
        }
    }
}
