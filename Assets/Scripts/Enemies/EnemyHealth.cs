using UnityEngine;
using UnityEngine.UI;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float maxHealth = 100;
    private float health;

    [SerializeField] private Image HPBar;
    [SerializeField] private GameObject deathEffect;

    private EnemyValue enemyValue;

    private void Start()
    {
        enemyValue = GetComponent<EnemyValue>();
        health = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        UpdateHPBar();

        if (health <= 0)
        {
            Die();
        }
    }

    private void UpdateHPBar()
    {
        HPBar.fillAmount = health / maxHealth;
    }

    private void Die()
    {
        if (enemyValue!= null)
            PlayerStats.AddMoney(enemyValue.Value);

        GameObject deathEffectInstance = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(deathEffectInstance, 3f);
        Destroy(gameObject);
    }
}
