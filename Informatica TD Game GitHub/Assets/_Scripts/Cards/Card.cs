using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private Image icon;

    public void CardInitialization(CardSO spawnedCard)
    {
        nameText.text = spawnedCard.cardName;
        icon.sprite = spawnedCard.icon;
    }
}
