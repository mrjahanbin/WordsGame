using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private string secretWord;
    [SerializeField] private TextAsset wordText;
    private string words;


    [Header("Setting")]
    private bool shouldReset;

    public static WordManager Instance;


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
        words = wordText.text;
    }

    // Start is called before the first frame update
    void Start()
    {
        SetSecretWord();
        GameManager.OnGameStateChanged += GameStateChangedCallback;

    }

    private void OnDestroy()
    {
        GameManager.OnGameStateChanged -= GameStateChangedCallback;

    }

    // Update is called once per frame
    void Update()
    {

    }

    public string GetSecretWord()
    {
        return secretWord.ToUpper();
    }
    public void SetSecretWord()
    {
        Debug.Log(words.Replace("\r\n", " ").Length);
        int wordCount = (words.Length + 2) / 7;
        int wordIndex = Random.Range(0, wordCount);

        int wordStartIndex = wordIndex * 7;

        secretWord = words.Substring(wordStartIndex, 5).ToUpper();
        shouldReset = false;
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
                    SetSecretWord();
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
}
