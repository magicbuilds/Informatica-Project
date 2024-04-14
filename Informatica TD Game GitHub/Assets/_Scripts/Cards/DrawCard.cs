using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawCard : MonoBehaviour
{
    private CardSO currentCard;

    private int amount;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private TextMeshProUGUI statsText;
    [SerializeField] private TextMeshProUGUI typeText;
    [SerializeField] private Image icon;

    public void CardInitialization(CardSO spawnedCard)
    {
        currentCard = spawnedCard;

        switch (spawnedCard.rarity)
        {
            case CardSO.Rarity.common:
                if (WaveManager.Instance.waveIndex < 25)
                {
                    amount = Random.Range(2, 4);
                }
                else amount = Random.Range(2, 5);

                nameText.color = Color.white;
                break;
            case CardSO.Rarity.uncommon:
                if (WaveManager.Instance.waveIndex < 25)
                {
                    amount = Random.Range(1, 3);
                }
                else amount = Random.Range(2, 4);

                nameText.color = Color.green;
                break;
            case CardSO.Rarity.rare:
                if (WaveManager.Instance.waveIndex < 25)
                {
                    amount = 1;
                }
                else amount = Random.Range(1, 3);

                nameText.color = Color.cyan;
                break;
            case CardSO.Rarity.epic:
                if (WaveManager.Instance.waveIndex < 25)
                {
                    amount = 1;
                }
                else amount = Random.Range(1, 3);

                nameText.color = Color.blue;
                break;
            case CardSO.Rarity.legendary:
                amount = 1;
                nameText.color = Color.yellow;
                break;
            case CardSO.Rarity.mythical:
                amount = 1;
                nameText.color = Color.black;
                break;
            default:
                Debug.Log("Rarity " + spawnedCard.rarity + " not found");
                break;
        }

        if (currentCard.cardType == CardSO.CardType.Upgrade)
        {
            amount = 1;
        }
     
        nameText.text = currentCard.cardName;
        amountText.text = amount + "X";
        icon.sprite = currentCard.icon;

        costText.text = currentCard.GetCost();
        statsText.text = currentCard.GetStats();
        typeText.text = currentCard.GetCardType();
        typeText.color = currentCard.GetCardTypeColor();
    }

    public void OnCardSelected()
    {
        InventoryManager.Instance.AddCardToInventory(currentCard, amount);
        InventoryManager.Instance.SpawnDeck();
        
        UIManager.Instance.DeactivateCardDrawUI();
        GameManager.Instance.SwitchGameState(GameManager.GameState.ChoseNextChunk);
        
    }

    public void OnExtraInformationSelected()
    {
        UIManager.Instance.ActivateExtraInformationUI(currentCard);
    }
}
