using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class TowerInformationUI : MonoBehaviour
{
    public static TowerInformationUI Instance;

    public GameObject selecterTowerObject;

    [SerializeField] private GameObject sellButton;

    public List<Transform> upgradeSlots;

    private bool deckCardIsSelected = false;

    private void Awake()
    {
        Instance = this;
    }

    public void UpdateUpgradeButtons()
    {

    }

    public void SellTower()
    {
        if (deckCardIsSelected) return;

        UIManager.Instance.SetHoveringState(false);
        if (selecterTowerObject != null) 
        {
            float cost = selecterTowerObject.GetComponent<Tower>().towerCard.baseCost;
            PlayerStatsManager.Instance.AddRemoveCoins(cost / 2);

            Destroy(selecterTowerObject);
            UIManager.Instance.DeactivateTowerInformationUI();
        }
    }   

    public void DeckCardSelected()
    {
        deckCardIsSelected = true;
        sellButton.SetActive(false);
    }

    public void DeckCardDeselected()
    {
        deckCardIsSelected = false;
        sellButton.SetActive(true);
    }
}
