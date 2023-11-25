using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{
    public static Transform[] waypoints;
    public static Vector3 spawnPoint;

    private void Start()
    {
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
            if (i == 0) spawnPoint = waypoints[0].position;
        }
    }
}
