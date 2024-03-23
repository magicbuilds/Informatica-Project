using TMPro;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    
    public EnemySO currentEnemy;

    [SerializeField] private float health;

    [SerializeField] private TextMeshPro healthText;

    private void Start()
    {
        health = currentEnemy.baseHealth;
        UpdateEnemyUI();
    }

    public void DealDamange(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            EnemyManager.Instance.ReduceEnemyCount();
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
}
