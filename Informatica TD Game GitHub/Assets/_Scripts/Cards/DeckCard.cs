using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class DeckCard : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public CardSO currentCard;

    [Header("Card")]
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private Image icon;

    [Header("StatsScreen")]
    [SerializeField] private GameObject statsScreen;
    [SerializeField] private TextMeshProUGUI statsText;

    [Header("Positions")]
    [SerializeField] public Transform downPosition;
    [SerializeField] public Transform otherCardSelectedPosition;
    [SerializeField] public Transform upPosition;

    public void CardInitialization(CardSO spawnedCard)
    {
        currentCard = spawnedCard;
        
        costText.text = currentCard.baseCost.ToString();
        nameText.text = currentCard.name;
        icon.sprite = currentCard.icon;



    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        transform.position = upPosition.position;
        UIManager.Instance.LowerDeckCardUI(transform.parent.parent);
        
        statsScreen.SetActive(true);
        statsText.text = "Range: " + currentCard.tower.baseRange + "\n" + "Damage: " +
                         currentCard.tower.baseDamage + "\n" + "Fire Rate: " +
                         currentCard.tower.baseFireRate;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        transform.position = downPosition.position;
        statsScreen.SetActive(false);

        UIManager.Instance.RaiseDeckCardUI();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (currentCard.cardType == CardSO.CardType.Tower)
        {
            InventoryManager.Instance.SetSelectedCard(this);
        }
        if (currentCard.cardType == CardSO.CardType.Upgrade)
        {
            UpgradeManager.Instance.Upgrade(currentCard.upgrade);
            InventoryManager.Instance.SetSelectedCard(this);
            InventoryManager.Instance.OnCardPlayed();
        }


    }
}
