using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomWalk
{
    private int gridWidth;
    private int gridHeight;
    private int minPathSize;

    private int maxAttempts;

    private HashSet<Vector2Int> path;
    private Vector2Int startPoint;
    private Vector2Int endPoint;

    public RandomWalk(int gridWidth, int gridHeight, int minPathSize)
    {
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;
        this.minPathSize = minPathSize;

        this.maxAttempts = 5000;
        this.path = new HashSet<Vector2Int>();
    }

    public HashSet<Vector2Int> GetPath()
    {
        int currentAttempt = 0;
        GeneratePath();
        while (path.Count < minPathSize)
        {
            if (currentAttempt < maxAttempts)
            {
                path.Clear();
                GeneratePath();
                currentAttempt++;
            }
        }
        return path;
    }

    public Vector2Int GetStartPoint() => startPoint;
    public Vector2Int GetEndPoint() => endPoint;

    private void GeneratePath()
    {
        int currPathSize = 1;
        int randomChoice = 0;
        // Start in upper left corner.
        Vector2Int currentPoint = new Vector2Int(2, gridHeight - 2);
        // Save first point as a start point.
        startPoint = currentPoint;
        path.Add(currentPoint);

        List<Vector2Int> nextPossibleNodes = GetNextPossibleNodes(currentPoint.x, currentPoint.y);
        while (nextPossibleNodes.Count > 0)
        {
            if (path.Count > minPathSize) break;
            randomChoice = Random.Range(0, nextPossibleNodes.Count);
            currentPoint = nextPossibleNodes[randomChoice];
            currPathSize++;
            path.Add(currentPoint);
            nextPossibleNodes = GetNextPossibleNodes(currentPoint.x, currentPoint.y);
        }
        // Save last point as an end point.
        endPoint = currentPoint;

    }

    private List<Vector2Int> GetNextPossibleNodes(int x, int y)
    {
        List<Vector2Int> nextPossibleNodes = new List<Vector2Int>();
        if (NodeIsValid(x - 1, y)) nextPossibleNodes.Add(new Vector2Int(x - 1, y));
        if (NodeIsValid(x, y + 1)) nextPossibleNodes.Add(new Vector2Int(x, y + 1));
        if (NodeIsValid(x + 1, y)) nextPossibleNodes.Add(new Vector2Int(x + 1, y));
        if (NodeIsValid(x, y - 1)) nextPossibleNodes.Add(new Vector2Int(x, y - 1));

        return nextPossibleNodes;
    }

    private bool NodeIsValid(int x, int y)
    {
        // Node is not taken already
        bool isNotTaken = NodeIsEmpty(x, y);
        // Node is in borders
        bool isInBorders = (x > 1) && (x < gridWidth - 1) && (y > 1) && (y < gridHeight - 1);
        // Node has only one neighbour (previous node)
        bool isOneNeighbour = GetNodeNeighboursNumber(x, y) == 1;

        return isNotTaken && isInBorders && isOneNeighbour;
    }

    private bool NodeIsEmpty(int x, int y) => !path.Contains(new Vector2Int(x, y));

    private bool NodeIsTaken(int x, int y) => path.Contains(new Vector2Int(x, y));

    public int GetNodeNeighboursNumber(int x, int y)
    {
        int neighbourValue = 0;

        if (NodeIsTaken(x - 1, y)) neighbourValue += 1;
        if (NodeIsTaken(x, y + 1)) neighbourValue += 1;
        if (NodeIsTaken(x + 1, y)) neighbourValue += 1;
        if (NodeIsTaken(x, y - 1)) neighbourValue += 1;

        return neighbourValue;
    }
}
