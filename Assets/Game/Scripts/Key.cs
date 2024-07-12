using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;
using UnityEngine.UI;
using TMPro;


// States of keys
enum States { None, Correct, Potential, Wrong }

// Class representing singular key
public class Key : MonoBehaviour
{
    [Header("Elements")] 
    // Letter text made private but with access allowed in Unity
    [SerializeField] private TextMeshProUGUI letterText;
    // Key renderer (container, background)
    [SerializeField] private Image keyRenderer;

    [Header("Settings")]
    // State of the key
    private States _state;
        
    // Action to handle key presses
    [Header("Events")] public static Action<char> OnKeyPressed;
    
    // Initialize components at the start
    void Start()
    {
        // Get the button component, add a listener to it
        GetComponent<Button>().onClick.AddListener(SendKeyPressed);
        
        // Initialize the key
        Initialize();
    }

    private void SendKeyPressed()
    {
        // Get the letter's text, make sure it's not null
        OnKeyPressed?.Invoke(letterText.text[0]);
    }
    
    // Return key's letter
    public char GetLetter()
    {
        return letterText.text[0];
    }
    
    // Set the key as correct
    public void SetCorrect()
    {
        // Set the color to green
        keyRenderer.color = Color.green;
        
        // Set the key's state to correct
        _state = States.Correct;
    }
    
    // Set the key as potential
    public void SetPotential()
    {
        // Make sure the key isn't already correct
        if (_state == States.Correct)
            return;
        
        // Set color to yellow
        keyRenderer.color = Color.yellow;
        
        // Set state
        _state = States.Potential;
    }
    
    // Set the key as wrong
    public void SetWrong()
    {
        // Make sure it wasn't already in other state
        if (_state == States.Correct || _state == States.Potential)
            return;
        
        // Set gray color
        keyRenderer.color = Color.gray;
        
        // Set the state
        _state = States.Wrong;
    }

    // Further initialize the key
    public void Initialize()
    {
        // Set the background's color
        keyRenderer.color = Color.white;
        
        // Set current state to none
        _state = States.None;
    }
}
