using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class LetterContainer : MonoBehaviour
{
    // A single letter in the container
    [Header("Elements")] [SerializeField] private TextMeshPro letter;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Initialize the letter
    public void Initialize()
    {
        // Set it to empty string
        letter.text = "";
    }
    
    // Set the letter
    public void SetLetter(char letter)
    {
        this.letter.text = letter.ToString();
    }
}
