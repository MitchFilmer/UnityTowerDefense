using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public static int money;
    public int startingMoney = 1000;

    public static int lives;
    public int startingLives = 20;

    public void Start()
    {
        money = startingMoney;
        lives = startingLives;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            money += 500;
        }
    }
}
