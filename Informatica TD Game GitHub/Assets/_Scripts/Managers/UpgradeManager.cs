using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    [Header("PlacedTowers")]
    public List<Tower> placedTowers = new List<Tower>();

    [Header("Towers + Stats")]
    public List<TowerSO.towerTypes> towerTypes = new List<TowerSO.towerTypes>();

    public List<float> baseSpecialStats = new List<float>();
    public List<float> baseRangeStats = new List<float>();
    public List<float> baseFireRateStats = new List<float>();
    public List<float> baseDamageStats = new List<float>();
    public List<float> baseBulletSpeed = new List<float>();

    private void Awake()
    {
        Instance = this;

        foreach (CardSO card in InventoryManager.Instance.cards)
        {
            if (card.cardType == CardSO.CardType.Tower)
            {
                towerTypes.Add(card.tower.towerType);
                baseSpecialStats.Add(card.tower.baseSpecial);
                baseFireRateStats.Add(card.tower.baseFireRate);
                baseRangeStats.Add(card.tower.baseRange);
                baseDamageStats.Add(card.tower.baseDamage);
                baseBulletSpeed.Add(card.tower.baseBulletSpeed);
            }
        }
    }

    private void Start()
    {

    }

    public void Upgrade(UpgradeSO upgradeStats)
    {
        int index = FindIndexOfTower(upgradeStats.correspondigTower);

        switch (upgradeStats.upgradeType)
        {
            case UpgradeSO.UpgradeType.Special:
                baseSpecialStats[index] += upgradeStats.upgradePower;
                break;
            case UpgradeSO.UpgradeType.Range:
                baseRangeStats[index] += upgradeStats.upgradePower;
                break;
            case UpgradeSO.UpgradeType.FireRate:
                baseFireRateStats[index] += upgradeStats.upgradePower;
                break;
            case UpgradeSO.UpgradeType.Damage:
                baseDamageStats[index] += upgradeStats.upgradePower;
                break;
            case UpgradeSO.UpgradeType.BulletSpeed:
                baseBulletSpeed[index] += upgradeStats.upgradePower;
                break;
        }

        foreach (Tower tower in placedTowers)
        {
            if (tower.towerCard.tower.towerType == upgradeStats.correspondigTower)
            {
                switch (upgradeStats.upgradeType)
                {
                    case UpgradeSO.UpgradeType.Special:
                        tower.currentSpecial += upgradeStats.upgradePower;
                        break;
                    case UpgradeSO.UpgradeType.Range:
                        tower.currentRange += upgradeStats.upgradePower;
                        break;
                    case UpgradeSO.UpgradeType.FireRate:
                        tower.currentFireRate += upgradeStats.upgradePower;
                        break;
                    case UpgradeSO.UpgradeType.Damage:
                        tower.currentDamage += upgradeStats.upgradePower;
                        break;
                    case UpgradeSO.UpgradeType.BulletSpeed:
                        tower.currentBulletSpeed += upgradeStats.upgradePower;
                        break;
                }
            }
        }
    }

    private int FindIndexOfTower(TowerSO.towerTypes towerType)
    {
        int index = 0;
        foreach (TowerSO.towerTypes type in towerTypes)
        {
            if (type == towerType) return index;

            index++;
        }
        Debug.Log("TowerType Not Found: " + towerType);
        return -1;
    }

    public float ReturnValueOf(TowerSO.towerTypes towerType, UpgradeSO.UpgradeType upgradeType)
    {
        int index = FindIndexOfTower(towerType);

        switch (upgradeType)
        {
            case UpgradeSO.UpgradeType.Special:
                return baseSpecialStats[index];
            case UpgradeSO.UpgradeType.Range:
                return baseRangeStats[index];
            case UpgradeSO.UpgradeType.FireRate:
                return baseFireRateStats[index];
            case UpgradeSO.UpgradeType.Damage:
                return baseDamageStats[index];
            case UpgradeSO.UpgradeType.BulletSpeed:
                return baseBulletSpeed[index];
        }
        return 0;
    }
}