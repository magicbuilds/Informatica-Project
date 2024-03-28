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

    //DeckCards
    public List<GameObject> deckCardTemplates;


    private void Awake()
    {
        Instance = this;
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
        cardStatsText.text = card.stats;
        cardDiscription.text = card.discription;
        cardIcon.sprite = card.icon;
    }

    public void DeactivateExtraInformationUI()
    {
        extraCardInformationUI.SetActive(false);
        cardDrawUI.SetActive(true);
    }

    public void ActivateDeckCardUI()
    {
        foreach (GameObject card in deckCardTemplates)
        {
            card.SetActive(true);
        }
    }

    public void DeactivateDeckCardUI()
    {
        foreach (GameObject card in deckCardTemplates)
        {
            card.SetActive(false);
        }
    }
}
