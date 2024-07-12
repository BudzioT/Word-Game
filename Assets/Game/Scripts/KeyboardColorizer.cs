using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


// Class that gives keyboard colors
public class KeyboardColorizer : MonoBehaviour
{
    // Array of keys
    [Header("Elements")] private Key[] _keys;
    
    // Reset word flag
    [Header("Settings")] private bool _reset;

    // Initialize the colorizer
    private void Awake()
    {
        // Get the keys
        _keys = GetComponentsInChildren<Key>();
    }
    
    // Add components to the keyboard colorizer
    private void Start()
    {
        // Add state change callback
        GameManager.StateChanged += StateChanged;
    }
    
    // Free the components on destroy
    private void OnDestroy()
    {
        // Delete state change callback
        GameManager.StateChanged -= StateChanged;
    }

    // Set color of keys based off letters state
    public void SetColor(string keyword, string word)
    {
        // Check every key on the keyboard
        for (int i = 0; i < _keys.Length; ++i)
        {
            // Retrieve its letter
            char letter = _keys[i].GetLetter();

            // Go through each of letters in the word that user wrote
            for (int j = 0; j < word.Length; ++j)
            {
                // If current keyboard key isn't this one, continue
                if (letter != word[j])
                    continue;
                
                // If it appears in the keyword and is in correct place, set it as correct
                if (letter == keyword[j])
                    _keys[i].SetCorrect();
                
                // Otherwise, if it's in other place, set it as potential key
                else if (keyword.Contains(letter))
                    _keys[i].SetPotential();
                
                // Else, set it as wrong one
                else
                    _keys[i].SetWrong();
            }
        }
    }

    // Handle state changing
    private void StateChanged(GameStates state)
    {
        // Handle changes correctly depending on the state
        switch (state)
        {
            // On going into game
            case GameStates.Game:
                // Initialize keyboard if game should reset
                if (_reset)
                    Initialize();
                break;
            
            // On completing a level
            case GameStates.Complete:
                // Initialize keyboard
                if (_reset)
                    Initialize();
                
                // Turn off the reset flag
                _reset = false;
                break;
            
            // On losing
            case GameStates.Lost:
                // Switch off the reset flag
                _reset = false;
                break;
        }
    }
    
    // Initialize the keyboard
    private void Initialize()
    {
        // Initialize every key
        foreach (var key in _keys)
            key.Initialize();
        
        // Make the game not reset
        _reset = false;
    }
}
