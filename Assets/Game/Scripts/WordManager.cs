using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class WordManager : MonoBehaviour
{
    // Set one static word manager
    public static WordManager Instance;
    
    [Header("Elements")] 
    // Game's keyword
    [SerializeField] private string keyword;
    // Text file with words
    [SerializeField] private TextAsset wordsText;
    
    // Available words
    private string _words;

    // Reset the word flag
    [Header("Settings")] private bool _reset;

    // Prepare the word manager on awakening
    private void Awake()
    {
        // If the static word manager isn't set, set it as this one
        if (!Instance)
            Instance = this;

        // Otherwise destroy this object, only one word manager is allowed
        else
            Destroy(gameObject);
        
        // Load the words
        _words = wordsText.text;
    }
    
    // Create components on start
    private void Start()
    {
        // Choose a new keyword
        SetNewKeyword();
        
        // Assign a new callback when changing game states
        GameManager.StateChanged += StateChanged;
    }
    
    // Clean the resources on destroy
    private void OnDestroy()
    {
        // Unassign the state change callback
        GameManager.StateChanged -= StateChanged;
    }

    // Return the game's keyword, formatted to upper case
    public string GetKeyword()
    {
        return keyword.ToUpper();
    }
    
    // Set a new keyword from the loaded words
    private void SetNewKeyword()
    {
        Debug.Log("Length: " + _words.Length);
        
        // Count of loaded words
        int wordCount = (_words.Length + 2) / 7;
        
        // Choose a random word
        int wordIndex = Random.Range(0, wordCount);
        // Get its index in file
        int wordStart = wordIndex * 7;
        
        // Set the new word as keyword
        keyword = _words.Substring(wordStart, 5).ToUpper();
        
        // Turn off the reset flag
        _reset = false;
    }

    // Handle changing state
    private void StateChanged(GameStates state)
    {
        // Handle it depending on the state
        switch (state)
        {
            // Going into menu
            case GameStates.Menu:
                break;
            
            // Playing the game
            case GameStates.Game:
                // If reset flag is true, choose a new keyword
                if (_reset)
                    SetNewKeyword();
                break;
            
            // Completing a level
            case GameStates.Complete:
                // Set the reset flag
                _reset = true;
                break;
            
            // Losing
            case GameStates.Lost:
                // Turn on the reset flag
                _reset = true;
                break;
        }
    }
}
