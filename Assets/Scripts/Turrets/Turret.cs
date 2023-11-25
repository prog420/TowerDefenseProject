using UnityEngine;

public class Turret : MonoBehaviour
{
    private Transform enemy;
    private EnemyHealth enemyHealth;
    private EnemyMovement enemyMovement;

    [Header("General Attributes")]
    [SerializeField] private float range = 15f;
    [SerializeField] private float turnSpeed = 10f;

    [Header("Bullet Turret Attributes")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float fireRate = 0.5f;
    private float fireCountdown = 0f;

    [Header("Laser Turret Attributes")]
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] ParticleSystem laserImpactEffect;
    [SerializeField] private bool useLaser = false;
    [SerializeField] private int damageOverTime = 20;
    [SerializeField] private float slowPercent = 0.5f;

    [Header("Unity Setup Fields")]
    [SerializeField] private Transform rotator;
    [SerializeField] private Transform firePoint;

    private string enemyTag = "Enemy";

    void Start()
    {
        InvokeRepeating("FindClosestTarget", 0f, 0.5f);
    }


    void FixedUpdate()
    {
        TrackTarget();

        if (useLaser)
        {
            Laser();
        }
        else
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (enemy != null)
        {
            if (fireCountdown <= 0f)
            {
                fireCountdown = 1f / fireRate;
                GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                Bullet bullet = bulletGO.GetComponent<Bullet>();

                if (bullet != null )
                {
                    bullet.Seek(enemy);
                }
            }
            fireCountdown -= Time.fixedDeltaTime;
        }
    }

    private void Laser()
    {
        if (enemyHealth != null)
        {
            enemyHealth.TakeDamage(damageOverTime * Time.fixedDeltaTime);
            enemyMovement.Slow(slowPercent);
        }

        if (enemy != null)
        {
            lineRenderer.SetPosition(0, firePoint.position);
            lineRenderer.SetPosition(1, enemy.position);

            Vector3 dir = firePoint.position - enemy.position;

            laserImpactEffect.transform.rotation = Quaternion.LookRotation(dir);

            laserImpactEffect.transform.position = enemy.position + dir.normalized;

            if (!lineRenderer.enabled)
            {
                lineRenderer.enabled = true;
                laserImpactEffect.Play();
            }
        }
    }

    private void TrackTarget()
    {
        if (enemy == null)
        {
            if (useLaser && lineRenderer.enabled)
            {
                lineRenderer.enabled = false;
                laserImpactEffect.Stop();
            }
            return;
        }

        Vector3 dir = enemy.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotator.rotation, lookRotation, Time.fixedDeltaTime * turnSpeed).eulerAngles;
        rotator.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    /// <summary>
    /// Find closest target and retrieve its components.
    /// </summary>
    private void FindClosestTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
            else
            {
                this.enemy = null;
            }
        }
        // Get closest enemy and its components
        if (nearestEnemy != null && shortestDistance <= range)
        {
            enemy = nearestEnemy.transform;
            enemyHealth = enemy.GetComponent<EnemyHealth>();
            enemyMovement = enemy.GetComponent<EnemyMovement>();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
