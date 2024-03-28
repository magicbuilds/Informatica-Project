using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    private Dictionary<CardSO, int> cardsInInventory = new Dictionary<CardSO, int>();
    private List<CardSO> playedCards = new List<CardSO>();  

    [SerializeField] private List<CardSO> cards;

    [SerializeField] private List<GameObject> displayCardTemplates;
    

    [SerializeField] private List<CardSO> starterCards;

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

    public void SpawnDrawCards()
    {
        UIManager.Instance.ActivateCardDrawUI();

        foreach (GameObject displayCard in displayCardTemplates)
        {
            displayCard.GetComponentInChildren<DrawCard>().CardInitialization(RandomCard());
        }
    }

    public void SpawnDeck()
    {
        foreach (GameObject deckCard in UIManager.Instance.deckCardTemplates)
        {

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
