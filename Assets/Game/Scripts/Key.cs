using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;


// Class representing singular key
public class Key : MonoBehaviour
{
    // Letter text made private but with access allowed in Unity
    [Header("Elements")] [SerializeField] private TextMeshProUGUI letterText;
    // Action to handle key presses
    [Header("Events")] public static Action<char> OnKeyPressed;
    
    // Initialize components at the start
    void Start()
    {
        // Get the button component, add a listener to it
        GetComponent<Button>().onClick.AddListener(SendKeyPressed);
    }

    private void SendKeyPressed()
    {
        // Get the letter's text, make sure it's not null
        OnKeyPressed?.Invoke(letterText.text[0]);
    }
}
