using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    [Header("Data")]
    private int coins;
    private int score;
    private int bestScore;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(instance);
        }

        LoadData();

    }



    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void AddCoins(int amount)
    {
        coins += amount;
        SaveData();
    }
    public void RemoveCoins(int amount)
    {
        coins -= amount;
        coins = Mathf.Max(coins, 0);
        SaveData();
    }

    public void InceaseScore(int amount)
    {
        score = amount;
        if (score > bestScore)
        {
            bestScore = score;
        }
        SaveData();
    }


    public int GetCoins()
    {
        return coins;
    }

    public int GetScore()
    {
        return score;
    }
    public int GetBestScore()
    {
        return bestScore;
    }



    private void LoadData()
    {
        coins = PlayerPrefs.GetInt("coins", 100);
        score = PlayerPrefs.GetInt("score", 100);
        bestScore = PlayerPrefs.GetInt("bestScore", 100);
    }

    private void SaveData()
    {
        PlayerPrefs.GetInt("coins", coins);
        PlayerPrefs.GetInt("score", score);
        PlayerPrefs.GetInt("bestScore", bestScore);
    }
}
