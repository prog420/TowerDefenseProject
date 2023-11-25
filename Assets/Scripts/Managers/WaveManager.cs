using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WaveManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_Text waveCountdownText;

    [Header("Enemy Prefabs")]
    public Transform enemyParent;
    public Transform enemyPrefab;

    [Header("Wave Parameters")]
    [SerializeField] float timeBetweenWaves = 5f;
    [SerializeField] float timeBetweenEnemies = 0.5f;
    private float countdown = 2f;

    private int waveNumber = 0;

    private void Start()
    {

    }

    private void FixedUpdate()
    {
        if (countdown <= 0f)
        {
            StartCoroutine(SpawnWave());
            countdown = timeBetweenWaves;
        }

        countdown -= Time.fixedDeltaTime;
        countdown = Mathf.Clamp(countdown, 0f, Mathf.Infinity);

        waveCountdownText.text = string.Format("{0:00.00}", countdown);
    }

    private IEnumerator SpawnWave()
    {
        waveNumber++;
        
        PlayerStats.Rounds++;
        timeBetweenWaves += timeBetweenEnemies;
        
        for (int i = 0; i < waveNumber; i++)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(timeBetweenEnemies);
        }
    }

    private void SpawnEnemy()
    {
        if (enemyPrefab != null)
        {
            Instantiate(enemyPrefab, Waypoints.spawnPoint, Quaternion.identity, enemyParent);
        }
    }
}
