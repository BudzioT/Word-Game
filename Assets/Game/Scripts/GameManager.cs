using System.Collections;
using System.Collections.Generic;
using System;

using UnityEngine;

// States of game
public enum GameStates { Menu, Game, Complete, Lost, Idle }

public class GameManager : MonoBehaviour
{
    // Global instance of Game Manager
    public static GameManager Instance;

    // Current state of game
    [Header("Settings")] private GameStates _state;

    // Action on state changed
    [Header("Events")] public static Action<GameStates> StateChanged;

    // Prepare the game manager
    private void Awake()
    {
        // Make sure only one instance of this class exists
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Go to the next level
    public void NextLevel()
    {
        SetState(GameStates.Game);
    }
    
    // Play the game
    public void Play()
    {
        SetState(GameStates.Game);
    }
    
    // Go back to menu
    public void GoBack()
    {
        SetState(GameStates.Menu);
    }

    // Set the current state of game
    public void SetState(GameStates state)
    {
        // Set the state
        this._state = state;
        
        Debug.Log(_state);
        Debug.Log(state);
        
        // Invoke the state changed action
        StateChanged?.Invoke(state);
    }
    
    // Return if the game is in play state
    public bool GameState()
    {
        return _state == GameStates.Game;
    }
}
