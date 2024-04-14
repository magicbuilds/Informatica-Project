using System;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyStats : MonoBehaviour
{
    public EnemySO currentEnemy;
    public EnemyAI enemyAI;

    public float health;

    [SerializeField] private TextMeshPro healthText;

    [SerializeField] private SpriteRenderer currentlyHoldingSprite;

    public bool isDead = false;
    public bool isTarget = false;

    public float currentSpeed;

    private float timeSlowed = 0;
    private bool isSlowed = false;

    private void Start()
    {
        currentSpeed = currentEnemy.speed;

        health = currentEnemy.baseHealth;
        UpdateEnemyUI();

        if (currentEnemy.showHoldingEnemy)
        {
            currentlyHoldingSprite.sprite = currentEnemy.holdingEnemies[Random.Range(0, currentEnemy.holdingEnemies.Count)].xSprite;
        }
    }

    private void Update()
    {
        if (isSlowed)
        {
            timeSlowed += Time.deltaTime;

            if (timeSlowed >= 0.5f)
            {
                currentSpeed = currentEnemy.speed;

                isSlowed = false;
                timeSlowed = 0;
            }
        }
    }

    public void DealDamange(float damage)
    {
        health -= damage;
        

        if (health <= 0 && !isDead)
        {
            isDead = true;

            if (currentEnemy.holdingEnemies.Count != 0) SummonHoldingEnemies();   
            
            EnemyManager.Instance.ReduceEnemyCount(this);
            PlayerStatsManager.Instance.AddRemoveCoins(currentEnemy.baseCoins);

            AudioManager.Instance.PlayEnemiesDeath(AudioManager.Instance.death);
            Destroy(gameObject);
        }

        UpdateEnemyUI();
    }

    public void SlowEnemy(float slowPercentage)
    {
        currentSpeed = currentEnemy.speed * (100 - slowPercentage) / 100;

        isSlowed = true;
        timeSlowed = 0;
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
