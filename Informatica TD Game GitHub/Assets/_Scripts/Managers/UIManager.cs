using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public UpgradeSO.UpgradeType upgradeType;
    public CardSO currentCard;

    public TowerSO.towerTypes towerType;
    [Header("EnemiesLeftUI")]
    [SerializeField] private TextMeshProUGUI enemiesLeftText;
    [SerializeField] private Slider enemiesLeftBar ;

    [Header("Health")] 
    [SerializeField] private TextMeshProUGUI healthText;
    [SerializeField] private Slider healthBar;

    [Header("CardDrawUI")]
    [SerializeField] private GameObject cardDrawUI;

    [Header("cardStats")] 
    public TextMeshProUGUI cardStats1;
    public TextMeshProUGUI cardStats2;
    public TextMeshProUGUI cardStats3;
    public TextMeshProUGUI cardStats4;

    [Header("DeckCard")]
    public TextMeshProUGUI deckCard1;
    public TextMeshProUGUI deckCard2;
    public TextMeshProUGUI deckCard3;
    public TextMeshProUGUI deckCard4;
    
    
    [Header("ExtraCardInformationUI")]
    [SerializeField] private GameObject extraCardInformationUI;
    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI cardStatsText;
    [SerializeField] private TextMeshProUGUI cardDiscription;
    [SerializeField] private Image cardIcon;

    private bool isHoveringUI;

    private void Awake()
    {
        Instance = this;
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
        
        healthText.text = PlayerStatsManager.Instance.health.ToString() + "/" + PlayerStatsManager.Instance.maxHealth;
        healthBar.maxValue = PlayerStatsManager.Instance.maxHealth;
        healthBar.value = PlayerStatsManager.Instance.health;

    }

    /*public void textDrawCardUpdate()
    {
        cardStats1.text = "Range: " + UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.Range)+ "\n" + "Damage: " +
                             UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.Damage) + "\n" + "Fire Rate: " +
                             UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.FireRate);
        cardStats2.text = "Range: " + UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.Range)+ "\n" + "Damage: " +
                          UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.Damage) + "\n" + "Fire Rate: " +
                          UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.FireRate);
        cardStats3.text = "Range: " + UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.Range)+ "\n" + "Damage: " +
                          UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.Damage) + "\n" + "Fire Rate: " +
                          UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.FireRate);
        cardStats4.text = "Range: " + UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.Range)+ "\n" + "Damage: " +
                          UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.Damage) + "\n" + "Fire Rate: " +
                          UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.FireRate);
        

    }*/


    public void ActivateCardDrawUI()
    {
        cardDrawUI.SetActive(true);
        //textDrawCardUpdate();
        
    }
    
    public void DeactivateCardDrawUI()
    {
        cardDrawUI.SetActive(false);
    }

    public void ActivateExtraInformationUI(CardSO card)
    {
        cardDrawUI.SetActive(false);

        extraCardInformationUI.SetActive(true);

        cardNameText.text = card.cardName;
        cardStatsText.text = "Range: " + UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.Range)+ "\n" + "Damage: " +
                             UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.Damage) + "\n" + "Fire Rate: " +
                             UpgradeManager.Instance.ReturnValueOf(towerType, UpgradeSO.UpgradeType.FireRate);
        cardDiscription.text = card.discription;
        cardIcon.sprite = card.icon;
    }

    public void DeactivateExtraInformationUI()
    {
        extraCardInformationUI.SetActive(false);
        cardDrawUI.SetActive(true);
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
