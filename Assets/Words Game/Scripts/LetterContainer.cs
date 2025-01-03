using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LetterContainer : MonoBehaviour
{

    [Header("Elements")]
    [SerializeField] private SpriteRenderer letterContainers;
    [SerializeField] private TextMeshPro letter;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Initialize()
    {
        letter.text = "";
        letterContainers.color = Color.white;
    }

    public void SetLetter(char letter, bool isHint = false)
    {
        if (isHint)
        {
            this.letter.color = Color.blue;
        }
        else
        {
            this.letter.color = Color.black;
        }
        this.letter.text = letter.ToString();
    }

    public char GetLetter()
    {
        return letter.text[0];
    }

    public void SetValid()
    {
        letterContainers.color = Color.green;
    }

    public void SetPotential()
    {
        letterContainers.color = Color.yellow;
    }

    public void SetInValid()
    {
        letterContainers.color = Color.gray;
    }
}
