using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    //EnemiesLeft
    [SerializeField] private TextMeshProUGUI enemiesLeftText;

    //CardDraw
    [SerializeField] private GameObject cardDrawUI;

    //ExtraCardInformation
    [SerializeField] private GameObject extraCardInformationUI;

    [SerializeField] private TextMeshProUGUI cardNameText;
    [SerializeField] private TextMeshProUGUI cardStatsText;
    [SerializeField] private TextMeshProUGUI cardDiscription;
    [SerializeField] private Image cardIcon;

    //Upgrade
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

    public void UpdateEnemysLeftUI(int enemysLeft)
    {
        enemiesLeftText.text = enemysLeft.ToString();
    }

    public void ActivateCardDrawUI()
    {
        cardDrawUI.SetActive(true);
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
        //cardStatsText.text = card.stats;
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
}
