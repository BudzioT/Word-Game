using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordManager : MonoBehaviour
{
    // Set one static word manager
    public static WordManager Instance;
    
    // Game's keyword
    [Header("Elements")] [SerializeField] private string keyword;

    // Prepare the word manager on awakening
    private void Awake()
    {
        // If the static word manager isn't set, set it as this one
        if (!Instance)
            Instance = this;
        
        // Otherwise destroy this object, only one word manager is allowed
        else
            Destroy(gameObject);
    }

    // Return the game's keyword, formatted to upper case
    public string GetKeyword()
    {
        return keyword.ToUpper();
    }
}
