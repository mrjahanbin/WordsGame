using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.EditorTools;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    [Header("Elements")]
    [SerializeField] private CanvasGroup menuCG;
    [SerializeField] private CanvasGroup gameCG;
    [SerializeField] private CanvasGroup levelCompleteCG;
    [SerializeField] private CanvasGroup gameOverCG;

    [Header("Menu Elements")]
    [SerializeField] private TextMeshProUGUI menuCoins;
    [SerializeField] private TextMeshProUGUI menuBestScore;


    [Header("LevelCompleteEvents")]
    [SerializeField] private TextMeshProUGUI levelCompleteCoins;
    [SerializeField] private TextMeshProUGUI levelCompleteSecretWord;
    [SerializeField] private TextMeshProUGUI levelCompleteScore;
    [SerializeField] private TextMeshProUGUI levelCompleteBestScore;

    [Header("GameOver Elements")]
    [SerializeField] private TextMeshProUGUI gameOverCoins;
    [SerializeField] private TextMeshProUGUI gameOverSecretWord;
    [SerializeField] private TextMeshProUGUI gameOverScore;
    [SerializeField] private TextMeshProUGUI gameOverBestScore;



    [Header("Game Elements")]
    [SerializeField] private TextMeshProUGUI gameCoins;
    [SerializeField] private TextMeshProUGUI gameScore;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        ShowMenu();
        HidelevelComplete();
        GameManager.OnGameStateChanged += GameStateChangedCallback;

        DataManager.onCoinsUpdated += UpdateCoinsText;

    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallback;
        DataManager.onCoinsUpdated -= UpdateCoinsText;

    }

    private void UpdateCoinsText()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        gameCoins.text = menuCoins.text;
        levelCompleteCoins.text = menuCoins.text;
        gameOverCoins.text = menuCoins.text;
    }

    private void GameStateChangedCallback(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                HideGame();
                ShowMenu();
                break;
            case GameState.Game:
                HideMenu();
                HidelevelComplete();
                HideGameOver();
                ShowGame();
                break;
            case GameState.LevelComplete:
                HideGame();
                HideGameOver();
                ShowlevelComplete();
                break;
            case GameState.GameOver:
                HideGame();
                ShowGameOver();
                break;
            case GameState.Idle:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ShowMenu()
    {
        menuCoins.text = DataManager.instance.GetCoins().ToString();
        menuBestScore.text = DataManager.instance.GetBestScore().ToString();
        ShowCG(menuCG);
    }
    private void HideMenu()
    {
        HideCG(menuCG);
    }

    private void ShowGame()
    {
        gameCoins.text =DataManager.instance.GetCoins().ToString();
        gameScore.text =DataManager.instance.GetScore().ToString();
        ShowCG(gameCG);
    }
    private void HideGame()
    {
        HideCG(gameCG);
    }

    private void ShowlevelComplete()
    {
        levelCompleteCoins.text = DataManager.instance.GetCoins().ToString();
        levelCompleteSecretWord.text = WordManager.Instance.GetSecretWord();
        levelCompleteScore.text = DataManager.instance.GetScore().ToString();
        levelCompleteBestScore.text = DataManager.instance.GetBestScore().ToString();


        ShowCG(levelCompleteCG);
    }
    private void HidelevelComplete()
    {
        HideCG(levelCompleteCG);
    }


    private void ShowGameOver()
    {
        gameOverCoins.text = DataManager.instance.GetCoins().ToString();
        gameOverSecretWord.text = WordManager.Instance.GetSecretWord();
        gameOverScore.text = DataManager.instance.GetScore().ToString();
        gameOverBestScore.text = DataManager.instance.GetBestScore().ToString();

        ShowCG(gameOverCG);
    }
    private void HideGameOver()
    {
        HideCG(gameOverCG);
    }

    private void ShowCG(CanvasGroup cg)
    {
        cg.alpha = 1;
        cg.interactable = true;
        cg.blocksRaycasts = true;
    }

    private void HideCG(CanvasGroup cg)
    {
        cg.alpha = 0;
        cg.interactable = false;
        cg.blocksRaycasts = false;
    }
}
