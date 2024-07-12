using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WordContainer : MonoBehaviour
{
    // Letter containers list
    [Header("Elements")] private LetterContainer[] _letterContainers;
    
    // Current letter index
    [Header("Settings")] private int _letterIndex;

    // Load this after the script awakes
    private void Awake()
    {
        // Initialize letter containers from the childrens
        _letterContainers = GetComponentsInChildren<LetterContainer>();
        
        // Initialize the script
        Initialize();
    }
    
    // Initialize the word container
    public void Initialize()
    {
        // Reset current letter's index
        _letterIndex = 0;
        
        // Go through each of the letters containers, initialize them
        for (int i = 0; i < _letterContainers.Length; ++i)
            _letterContainers[i].Initialize();
    }
    
    // Handle adding a letter
    public void Add(char letter)
    {
        // Set the current letter
        _letterContainers[_letterIndex].SetLetter(letter);
        // Move to the next one
        ++_letterIndex;
    }
    
    // Add a letter using a hint
    public void AddWithHint(int letterIndex, char letter)
    {
        _letterContainers[letterIndex].SetLetter(letter, true);
    }
    
    // Handle removing letter
    public bool Remove()
    {
        // If there aren't any letters, return
        if (_letterIndex < 1)
            return false;
        
        // Go back to the previous index
        --_letterIndex;
        // Reset it by initializing it again
        _letterContainers[_letterIndex].Initialize();
        
        // Return success
        return true;
    }

    // Return if the current word is completed
    public bool IsComplete()
    {
        return _letterIndex >= 5;
    }
    
    // Get the current word
    public string GetWord()
    {
        // Variable to build the word
        string word = "";

        // Go through each letter and append it  to the word
        for (int i = 0; i < _letterContainers.Length; ++i)
            word += _letterContainers[i].GetLetter().ToString();
        
        // Return the result word
        return word;
    }

    // Set the color of the letters
    public void SetColor(string keyword)
    {
        // Split the keyword into characters, store them in a list
        List<char> keywordChars = new List<char>(keyword.ToCharArray());
        
        // Go through each of current word's letters
        for (int i = 0; i < _letterContainers.Length; ++i)
        {
            // Get the current letter
            char letter = _letterContainers[i].GetLetter();
            
            // Check if it's correct
            if (letter == keyword[i])
            {
                // Set is as valid
                _letterContainers[i].SetCorrect();
                // Remove it from the letters to check
                keywordChars.Remove(letter);
            }
            
            // Check if it's somewhere else
            else if (keywordChars.Contains(letter))
            {
                // Set is as potential letter
                _letterContainers[i].SetPotential();
                // Remove it
                keywordChars.Remove(letter);
            }
            
            // Otherwise set it as invalid
            else
            {
                _letterContainers[i].SetWrong();
            }
        }
    }
}
