using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class HintManager : MonoBehaviour
{
    [Header("Elements")] 
    // The entire keyboard game object
    [SerializeField] private GameObject keyboard;
    // Array of keys
    private Key[] _keys;
    
    // Reset flag
    [Header("Settings")] private bool _reset;
    
    // List of given hint indices
    private List<int> _letterHintIndices = new List<int>();

    private void Awake()
    {
        _keys = keyboard.GetComponentsInChildren<Key>();
    }
    
    // On start, add components
    private void Start()
    {
        // Add state change callback
        GameManager.StateChanged += StateChanged;
    }
    
    // Deallocate components on destroy
    private void OnDestroy()
    {
        // Remove the state change callback
        GameManager.StateChanged -= StateChanged;
    }

    // Give a hint with keyboard keys
    public void KeyboardHint()
    {
        // Get the keyword
        string keyword = WordManager.Instance.GetKeyword();
        // Store a list of not affected keys
        List<Key> untouchedKeys = new List<Key>();
        
        // Go through each of keys, append it to the array if it's unaffected yet
        foreach (var key in _keys)
        {
            if (key.Untouched())
                untouchedKeys.Add(key);
        }
        
        // Copy of the array
        List<Key> hintUntouchedKeys = new List<Key>(untouchedKeys);
        
        // Go through each untouched key, if the keyword has it, remove it from the copied array
        foreach (var key in untouchedKeys)
        {
            if (keyword.Contains(key.GetLetter()))
                hintUntouchedKeys.Remove(key);
        }
        
        // If all keys were removed, return
        if (hintUntouchedKeys.Count <= 0)
            return;
        
        // Get a random key index from all the invalid keys
        int randomIndex = Random.Range(0, hintUntouchedKeys.Count);
        // Set it to wrong one, to help the user
        hintUntouchedKeys[randomIndex].SetWrong();
    }
    
    // Give a hint by adding letters
    public void LetterHint()
    {
        // Return if all letter hints are used up already
        if (_letterHintIndices.Count >= 5)
        {
            Debug.Log("HINTS ARE ALL USED UP");
            return;
        }

        // List with new letter hint indices
        List<int> newHintIndices = new List<int>();
        // Add not yet used indices into the new list, based off used ones
        for (int i = 0; i < 5; ++i)
        {
            if (!_letterHintIndices.Contains(i))
                newHintIndices.Add(i);
        }
        
        // Get the currently used word container
        WordContainer wordContainer = InputManager.Instance.GetWordContainer();
        
        // Store the keyword
        string keyword = WordManager.Instance.GetKeyword();
        
        // Choose a random index from not yet given hints
        int randomIndex = newHintIndices[Random.Range(0, newHintIndices.Count)];
        // Add it to the already used ones
        _letterHintIndices.Add(randomIndex);
        
        // Add the letter with a hint
        wordContainer.AddWithHint(randomIndex, keyword[randomIndex]);
    }
    
    // Handle changing state
    private void StateChanged(GameStates state)
    {
        // Check to which state does the app change
        switch (state)
        {
            // Into menu
            case GameStates.Menu:
                break;
            
            // Into game
            case GameStates.Game:
                // If reset flag is true, clear the hints and turn off the flag
                if (_reset)
                {
                    _letterHintIndices.Clear();
                    _reset = false;
                }
                break;
            
            // Into level complete
            case GameStates.Complete:
                // Set the reset flag
                _reset = true;
                break;
            
            // Into game over
            case GameStates.Lost:
                // Turn reset flag
                _reset = true;
                break;
        }
    }
}
