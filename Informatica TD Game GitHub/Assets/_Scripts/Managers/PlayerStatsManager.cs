using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;

    public float maxHealth = 50f;
    public float health;
    public float coins;

    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        UIManager.Instance.UpdateHealthUI();
    }

    public void AddRemoveHealth(float healthToAdd)
    {
        health += healthToAdd;
        UIManager.Instance.UpdateHealthUI();
    }

    public void AddRemoveCoins(float coinsToAdd)
    {
        coins += coinsToAdd;
    }
    
}
