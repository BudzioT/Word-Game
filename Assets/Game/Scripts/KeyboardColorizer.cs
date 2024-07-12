using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class that gives keyboard colors
public class KeyboardColorizer : MonoBehaviour
{
    // Array of keys
    [Header("Elements")] private Key[] _keys;

    // Initialize the colorizer
    private void Awake()
    {
        // Get the keys
        _keys = GetComponentsInChildren<Key>();
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
}
