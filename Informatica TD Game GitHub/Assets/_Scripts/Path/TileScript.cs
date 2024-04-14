using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hoverColor;

    [SerializeField] private GameObject rangeObject;
    
    private Tower tower;
    private CardSO currentCard;
    private Color startColor;

    private void Start()
    {
        startColor = spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        if (InventoryManager.Instance.currentSelectedCard != null)
        {
            if (InventoryManager.Instance.currentSelectedCard.currentCard.cardType == CardSO.CardType.Tower)
            {
                TowerSO selectedTower = InventoryManager.Instance.currentSelectedCard.currentCard.tower;

                float diameter = selectedTower.baseRange * 2;
                rangeObject.transform.localScale = new Vector3(diameter, diameter, 1);
                rangeObject.SetActive(true);
            }

        }

        spriteRenderer.color = hoverColor;
    }

    private void OnMouseExit()
    {
        rangeObject.SetActive(false);

        spriteRenderer.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.Instance.IsHoveringUI()) return;
        
        if (tower != null)
        {
            
            UIManager.Instance.ActivateTowerInformationUI(tower);
            
            UpdateRange();
        }
        else
        {
            if (InventoryManager.Instance.currentSelectedCard != null)
            {
                if (InventoryManager.Instance.currentSelectedCard.currentCard.cardType == CardSO.CardType.Tower)
                {
                    PlayerStatsManager.Instance.AddRemoveCoins(-InventoryManager.Instance.currentSelectedCard.currentCard.baseCost);
                    SpawnTower();
                    InventoryManager.Instance.OnCardPlayed();
                }
            }
            
            UIManager.Instance.DeactivateTowerInformationUI();
            
        }
    }

    public void UpdateRange()
    {
        float diameter = tower.currentRange * 2;
        rangeObject.transform.localScale = new Vector3(diameter, diameter, 1);
        rangeObject.SetActive(true);
    }

    private void SpawnTower()
    {
        GameObject towerToSpawn = InventoryManager.Instance.currentSelectedCard.currentCard.tower.towerPrefab;
        GameObject spawnedTower = Instantiate(towerToSpawn, transform.position, Quaternion.identity);
        tower = spawnedTower.GetComponent<Tower>();
        tower.towerCard = InventoryManager.Instance.currentSelectedCard.currentCard;
        tower.tile = this;

        UpgradeManager.Instance.placedTowers.Add(tower);
    }
}
