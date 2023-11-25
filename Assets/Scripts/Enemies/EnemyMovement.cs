using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 20f;
    [SerializeField] private float speedLowerBound = 10f;
    private float currentSpeed;

    private Transform target;
    private int waypointIndex = 0;

    public void Slow(float slowPercent)
    {
        currentSpeed *= (1f - slowPercent);
        if (currentSpeed < speedLowerBound)
            currentSpeed = speedLowerBound;
    }

    public void RestoreSpeed() => currentSpeed = baseSpeed;

    private void Start()
    {
        target = Waypoints.waypoints[waypointIndex];
    }

    private void FixedUpdate()
    {
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * currentSpeed * Time.fixedDeltaTime, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= 0.2f)
        {
            SetNextWaypoint();
        }

        RestoreSpeed();
    }

    private void SetNextWaypoint()
    {
        if (waypointIndex >= Waypoints.waypoints.Length - 1)
        {
            EndPath();
            return;
        }

        waypointIndex++;
        target = Waypoints.waypoints[waypointIndex];
    }

    private void EndPath()
    {
        PlayerStats.RemoveLive();
        Destroy(gameObject);
    }
}
