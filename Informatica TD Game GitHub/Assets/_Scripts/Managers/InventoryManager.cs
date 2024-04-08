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

    [SerializeField] private List<CardSO> cardsInDeck = new List<CardSO>();

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

    private void Update()
    {
        foreach (CardSO card in cardsInInventory.Keys) 
        { 
            Debug.Log("Card: " + card + ", amount" + cardsInInventory[card]);
        }
    }

    public void AddCardToInventory(CardSO card, int amount)
    {
        if (cardsInInventory.ContainsKey(card))
        {
            cardsInInventory[card] += amount;
        }
        else
        {
            cardsInInventory[card] = amount;
        }

    }

    public void RemoveCardFromInventory(CardSO card)
    {
        cardsInInventory[card] -= 1;
        if (cardsInInventory[card] <= 0)
        {
            cardsInInventory.Remove(card);
        }
    }

    public void SpawnDeck()
    {
        UIManager.Instance.HideDeckCards();

        if (cardsInDeck.Count > 0)
        {
            foreach (CardSO card in cardsInDeck)
            {
                AddCardToInventory(card, 1);
            }
            cardsInDeck.Clear();
        }

        int totalCardAmount = 0;
        foreach (int amount in cardsInInventory.Values)
        {
            totalCardAmount += amount;
        }

        int cardsToSpawn = deckCardSlots.Count;
        if (totalCardAmount < cardsToSpawn)
        {
            cardsToSpawn = totalCardAmount;
        }

        List<int> indexesWithCards = new List<int>();

        for (int i = 0; i < cardsToSpawn; i++)
        {
            indexesWithCards.Add(i);

            int cardIndex = 0;
            int prossedAmount = 0;
            int randomAmount = Random.Range(0, totalCardAmount);
            foreach (int amount in cardsInInventory.Values)
            {
                prossedAmount += amount;
                if (prossedAmount >= randomAmount)
                {
                    CardSO card = cardsInInventory.Keys.ToList()[cardIndex];
                    cardsInDeck.Add(card);

                    RemoveCardFromInventory(card);
                    totalCardAmount -= 1;

                    deckCardSlots[i].GetComponentInChildren<DeckCard>().CardInitialization(card);

                    break;
                }
                cardIndex++;
            }
        }

        UIManager.Instance.ShowDeckCards(indexesWithCards);
    }

    public void SetSelectedCard(DeckCard card)
    {
        currentSelectedCard = card;
    }

    public void OnTowerPlaced()
    {
        cardsInDeck.Remove(currentSelectedCard.currentCard);

        currentSelectedCard.transform.parent.parent.gameObject.SetActive(false);
        currentSelectedCard = null;
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
