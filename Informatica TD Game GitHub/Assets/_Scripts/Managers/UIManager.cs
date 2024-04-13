using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("EnemiesLeftUI")]
    [SerializeField] private TextMeshProUGUI enemiesLeftText;
    [SerializeField] private Slider enemiesLeftBar;

    [Header("HealthBar")] 
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Slider healthBar;

    [Header("CoinBar")]
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] private Slider coinBar;

    [Header("CardDrawUI")]
    [SerializeField] private GameObject cardDrawUI;
    
    
    [Header("ExtraCardInformationUI")]
    [SerializeField] private GameObject extraCardInformationUI;
    [SerializeField] private TextMeshProUGUI cardNameTextExtraInformationUI;
    [SerializeField] private TextMeshProUGUI cardStatsTextExtraInformationUI;
    [SerializeField] private TextMeshProUGUI cardDiscriptionExtraInformationUI;
    [SerializeField] private Image cardIconExtraInformationUI;

    [Header("TowerInformation")]
    [SerializeField] private GameObject towerInformationUI;
    [SerializeField] private TextMeshProUGUI cardNameTowerInformationUI;
    [SerializeField] private TextMeshProUGUI cardStatsTowerInformationUI;
    [SerializeField] private Image cardIconTowerInformationUI;

    private bool isHoveringUI;
    private GameObject currentUI;

    private void Awake()
    {
        Instance = this;
    }

    public void OnESCPressed()
    {

    }
    
    public void SetHoveringState(bool state)
    {
        isHoveringUI = state;
    }

    public bool IsHoveringUI()
    {
        
        return isHoveringUI;
    }

    public void UpdateEnemysLeftUI(int enemiesLeft)
    {
        enemiesLeftText.text = enemiesLeft + "/" + WaveManager.Instance.enemiesInThisWave;
        enemiesLeftBar.maxValue = WaveManager.Instance.enemiesInThisWave;
        enemiesLeftBar.value= enemiesLeft;
    }

    public void UpdateHealthUI()
    {
        healthText.text = PlayerStatsManager.Instance.health + "/" + PlayerStatsManager.Instance.maxHealth;
        healthBar.maxValue = PlayerStatsManager.Instance.maxHealth;
        healthBar.value = PlayerStatsManager.Instance.health;
    }
    public void UpdateCoinsUI()
    {
        coinText.text = PlayerStatsManager.Instance.coins + "/" + PlayerStatsManager.Instance.maxCoins;
        coinBar.maxValue = PlayerStatsManager.Instance.maxCoins;
        coinBar.value = PlayerStatsManager.Instance.coins;
    }


    public void ActivateCardDrawUI()
    {
        cardDrawUI.SetActive(true);

        currentUI = cardDrawUI;

        DeactivateTowerInformationUI();

    }
    
    public void DeactivateCardDrawUI()
    {
        cardDrawUI.SetActive(false);

        currentUI = null;
    }

    public void ActivateExtraInformationUI(CardSO card)
    {
        cardDrawUI.SetActive(false);

        extraCardInformationUI.SetActive(true);

        cardNameTextExtraInformationUI.text = card.cardName;
        cardStatsTextExtraInformationUI.text = card.GetStats();

        cardDiscriptionExtraInformationUI.text = card.discription;
        cardIconExtraInformationUI.sprite = card.icon;

        currentUI = extraCardInformationUI;

    }

    public void DeactivateExtraInformationUI()
    {
        extraCardInformationUI.SetActive(false);
        cardDrawUI.SetActive(true);

        currentUI = cardDrawUI;
    }

    public void ActivateTowerInformationUI(Tower tower)
    {
        TowerInformationUI.Instance.SetSelectedTower(tower.gameObject);

        towerInformationUI.SetActive(true);

        cardNameTowerInformationUI.text = tower.towerCard.cardName;
        cardStatsTowerInformationUI.text = tower.towerCard.GetStats();
        cardIconTowerInformationUI.sprite = tower.towerCard.icon;

        TowerInformationUI.Instance.selectedTowerCard = tower.towerCard;

        currentUI = towerInformationUI;
    }

    public void DeactivateTowerInformationUI()
    {
        towerInformationUI.SetActive(false);
        TowerInformationUI.Instance.selectedTowerCard = null;

        currentUI = null;
    }

    public void ActivateESCMenu()
    {

    }

    public void DeactivateESCMenu()
    {

    }

    public void RaiseDeckCardUI()
    {
        foreach (GameObject slot in InventoryManager.Instance.deckCardSlots)
        {
            DeckCard deckCard = slot.GetComponentInChildren<DeckCard>();
            deckCard.transform.position = deckCard.downPosition.position;
        }
    }

    public void LowerDeckCardUI(Transform selectedSlot)
    {
        foreach (GameObject slot in InventoryManager.Instance.deckCardSlots)
        {
            if (slot != selectedSlot.gameObject)
            {
                DeckCard deckCard = slot.GetComponentInChildren<DeckCard>();
                deckCard.transform.position = deckCard.otherCardSelectedPosition.position;
            }
        }
    }

    public void ShowDeckCards(List<int> indexes)
    {
        int index = 0;
        foreach (GameObject slot in InventoryManager.Instance.deckCardSlots)
        {
            if (indexes.Contains(index))
            {
                slot.SetActive(true);
            }
            
            DeckCard deckCard = slot.GetComponentInChildren<DeckCard>();
            deckCard.transform.position = deckCard.downPosition.position;

            index++;
        }
    }

    public void HideDeckCards()
    {
        foreach (GameObject slot in InventoryManager.Instance.deckCardSlots)
        {
            slot.SetActive(false);
        }
    }
}
