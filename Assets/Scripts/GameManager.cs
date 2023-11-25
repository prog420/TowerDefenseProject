using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static bool GameIsOver;

    public GameObject gameOverUI;

    private void Start()
    {
        GameIsOver = false;
    }

    private void Update()
    {
        if (GameIsOver) return;

        if (PlayerStats.Lives <= 0) EndGame();

        if (Input.GetKeyDown("e"))
        {
            EndGame();
        }
    }
    public void EndGame()
    {
        GameIsOver = true;

        gameOverUI.SetActive(true);
        Debug.Log("Game Over");
    }
}
