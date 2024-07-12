using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Word containers
    [Header("Elements")] [SerializeField] private WordContainer[] wordContainers;
    
    // Current word index
    [Header("Settings")] private int _wordContainerIndex;
    
    // Prepare manager at the start
    void Start()
    {
        // Initialize the input manager
        Initialize();
        
        // Assign a letter add function on press to the key
        Key.OnKeyPressed += AddLetter;
    }

    // Update is called once per frame
    void Update()
    {
        
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
        // If current word is completed, move to the next one
        if (wordContainers[_wordContainerIndex].IsComplete())
            ++_wordContainerIndex;

        // Add a letter to the current container
        wordContainers[_wordContainerIndex].Add(letter);
    }
}
