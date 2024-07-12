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
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Give a hint with keyboard keys
    public void KeyboardHint()
    {
        Debug.Log("KEYBOARD HINT!");
        
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
        Debug.Log("LETTER HINT!");
    }
}
