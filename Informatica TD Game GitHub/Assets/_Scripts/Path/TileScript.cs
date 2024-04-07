using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileScript : MonoBehaviour
{
    [Header("References")] 
    [SerializeField] private SpriteRenderer sr;

    [SerializeField] private Color hoverColor;
    
    private GameObject towerObj;
    public Tower tower;
    private Color startColor;

    private void Start()
    {
        startColor = sr.color;
    }

    private void OnMouseEnter()
    {
        sr.color = hoverColor;
    }

    private void OnMouseExit()
    {
        sr.color = startColor;
    }

    private void OnMouseDown()
    {
        if (UIManager.Instance.IsHoveringUI()) return;
            
        if (towerObj != null)
        {
            tower.OpenUpgradeUI();
            return;
        }

        GameObject towerToBuild = BuildManager.Instance.GetSelectedTower();
        towerObj = Instantiate(towerToBuild, transform.position, Quaternion.identity);
        tower = towerObj.GetComponent<Tower>();
    }
}
