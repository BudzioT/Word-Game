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

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Initialize the word container
    public void Initialize()
    {
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

    // Return if the current word is completed
    public bool IsComplete()
    {
        return _letterIndex >= 5;
    }
}
