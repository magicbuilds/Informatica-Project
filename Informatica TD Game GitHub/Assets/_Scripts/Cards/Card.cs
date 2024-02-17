using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    private CardSO currentCard;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Image icon;

    public void CardInitialization(CardSO spawnedCard)
    {
        currentCard = spawnedCard;

        nameText.text = currentCard.cardName;
        icon.sprite = currentCard.icon;
    }

    public void OnCardSelected()
    {
        CardDrawManager.Instance.RemoveCards();
    }

    public void OnExtraInformationSelected()
    {
        UIManager.Instance.ActivateExtraInformationUI(currentCard);
    }
}
