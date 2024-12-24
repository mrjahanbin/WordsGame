using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private WordContainer[] wordContainers;



    [Header("Setting")]
    private int currentWorkContainerIndex;


    private bool canAddLetter = true;


    // Start is called before the first frame update
    void Start()
    {
        Initialize();
        KeyboardKey.onKeyPressed += KeyPressCallback;
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

        if (wordToCheck == secretWord)
        {
            Debug.Log("Afarin");
        }
        else
        {
            Debug.Log("Ghalate");
            canAddLetter = true;

        currentWorkContainerIndex++;
        }
    }

    public void BackSpacePressedCallBack()
    {
        wordContainers[currentWorkContainerIndex].RemoveLetter();
        canAddLetter = true;
    }
}
