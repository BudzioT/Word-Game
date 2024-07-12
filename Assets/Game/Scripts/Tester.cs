using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Class for testing
public class Tester : MonoBehaviour
{
    void Start()
    {
        // Assign an action to test letter on key pressed
        Key.OnKeyPressed += TestLetter;
    }

    // Test if a letter works
    private void TestLetter(char letter)
    {
        // Log the specified letter
        Debug.Log(letter);
    }
}
