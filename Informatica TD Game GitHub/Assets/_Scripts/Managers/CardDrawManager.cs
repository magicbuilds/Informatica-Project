using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDrawManager : MonoBehaviour
{
    public static CardDrawManager Instance;

    [SerializeField] private GameObject cardPrefab;

    [SerializeField] private List<CardSO> cards;
    private List<GameObject> spawnedCards;

    [SerializeField] private List<Transform> spawnLocations;

    [SerializeField] private Canvas canvas;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnCards()
    {
        spawnedCards = new List<GameObject>();

        foreach (Transform location in spawnLocations) 
        {
            GameObject spawnedCard = Instantiate(cardPrefab, location.position, Quaternion.identity);
            spawnedCard.GetComponent<Card>().CardInitialization(RandomCard());

            spawnedCard.transform.SetParent(canvas.transform);

            Debug.Log(spawnedCard);
            spawnedCards.Add(spawnedCard);
        }
    }

    public void RemoveCards()
    {
        foreach (GameObject card in spawnedCards)
        {
            Destroy(card);
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
