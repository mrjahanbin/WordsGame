using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordContainer : MonoBehaviour
{
    [Header("Elements")]
    private LetterContainer[] letterContainers;

    [Header("Setting")]
    private int currentLetterIndex;


    private void Awake()
    {
        letterContainers = GetComponentsInChildren<LetterContainer>();
        //Initialize();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    public void Initialize()
    {
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
            else { 
                letterContainers[i].SetInValid();
            }
        }
    }
}
