using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public enum Validity
{
    None,Valid,Potential,Invalid
}
public class KeyboardKey : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private Image renderers;
    [SerializeField] private TextMeshProUGUI letterText;

    [Header("Setting")]
    private Validity validity;

    [Header("Events")]
    public static Action<char> onKeyPressed;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(SendKeyPressedEvent);
    }

    private void SendKeyPressedEvent()
    {
        onKeyPressed?.Invoke(letterText.text[0]);
    }

    // Update is called once per frame
    void Update()
    {

    }
    
    public char GetLetter()
    {
        return letterText.text[0];
    }

    public void Initialize()
    {
        renderers.color = Color.white;
        validity = Validity.None;
    }

    public void SetValid()
    {
        renderers.color = Color.green;
        validity = Validity.Valid;
    }

    public void SetPotential()
    {
        if (validity == Validity.Valid)
        {
            return;
        }
        renderers.color = Color.yellow;
        validity = Validity.Potential;


    }

    public void SetInValid()
    {
        if (validity == Validity.Valid || validity == Validity.Potential)
        {
            return;
        }

        renderers.color = Color.gray;
        validity = Validity.Invalid;

    }
}
