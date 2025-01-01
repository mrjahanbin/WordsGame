using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardColorizer : MonoBehaviour
{

    [Header("Elements")]
    private KeyboardKey[] keys;

    [Header("Setting")]
    private bool shouldReset;

    private void Awake()
    {
        keys = GetComponentsInChildren<KeyboardKey>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChanged += GameStateChangedCallback;
    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
    }

    private void GameStateChangedCallback(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                break;
            case GameState.Game:
                if (shouldReset)
                {
                    Initialize();
                }
                break;
            case GameState.LevelComplete:
                shouldReset = true;
                break;
            case GameState.GameOver:
                shouldReset = true;
                break;
            case GameState.Idle:
                break;
            default:
                break;
        }
    }

    public void Initialize()
    {
        for (int i = 0; i < keys.Length; i++)
        {
            keys[i].Initialize();
        }

        shouldReset = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Colorize(string secretWord, string wordToCheck)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            char keyLetter = keys[i].GetLetter();
            for (int j = 0; j < wordToCheck.Length; j++)
            {
                if (keyLetter != wordToCheck[j])
                    continue;

                if (keyLetter == secretWord[j])
                {
                    keys[i].SetValid();
                }
                else if (secretWord.Contains(keyLetter))
                {
                    keys[i].SetPotential();
                }
                else
                {
                    keys[i].SetInValid();
                }

            }
        }
    }
}
