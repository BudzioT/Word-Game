using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


// Class to manage user's interface
public class UIManager : MonoBehaviour
{
    // Make static instance of this class
    public static UIManager Instance;

    [Header("Elements")] 
    // Main game's canvas group
    [SerializeField] private CanvasGroup gameCg;
    // Level's complete canvas group
    [SerializeField] private CanvasGroup completeCg;
    // Game's over canvas group
    [SerializeField] private CanvasGroup gameOverCg;
    // Menu's canvas group
    [SerializeField] private CanvasGroup menuCg;

    [Header("Menu Elements")]
    // Menu visible statistics
    [SerializeField] private TextMeshProUGUI menuCoins;
    [SerializeField] private TextMeshProUGUI menuHighscore;

    [Header("Complete Elements")] 
    // Word that shows when level's completed
    [SerializeField] private TextMeshProUGUI completeWord;
    // Player's coins on level's completed screen
    [SerializeField] private TextMeshProUGUI completeCoins;
    // His score and highscore
    [SerializeField] private TextMeshProUGUI completeScore;
    [SerializeField] private TextMeshProUGUI completeHighscore;

    [Header("Game Elements")] 
    // Current score and coins, while in the game
    [SerializeField] private TextMeshProUGUI gameScore;
    [SerializeField] private TextMeshProUGUI gameCoins;

    [Header("Game Over Elements")]
    // Game over statistics
    [SerializeField] private TextMeshProUGUI gameOverWord;
    [SerializeField] private TextMeshProUGUI gameOverCoins;
    [SerializeField] private TextMeshProUGUI gameOverHighscore;
    
    // Create components on script's awakening
    private void Awake()
    {
        //Create only one instance of this class
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
    }
    
    // On start, initialize the components
    private void Start()
    {
        // Hide level's completion screen
        HideComplete();
        // Show game as the first screen
        ShowGame();
        
        // Assign game's state change function as a callback
        GameManager.StateChanged += StateChange;
    }
    
    // Callback when game state is changed
    private void StateChange(GameStates state)
    {
        // Handle change, depending on the state
        switch (state)
        {
            // When user goes into the game
            case GameStates.Game:
                // Show its screen
                ShowGame();
                // Hide the other screens
                HideComplete();
                HideGameOver();
                break;
            
            // When level is completed
            case GameStates.Complete:
                // Show the level's complete screen
                ShowComplete();
                // Hide the game's screen
                HideGame();
                break;
            
            // When user lost
            case GameStates.Lost:
                // Show game over screen
                ShowGameOver();
                // Hide the game
                HideGame();
                break;
        }
    }

    // Show the given canvas group
    private void Show(CanvasGroup cg)
    {
        // Set its opacity to full
        cg.alpha = 1;
        // Allow interactions
        cg.interactable = true;
        // Block ray casts
        cg.blocksRaycasts = true;
    }
    
    // Hide the specified canvas group
    private void Hide(CanvasGroup cg)
    {
        // Set opacity to 0
        cg.alpha = 0;
        // Disable interactions
        cg.interactable = false;
        // Unblock ray casts
        cg.blocksRaycasts = false;
    }

    // Show the game's screen
    private void ShowGame()
    {
        // Set correct text with amount of coins and score
        gameCoins.text = DataManager.Instance.GetCoins().ToString();
        gameScore.text = DataManager.Instance.GetScore().ToString();
        
        // Show the game
        Show(gameCg);
    }

    // Hide the game's screen
    private void HideGame()
    {
        Hide(gameCg);
    }
    
    // Show menu
    private void ShowMenu()
    {
        // Update menu data
        menuCoins.text = DataManager.Instance.GetCoins().ToString();
        menuHighscore.text = DataManager.Instance.GetHighscore().ToString();
        
        // Show the menu
        Show(menuCg);
    }
    
    // Hide menu
    private void HideMenu()
    {
        Hide(menuCg);
    }
    
    // Show level's complete screen
    private void ShowComplete()
    {
        // Assign all the variable texts needed from the game's data
        completeWord.text = WordManager.Instance.GetKeyword();
        completeCoins.text = DataManager.Instance.GetCoins().ToString();
        completeScore.text = DataManager.Instance.GetScore().ToString();
        completeHighscore.text = DataManager.Instance.GetHighscore().ToString();
        
        Show(completeCg);
    }
    
    // Hide level's complete screen
    private void HideComplete()
    {
        Hide(completeCg);
    }
    
    // Show game over screen
    private void ShowGameOver()
    {
        // Get proper text variables
        gameOverWord.text = WordManager.Instance.GetKeyword();
        gameOverCoins.text = DataManager.Instance.GetCoins().ToString();
        gameOverHighscore.text = DataManager.Instance.GetHighscore().ToString();
        
        // Show the canvas group
        Show(gameOverCg);
    }
    
    // Hide game over screen
    private void HideGameOver()
    {
        Hide(gameOverCg);
    }
}
