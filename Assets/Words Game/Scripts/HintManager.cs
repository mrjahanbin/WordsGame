using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HintManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private GameObject keyboard;
    private KeyboardKey[] keys;

    [Header("Setting")]
    [SerializeField] private int keyboardHintPrice;
    [SerializeField] private int letterHintPrice;
    private bool shouldReset;
    
    [Header("Text Elements")]
    [SerializeField] private TextMeshProUGUI keyboardPriceText;
    [SerializeField] private TextMeshProUGUI letterPriceText;

    private void Awake()
    {
        keys = keyboard.GetComponentsInChildren<KeyboardKey>();

    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.OnGameStateChanged += GameStateChangedCallback;
        keyboardPriceText.text = keyboardHintPrice.ToString();
        letterPriceText.text = letterHintPrice.ToString();

    }
    public void OnDestroy()
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
                    letterHintGive.Clear();
                    shouldReset = false;
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

    // Update is called once per frame
    void Update()
    {

    }

    public void KeyboardHint()
    {

        if (DataManager.instance.GetCoins()<keyboardHintPrice)
        {
            return;
        }

        string secretWord = WordManager.Instance.GetSecretWord();
        List<KeyboardKey> untouchedKeys = new();

        for (int i = 0; i < keys.Length; i++)
        {
            if (keys[i].IsUntouchedKeys())
            {
                untouchedKeys.Add(keys[i]);
            }
        }

        List<KeyboardKey> tempuntouchedKeys = new(untouchedKeys);
        for (int i = 0; i < untouchedKeys.Count; i++)
        {
            if (secretWord.Contains(untouchedKeys[i].GetLetter()))
            {
                tempuntouchedKeys.Remove(untouchedKeys[i]);
            }
        }

        if (tempuntouchedKeys.Count <= 0)
        {
            return;
        }

        int randomKey = Random.Range(0, tempuntouchedKeys.Count);
        tempuntouchedKeys[randomKey].SetInValid();


        DataManager.instance.RemoveCoins(keyboardHintPrice);

    }

    List<int> letterHintGive = new();

    public void LetterHint()
    {

        if (DataManager.instance.GetCoins()<letterHintPrice)
        {
            return;
        }

        if (letterHintGive.Count >= 5)
        {
            return;
        }

        List<int> letterHintNotGive = new();

        for (int i = 0; i < 5; i++)
        {
            if (!letterHintGive.Contains(i))
            {
                letterHintNotGive.Add(i);
            }
        }

        WordContainer currentWordContainer = InputManager.instance.GetCurrentWordContainer();
        string secretWord = WordManager.Instance.GetSecretWord();


        int randomIndex = letterHintNotGive[Random.Range(0, letterHintNotGive.Count)];
        letterHintGive.Add(randomIndex);

        currentWordContainer.AddAsHint(randomIndex, secretWord[randomIndex]);

        DataManager.instance.RemoveCoins(letterHintPrice);


    }
}
