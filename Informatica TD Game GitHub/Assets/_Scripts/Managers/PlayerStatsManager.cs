using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;

    public float health;
    public float coins;

    private void Awake()
    {
        Instance = this; 
    }

    public void AddRemoveHealth(float healthToAdd)
    {
        health += healthToAdd;
    }

    public void AddRemoveCoins(float coinsToAdd)
    {
        coins += coinsToAdd;
    }
}
