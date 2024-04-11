using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.PlasticSCM.Editor.WebApi.CredentialsResponse;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance;

    public List<Tower> placedTowers = new List<Tower>();

    private bool hasAlreadyUpgradedBase;

    public float baseShelfBombTargetingRange = 1.5f;

    public float maxPeopleAtCheckout = 1;

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
        foreach (Tower tower in placedTowers)
        {
            if (tower.currentTower.towerType == upgradeStats.correspondigTower)
            {
                switch (upgradeStats.upgradeType)
                {
                    case UpgradeSO.UpgradeType.Special:
                        if (!hasAlreadyUpgradedBase)
                        {
                            baseShelfBombTargetingRange += upgradeStats.upgradePower;
                            hasAlreadyUpgradedBase = true;
                        }

                        tower.bombTargetingRange += upgradeStats.upgradePower;
                        break;
                }
            }
        }
        hasAlreadyUpgradedBase = false;
    }
}
