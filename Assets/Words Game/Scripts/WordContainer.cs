using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordContainer : MonoBehaviour
{
    [Header("Elements")]
    private LetterContainer[] letterContainers;

    // Start is called before the first frame update
    void Start()
    {
        letterContainers = GetComponentsInChildren<LetterContainer>();
        Initialize();
    }

    private void Initialize()
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
}
