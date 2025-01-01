using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordContainer : MonoBehaviour
{
    public static WordContainer Instance;
    [Header("Elements")]
    private LetterContainer[] letterContainers;

    [Header("Setting")]
    private int currentLetterIndex;


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
        letterContainers = GetComponentsInChildren<LetterContainer>();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Initialize()
    {
        currentLetterIndex = 0;

        for (int i = 0; i < letterContainers.Length; i++)
        {
            letterContainers[i].Initialize();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Add(char letter)
    {
        letterContainers[currentLetterIndex].SetLetter(letter);
        currentLetterIndex++;
    }

    public bool IsComplete()
    {
        return currentLetterIndex >= 5;
    }

    public string GetWord()
    {
        string word = "";
        for (int i = 0; i < letterContainers.Length; i++)
        {
            word += letterContainers[i].GetLetter().ToString();
        }
        return word;
    }

    public bool RemoveLetter()
    {
        if (currentLetterIndex <= 0)
        {
            return false;
        }

        currentLetterIndex--;
        letterContainers[currentLetterIndex].Initialize();

        return true;
    }

    public void Colorize(string secretWord)
    {
        List<char> Chars = new List<char>(secretWord.ToCharArray());

        for (int i = 0; i < letterContainers.Length; i++)
        {
            char letterToCheck = letterContainers[i].GetLetter();
            if (letterToCheck == secretWord[i])
            {
                letterContainers[i].SetValid();
                Chars.Remove(letterToCheck);
            }
            else if (Chars.Contains(letterToCheck))
            {
                letterContainers[i].SetPotential();
                Chars.Remove(letterToCheck);
            }
            else
            {
                letterContainers[i].SetInValid();
            }
        }
    }

    public void AddAsHint(int letterIndex, char letter)
    {
        letterContainers[letterIndex].SetLetter(letter, true);
    }
}
