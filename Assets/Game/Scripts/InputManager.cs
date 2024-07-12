using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    // Word containers
    [Header("Elements")] [SerializeField] private WordContainer[] wordContainers;
    // Button to submit the answer
    [SerializeField] private Button submitButton;
    
    // Keyboard colorizer
    [SerializeField] private KeyboardColorizer _keyboardColorizer;
    
    // Current word index
    [Header("Settings")] private int _wordContainerIndex;
    
    // Can add letter flag
    private bool _canAddLetter = true;
    
    // Prepare manager at the start
    void Start()
    {
        // Initialize the input manager
        Initialize();
        
        // Assign a letter add function on press to the key
        Key.OnKeyPressed += AddLetter;
    }
    
    // Initialize the manager
    private void Initialize()
    {
        // Go through every word container and initialize it
        for (int i = 0; i < wordContainers.Length; ++i)
            wordContainers[i].Initialize();
    }
    
    // Add a letter to the word
    private void AddLetter(char letter)
    {
        // If it isn't possible to add letters anymore, don't add it
        if (!_canAddLetter)
            return;
        
        // Add a letter to the current container
        wordContainers[_wordContainerIndex].Add(letter);
        
        // If current word is completed, move to the next one
        if (wordContainers[_wordContainerIndex].IsComplete())
        {
            // Don't allow adding letters anymore
            _canAddLetter = false;
            
            // Enable the submit button
            EnableSubmit();
        }
    }
    
    // Check if the word is correct
    public void CheckWord()
    {
        // Get the current word
        string word = wordContainers[_wordContainerIndex].GetWord();
        // Store the keyword
        string keyword = WordManager.Instance.GetKeyword();
               
        // Set color of the word letters based of their state
        wordContainers[_wordContainerIndex].SetColor(keyword);
        // Also set the color of the keyboard letter
        keyboardColorizer.SetColor(keyword, word);
        
        // If there are the same, complete the game
        if (word == keyword)
        {
            Debug.Log("COMPLETE!");
        }

        // Otherwise move to the next line
        else
        {
            // Allow adding letters again
            _canAddLetter = true;
            // Disable the submit button
            DisableSubmit();
            
            Debug.Log("WRONG!");
            
            // Move to the next line
            ++_wordContainerIndex;
        }
    }

    // Handle backspace being pressed
    public void BackspacePress()
    {
        // Remove the current letter and store if it was successful
        bool removed = wordContainers[_wordContainerIndex].Remove();
        
        // If it was, disable the submit button
        if (removed)
            DisableSubmit();
        
        // Set add flag again to true
        _canAddLetter = true;
    }
    
    // Enable submit button interactions
    private void EnableSubmit()
    {
        submitButton.interactable = true;
    }
    
    // Disable the submit button
    private void DisableSubmit()
    {
        submitButton.interactable = false;
    }
}
