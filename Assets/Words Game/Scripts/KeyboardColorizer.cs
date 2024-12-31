using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardColorizer : MonoBehaviour
{

    [Header("Elements")]
    private KeyboardKey[] keys;

    private void Awake()
    {
        keys = GetComponentsInChildren<KeyboardKey>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Colorize(string secretWord,string wordToCheck)
    {
        for (int i = 0; i < keys.Length; i++)
        {
            char keyLetter = keys[i].GetLetter();
            for (int j = 0; j < wordToCheck.Length; j++)
            {

            }
        }
    }
}
