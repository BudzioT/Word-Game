using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    // static instance of this class
    public static InputManager Instance;
    
    // Word containers
    [Header("Elements")] [SerializeField] private WordContainer[] wordContainers;
    // Button to submit the answer
    [SerializeField] private Button submitButton;
    
    // Keyboard colorizer
    [SerializeField] private KeyboardColorizer keyboardColorizer;
    
    // Current word index
    [Header("Settings")] private int _wordContainerIndex;
    
    // Can add letter flag
    private bool _canAddLetter = true;
    
    // On script's awakening, make sure to remain only one copy of this class
    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Prepare manager at the start
    void Start()
    {
        // Initialize the input manager
        Initialize();
        
        // Assign a letter add function on press to the key
        Key.OnKeyPressed += AddLetter;
        // Add function to go into the next level too
        GameManager.StateChanged += StateChange;
    }
    
    // Free the components on destroy
    private void OnDestroy()
    {
        // Unassign the letter add function from the actions
        Key.OnKeyPressed -= AddLetter;
        // Delete the state change function too
        GameManager.StateChanged += StateChange;
    }

    // Initialize the manager
    private void Initialize()
    {
        // Reset the word's index, allow adding letters
        _wordContainerIndex = 0;
        _canAddLetter = true;
        
        // Disable submit button
        DisableSubmit();
        
        // Go through every word container and initialize it
        foreach (var word in wordContainers)
            word.Initialize();
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
        
        // If there are the same, complete the level
        if (word == keyword)
        {
            SetLevelComplete();
        }

        // Otherwise move to the next line
        else
        {
            // Disable the submit button
            DisableSubmit();
            
            // Move to the next line
            ++_wordContainerIndex;

            // If this was the lasts possible word guess, handle game over
            if (_wordContainerIndex >= wordContainers.Length)
            {
                Debug.Log("GAME OVER!");
                
                // Reset the score
                DataManager.Instance.ResetScore();
                
                // Set current state to lost
                GameManager.Instance.SetState(GameStates.Lost);
            }
            // Otherwise, continue the game
            else
            {
                // Allow keyboard input
                _canAddLetter = true;
            }
        }
    }

    // Handle backspace being pressed
    public void BackspacePress()
    {
        // If user isn't in the game, return
        if (!GameManager.Instance.GameState())
            return;
        
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
    
    // Set level as completed
    private void SetLevelComplete()
    {
        // Update game's data
        UpdateData();
        
        // Set state to level completed
        GameManager.Instance.SetState(GameStates.Complete);
    }

    // Update game's information
    private void UpdateData()
    {
        // Calculate score increase, based off amount of words used
        int scoreIncrease = 6 - _wordContainerIndex;
        
        // Add it to the game's data
        DataManager.Instance.IncreaseScore(scoreIncrease);
        // Increase coins too
        DataManager.Instance.IncreaseCoins(scoreIncrease * 2);
    }
    
    // Handle game changing state
    private void StateChange(GameStates state)
    {
        // Handle it depending on the state
        switch (state)
        {
            // On starting game
            case GameStates.Game:
                // Initialize it
                Initialize();

                break;
            
            // On completing a level
            case GameStates.Complete:
                break;
        }
    }

    // Return the currently used word container
    public WordContainer GetWordContainer()
    {
        return wordContainers[_wordContainerIndex];
    }
}
