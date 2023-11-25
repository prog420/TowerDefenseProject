using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public static int Money { get; private set; }
    [SerializeField] private int _startMoney = 400;

    public static int Lives { get; private set; }
    [SerializeField] private int _startLives = 20;

    public static int Rounds { get; set; }

    private void Start()
    {
        Money = _startMoney;
        Lives = _startLives;

        Rounds = 0;
    }

    public static void AddMoney(int money)
    {
        Money += money;
    }

    public static void RemoveMoney(int money)
    {
        Money -= money;
        if (Money < 0)
        {
            Money = 0;
        }
    }

    public static void RemoveLive(int live = 1)
    {
        Lives -= live;
    }
}
