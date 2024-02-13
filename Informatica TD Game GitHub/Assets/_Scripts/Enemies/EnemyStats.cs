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
        UpdateUI();
    }

    public void DealDamange(float damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Destroy(gameObject);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (health < 0)
        {
            healthText.text = 0 + " HP";
            return;
        }
        healthText.text = health + " HP";
    }
}
