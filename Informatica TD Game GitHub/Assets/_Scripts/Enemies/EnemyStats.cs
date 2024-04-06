using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStats : MonoBehaviour
{
    public EnemySO currentEnemy;
    public EnemyAI enemyAI;

    [SerializeField] private float health;

    [SerializeField] private TextMeshPro healthText;

    [SerializeField] private SpriteRenderer currentlyHoldingSprite;

    private bool isDead = false;


    private void Start()
    {
        health = currentEnemy.baseHealth;
        UpdateEnemyUI();

        if (currentEnemy.showHoldingEnemy)
        {
            currentlyHoldingSprite.sprite = currentEnemy.holdingEnemies[Random.Range(0, currentEnemy.holdingEnemies.Count)].xSprite;
        }
        
    }

    public void DealDamange(float damage)
    {
        health -= damage;

        if (health <= 0 && !isDead)
        {
            isDead = true;

            if (currentEnemy.holdingEnemies.Count != 0) SummonHoldingEnemies();   
            
            EnemyManager.Instance.ReduceEnemyCount();
            PlayerStatsManager.Instance.AddRemoveCoins(currentEnemy.baseCoins);

            AudioManager.Instance.PlaySFX(AudioManager.Instance.death);
            Destroy(gameObject);
        }

        UpdateEnemyUI();
    }

    private void UpdateEnemyUI()
    {
        if (health < 0)
        {
            healthText.text = 0 + " HP";
            return;
        }
        healthText.text = health + " HP";
    }
    private void SummonHoldingEnemies()
    {
        int index = 0;
        foreach (int amount in currentEnemy.correspondingAmounts)
        {
            for (int i = 0; i < amount; i++)
            {
                EnemyManager.Instance.SpawnEnemy(currentEnemy.holdingEnemies[index], transform.position, enemyAI.target);
            }
            index++;
        }

    }
}
