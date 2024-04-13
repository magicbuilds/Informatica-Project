using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawCard : MonoBehaviour
{
    private CardSO currentCard;

    private int amount;

    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI amountText;
    [SerializeField] private TextMeshProUGUI cardStatsText;
    [SerializeField] private Image icon;

    public void CardInitialization(CardSO spawnedCard)
    {
        currentCard = spawnedCard;

        switch (spawnedCard.rarity)
        {
            case CardSO.Rarity.common:
                amount = Random.Range(2, 6);
                nameText.color = Color.white;
                break;
            case CardSO.Rarity.uncommon:
                amount = Random.Range(1, 5);
                nameText.color = Color.green;
                break;
            case CardSO.Rarity.rare:
                amount = Random.Range(1, 4);
                nameText.color = Color.cyan;
                break;
            case CardSO.Rarity.epic:
                amount = Random.Range(1, 3);
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
     
        nameText.text = currentCard.cardName;
        amountText.text = amount + "X";
        icon.sprite = currentCard.icon;

        Debug.Log(currentCard);
        cardStatsText.text = currentCard.GetStats();
        
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
