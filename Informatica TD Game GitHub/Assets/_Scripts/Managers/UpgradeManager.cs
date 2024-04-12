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
    }

    private void Start()
    {
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

    public void Upgrade(UpgradeSO upgradeStats)
    {
        int index = FindIndexOfTower(upgradeStats.correspondigTower);

        switch (upgradeStats.upgradeType)
        {
            case UpgradeSO.UpgradeType.Special:
                baseSpecialStats[index] += upgradeStats.upgradePower;
                break;
            case UpgradeSO.UpgradeType.Range:
                baseFireRateStats[index] += upgradeStats.upgradePower;
                break;
            case UpgradeSO.UpgradeType.FireRate:
                baseRangeStats[index] += upgradeStats.upgradePower;
                break;
            case UpgradeSO.UpgradeType.Damage:
                baseDamageStats[index] += upgradeStats.upgradePower;
                break;
            case UpgradeSO.UpgradeType.BulletSpeed:
                baseBulletSpeed[index] += upgradeStats.upgradePower;
                break;
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