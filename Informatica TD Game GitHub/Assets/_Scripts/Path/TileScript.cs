using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color hoverColor;

    [SerializeField] private GameObject rangeObject;
    
    private Tower tower;
    private Color startColor;

    private void Start()
    {
        startColor = spriteRenderer.color;
    }

    private void OnMouseEnter()
    {
        if (InventoryManager.Instance.currentSelectedCard != null)
        {
            TowerSO selectedTower = InventoryManager.Instance.currentSelectedCard.currentCard.tower;

            float diameter = selectedTower.baseRange * 2;
            rangeObject.transform.localScale = new Vector3(diameter, diameter, 1);
            rangeObject.SetActive(true);
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
        if (tower != null)
        {
            float diameter = tower.currentTower.baseRange * 2;
            rangeObject.transform.localScale = new Vector3(diameter, diameter, 1);
            rangeObject.SetActive(true);
        }
        else
        {
            if (InventoryManager.Instance.currentSelectedCard != null)
            {
                SpawnTower();
                InventoryManager.Instance.RemoveCardFromInventory(InventoryManager.Instance.currentSelectedCard.currentCard);
                InventoryManager.Instance.currentSelectedCard.transform.parent.gameObject.SetActive(false);
                InventoryManager.Instance.currentSelectedCard = null;
            }
        }
    }

    private void SpawnTower()
    {
        GameObject towerToSpawn = BuildManager.Instance.GetSelectedTower();
        GameObject spawnedTower = Instantiate(towerToSpawn, transform.position, Quaternion.identity);
        tower = spawnedTower.GetComponent<Tower>();
        tower.currentTower = InventoryManager.Instance.currentSelectedCard.currentCard.tower;
    }
}
