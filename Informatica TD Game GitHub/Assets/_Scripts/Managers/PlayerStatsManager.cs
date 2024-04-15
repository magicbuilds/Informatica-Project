using UnityEngine;


public class PlayerStatsManager : MonoBehaviour
{
    public static PlayerStatsManager Instance;

    public float maxHealth = 50f;
    public float health;

    public float maxCoins = 200f;
    public float coins;

    private void Awake()
    {
        Instance = this; 
    }

    private void Start()
    {
        health = maxHealth;
        UIManager.Instance.UpdateHealthUI();


        UIManager.Instance.UpdateCoinsUI();
    }

    public void AddRemoveHealth(float healthToAdd)
    {
        health += healthToAdd;

        if (health > maxHealth)
        {
            health = maxHealth;
        }
        if (health <= 0)
        {
            GameManager.Instance.SwitchGameState(GameManager.GameState.GameOver);
            
        }

        UIManager.Instance.UpdateHealthUI();
    }

    public void AddRemoveCoins(float coinsToAdd)
    {
        
        coins += coinsToAdd;

        if (coins > maxCoins)
        {

            coins = maxCoins;
        }

        UIManager.Instance.UpdateCoinsUI();
    }
    
    
    

}
