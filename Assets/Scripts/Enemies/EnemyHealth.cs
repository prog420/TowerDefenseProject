using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private float health = 100;
    [SerializeField] private GameObject deathEffect;

    private EnemyValue enemyValue;

    private void Start()
    {
        enemyValue = GetComponent<EnemyValue>();
    }

    public void TakeDamage(float amount)
    {
        health -= amount;

        if (health <= 0)
        {
            Die();
        }
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
