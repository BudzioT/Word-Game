using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintManager : MonoBehaviour
{
    [Header("Elements")] 
    // The entire keyboard game object
    [SerializeField] private GameObject keyboard;
    // Array of keys
    private Key[] _keys;
    
    // List of given hint indices
    private List<int> _letterHintIndices = new List<int>();

    private void Awake()
    {
        _keys = keyboard.GetComponentsInChildren<Key>();
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
        
        Debug.Log("LETTER HINT!");
    }
}
