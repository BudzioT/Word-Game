using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using TMPro;

public class LetterContainer : MonoBehaviour
{
    // Letter's renderer
    [Header("Elements")] [SerializeField] private SpriteRenderer letterContainer;
    // A single letter in the container
    [SerializeField] private TextMeshPro letter;

    // Initialize the letter
    public void Initialize()
    {
        // Set it to empty string
        letter.text = "";
        // Set the default contianer collor to white
        letterContainer.color = Color.white;
    }
    
    // Set the letter
    public void SetLetter(char letter, bool hint = false)
    {
        // If user used a hint, displayed this color with a gray background, otherwise with a black one
        this.letter.color = hint ? Color.gray : Color.black;

        // Set the letter's text
        this.letter.text = letter.ToString();
    }
    
    // Get the letter
    public char GetLetter()
    {
        return letter.text[0];
    }
    
    // Set the letter as correct one
    public void SetCorrect()
    {
        // Set the color to green, to indicate guessing right
        letterContainer.color = Color.green;
    }
    
    // Set the letter as potential (good guess but in wrong place)
    public void SetPotential()
    {
        // Set color to yellow
        letterContainer.color = Color.yellow;
    }
    
    // Set the letter as wrong
    public void SetWrong()
    {
        // Set color to red
        letterContainer.color = Color.gray;
    }
}
