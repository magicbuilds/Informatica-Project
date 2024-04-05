using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DeckCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private CardSO currentCard;

    [Header("Card")]
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image icon;

    [Header("StatsScreen")]
    [SerializeField] private GameObject statsScreen;
    [SerializeField] private TextMeshProUGUI statsText;

    [Header("Positions")]
    [SerializeField] private Transform downPosition;
    [SerializeField] private Transform upPosition;

    public void CardInitialization(CardSO spawnedCard)
    {
        currentCard = spawnedCard;

        costText.text = currentCard.baseCost.ToString();
        nameText.text = currentCard.name;
        icon.sprite = currentCard.icon;

        statsText.text = currentCard.stats;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.position = upPosition.position;
        UIManager.Instance.DeactivateDeckCardUI();


        statsScreen.SetActive(true);

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position = downPosition.position;
        statsScreen.SetActive(false);

        UIManager.Instance.ActivateDeckCardUI();
    }
}
