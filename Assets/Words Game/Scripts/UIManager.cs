using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public static UIManager Instance;

    [Header("Elements")]
    [SerializeField] private CanvasGroup gameCG;
    [SerializeField] private CanvasGroup levelCompleteCG;

    [Header("LevelCompleteEvents")]
    [SerializeField] private TextMeshProUGUI levelCompleteCoins;
    [SerializeField] private TextMeshProUGUI levelCompleteSecretWord;
    [SerializeField] private TextMeshProUGUI levelCompleteScore;
    [SerializeField] private TextMeshProUGUI levelCompleteBestScore;


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
        ShowGame();
        HidelevelCompleteCG();
        GameManager.OnGameStateChanged += GameStateChangedCallback;

    }

    private void GameStateChangedCallback(GameState state)
    {
        switch (state)
        {
            case GameState.Menu:
                break;
            case GameState.Game:
                break;
            case GameState.LevelComplete:
                ShowlevelCompleteCG();
                HideGame();
                break;
            case GameState.GameOver:
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

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallback;

    }

    private void ShowGame()
    {
        ShowCG(gameCG);
    }
    private void HideGame()
    {
        HideCG(gameCG);
    }

    private void ShowlevelCompleteCG()
    {
        levelCompleteCoins.text = DataManager.instance.GetCoins().ToString();
        levelCompleteSecretWord.text = WordManager.Instance.GetSecretWord();
        levelCompleteScore.text = DataManager.instance.GetScore().ToString();
        levelCompleteBestScore.text = DataManager.instance.GetBestScore().ToString();


        ShowCG(levelCompleteCG);
    }
    private void HidelevelCompleteCG()
    {
        HideCG(levelCompleteCG);
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
