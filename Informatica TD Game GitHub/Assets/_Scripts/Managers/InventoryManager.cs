using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [Header("Information")]
    public List<GameObject> deckCardSlots;
    [SerializeField] private List<GameObject> drawCardSlots;
    [SerializeField] private List<CardSO> starterCards;

    [Header("All Cards")]
    [SerializeField] private List<CardSO> cards;

    public DeckCard currentSelectedCard = null;

    private Dictionary<CardSO, int> cardsInInventory = new Dictionary<CardSO, int>();
    private List<CardSO> playedCards = new List<CardSO>();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (CardSO card in starterCards)
        {
            if (cardsInInventory.ContainsKey(card))
            {
                cardsInInventory[card]++;
            }
            else
            {
                cardsInInventory[card] = 1;
            }
        }

        SpawnDeck();
    }

    public void AddNewCardToInventory(CardSO card, int amount)
    {
        if (cardsInInventory.ContainsKey(card))
        {
            int totalAmount = cardsInInventory[card] + amount;
            cardsInInventory[card] = totalAmount;
        }
        else
        {
            cardsInInventory[card] = amount;
        }
    }

    public void RemoveCardFromInventory(CardSO card)
    {
        cardsInInventory[card] -= 1;
    }

    public void SpawnDeck()
    {
        int totalCardAmount = 0;
        foreach (int amount in cardsInInventory.Values)
        {
            totalCardAmount += amount;
        }

        for (int i = 0; i < deckCardSlots.Count; i++)
        {
            int cardIndex = 0;
            int prossedAmount = 0;
            int randomAmount = Random.Range(0, totalCardAmount);
            foreach (int amount in cardsInInventory.Values)
            {
                prossedAmount += amount;
                if (prossedAmount >= randomAmount)
                {
                    CardSO card = cardsInInventory.Keys.ToList()[cardIndex];

                    RemoveCardFromInventory(card);
                    totalCardAmount -= 1;

                    deckCardSlots[i].GetComponentInChildren<DeckCard>().CardInitialization(card);

                    break;
                }
                cardIndex++;
            }
        }
    }

    public void SetSelectedCard(DeckCard card)
    {
        currentSelectedCard = card;
    }

    public void SpawnDrawCards()
    {
        UIManager.Instance.ActivateCardDrawUI();

        foreach (GameObject displayCard in drawCardSlots)
        {
            displayCard.GetComponentInChildren<DrawCard>().CardInitialization(RandomCard());
        }
    }

    private CardSO RandomCard()
    {
        var totalWeight = 0;
        foreach (CardSO card in cards)
        {
            totalWeight += card.weight;
        }
        var randomWeightValue = Random.Range(1, totalWeight + 1);

        var processedWeight = 0;
        foreach (CardSO card in cards)
        {
            processedWeight += card.weight;

            if (randomWeightValue <= processedWeight)
            {
                return card;
            }
        }
        return null;
    }

}
