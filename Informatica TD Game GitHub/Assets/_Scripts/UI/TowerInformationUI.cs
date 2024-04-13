using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TowerInformationUI : MonoBehaviour
{
    public static TowerInformationUI Instance;

    public CardSO selectedTowerCard;
    public GameObject selecterTowerObject;

    public List<Transform> upgradeSlots;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateUpgradeButtons()
    {

    }

    public void SetSelectedTower(GameObject tower)
    {
        
        selecterTowerObject = tower;
        
    }

    public void RemoveSelectedTower()
    {
        
        selecterTowerObject = null;
        
    }

    public void SellTower()
    {
        UIManager.Instance.SetHoveringState(false);
        if (selectedTowerCard != null) 
        {
            PlayerStatsManager.Instance.AddRemoveCoins(selectedTowerCard.baseCost / 2);

            Destroy(selecterTowerObject);
            UIManager.Instance.DeactivateTowerInformationUI();
        }
        

    }
}
