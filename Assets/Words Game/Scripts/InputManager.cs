using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    public static InputManager instance;

    [Header("Elements")]
    [SerializeField] public WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private KeyboardColorizer keyboardColorizer;

    [Header("Setting")]
    public int currentWorkContainerIndex;
    public bool shouldReset;


    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(instance);
    }


    private bool canAddLetter = true;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        KeyboardKey.onKeyPressed += KeyPressCallback;
        DesableTryButton();
        GameManager.OnGameStateChanged += GameStateChangedCallback;

    }

    private void KeyPressCallback(char letter)
    {

        if (!canAddLetter)
        {
            return;
        }


        wordContainers[currentWorkContainerIndex].Add(letter);
        if (wordContainers[currentWorkContainerIndex].IsComplete())
        {
            canAddLetter = false;
            EnableTryButton();
            //currentWorkContainerIndex++;
            //WordToCheck();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnDestroy()
    {
        KeyboardKey.onKeyPressed -= KeyPressCallback;
        GameManager.OnGameStateChanged += GameStateChangedCallback;


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

    private void Initialize()
    {

        currentWorkContainerIndex = 0;
        canAddLetter = true;
        DesableTryButton();

        for (int i = 0; i < wordContainers.Length; i++)
        {
            wordContainers[i].Initialize();
        }
        shouldReset = false;
    }

    public void WordToCheck()
    {
        string wordToCheck = wordContainers[currentWorkContainerIndex].GetWord();
        string secretWord = WordManager.Instance.GetSecretWord();

        wordContainers[currentWorkContainerIndex].Colorize(secretWord);
        keyboardColorizer.Colorize(secretWord, wordToCheck);



        if (wordToCheck == secretWord)
        {
            SetLevelComplete();
        }
        else
        {
            //Debug.Log("Ghalate");
            currentWorkContainerIndex++;
            DesableTryButton();

            if (currentWorkContainerIndex >= wordContainers.Length)
            {
                //Debug.Log("GameOver");
                GameManager.Instance.SetGameState(GameState.GameOver);
                DataManager.instance.ResetScore();
            }
            else
            {
                canAddLetter = true;
            }

        }
    }

    private void SetLevelComplete()
    {
        UpdateData();
        GameManager.Instance.SetGameState(GameState.LevelComplete);
    }

    private void UpdateData()
    {
        int scoreToAdd = 6 - currentWorkContainerIndex;
        DataManager.instance.InceaseScore(scoreToAdd);
        DataManager.instance.AddCoins(scoreToAdd * 2);
    }

    public void BackSpacePressedCallBack()
    {
        bool removeLetter = wordContainers[currentWorkContainerIndex].RemoveLetter();

        if (removeLetter)
        {
            DesableTryButton();
        }
        canAddLetter = true;
    }
    public void EnableTryButton()
    {
        tryButton.interactable = true;
    }
    public void DesableTryButton()
    {
        tryButton.interactable = false;
    }

    public WordContainer GetCurrentWordContainer()
    {
        return wordContainers[currentWorkContainerIndex];
    }
}
