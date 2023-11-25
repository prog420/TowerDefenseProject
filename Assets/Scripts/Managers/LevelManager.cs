using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    // Camera: x 35 y 60 z - 10 for 16x16 grid
    private HashSet<Vector2Int> pathNodes;
    private Vector2Int startPoint;
    private Vector2Int endPoint;

    [Header("Prefabs")]
    [SerializeField] private GameObject pathNodePrefab;
    [SerializeField] private GameObject landNodePrefab;
    [SerializeField] private GameObject waypointPrefab;
    [SerializeField] private GameObject startPointPrefab;
    [SerializeField] private GameObject endPointPrefab;

    [Header("Parents")]
    private GameObject pathNodesParent;
    private GameObject landNodesParent;
    private GameObject waypointsParent;

    [Header("Grid Parameters")]
    [SerializeField] private int minPathSize = 50;
    [SerializeField] private int mapWidth = 32;
    [SerializeField] private int mapHeight = 32;
    [SerializeField] private float nodeOffset = 0.5f;

    private float nodeWidth;
    private float nodeHeight;

    private void Awake()
    {
        GenerateLevel();
    }

    public void GenerateLevel()
    {
        RandomWalk randomWalk = new RandomWalk(mapWidth, mapHeight, minPathSize);

        nodeWidth = pathNodePrefab.transform.localScale.x;
        nodeHeight = pathNodePrefab.transform.localScale.z;

        Clear();

        pathNodesParent = new GameObject("Path");
        landNodesParent = new GameObject("Land");
        waypointsParent = new GameObject("Waypoints");
        waypointsParent.AddComponent<Waypoints>();

        pathNodes = randomWalk.GetPath();
        startPoint = randomWalk.GetStartPoint();
        endPoint = randomWalk.GetEndPoint();

        SetLandNodes();
        SetPathNodes();
        SetWayPoints();
        SetStartAndEndPoints();
    }

    private void SetLandNodes()
    {
        // Vector3[,] nodePositions = GetNodePositions();
        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapHeight; y++)
            {
                if (!pathNodes.Contains(new Vector2Int(x, y)))
                {
                    Vector3 posWithOffset = new Vector3(x * (nodeWidth + nodeOffset), 0, y * (nodeHeight + nodeOffset));
                    Instantiate(landNodePrefab, posWithOffset, Quaternion.identity, landNodesParent.transform);
                }
            }
        }
    }

    private void SetPathNodes()
    {
        Vector3 prev = Vector3.zero;

        foreach (Vector2Int node in pathNodes)
        {
            Vector3 posWithOffset = new Vector3(node.x * (nodeWidth + nodeOffset), 0, node.y * (nodeHeight + nodeOffset));
            Instantiate(pathNodePrefab, posWithOffset, Quaternion.identity, pathNodesParent.transform);

            if (prev == Vector3.zero)
            {
                prev = posWithOffset;
            }
            else
            {
                Vector3 posBetween = 0.5f * (posWithOffset - prev) + prev;
                Instantiate(pathNodePrefab, posBetween, Quaternion.identity, pathNodesParent.transform);
                prev = posWithOffset;
            }
        }
    }

    private void SetWayPoints(float height = 2.5f)
    {
        foreach (Vector2Int node in pathNodes)
        {
            Vector3 posWithOffset = new Vector3(node.x * (nodeWidth + nodeOffset), height, node.y * (nodeHeight + nodeOffset));
            Instantiate(waypointPrefab, posWithOffset, Quaternion.identity, waypointsParent.transform);
        }
    }

    private void SetStartAndEndPoints()
    {
        Vector3 startWithOffset = new Vector3(startPoint.x * (nodeWidth + nodeOffset), 2.5f, startPoint.y * (nodeHeight + nodeOffset));
        Instantiate(startPointPrefab, startWithOffset, Quaternion.identity, pathNodesParent.transform);

        Vector3 endWithOffset = new Vector3(endPoint.x * (nodeWidth + nodeOffset), 2.5f, endPoint.y * (nodeHeight + nodeOffset));
        Instantiate(endPointPrefab, endWithOffset, Quaternion.identity, pathNodesParent.transform);
    }

    private void Clear()
    {
        DestroyImmediate(landNodesParent);
        DestroyImmediate(pathNodesParent);
        DestroyImmediate(waypointsParent);
    }
}
