using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private WordContainer[] wordContainers;
    [SerializeField] private Button tryButton;
    [SerializeField] private KeyboardColorizer keyboardColorizer;




    [Header("Setting")]
    private int currentWorkContainerIndex;


    private bool canAddLetter = true;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        KeyboardKey.onKeyPressed += KeyPressCallback;
        DesableTryButton();
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


    private void Initialize()
    {
        for (int i = 0; i < wordContainers.Length; i++)
        {
            wordContainers[i].Initialize();
        }
    }

    public void WordToCheck()
    {
        string wordToCheck = wordContainers[currentWorkContainerIndex].GetWord();
        string secretWord = WordManager.Instance.GetSecretWord();

        wordContainers[currentWorkContainerIndex].Colorize(secretWord);
        keyboardColorizer.Colorize(secretWord, wordToCheck);


        if (wordToCheck == secretWord)
        {
            Debug.Log("Afarin");
        }
        else
        {
            Debug.Log("Ghalate");
            canAddLetter = true;
            DesableTryButton();

        currentWorkContainerIndex++;
        }
    }

    public void BackSpacePressedCallBack()
    {
       bool removeLetter = wordContainers[currentWorkContainerIndex].RemoveLetter();

        if (removeLetter) { 
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
}
