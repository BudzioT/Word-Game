using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Manager of game's data
public class DataManager : MonoBehaviour
{
    // Static instance
    public static DataManager Instance;

    [Header("Data")] 
    // Player's current coins amount
    private int _coins;
    // His score and highscore

    private int _score;
    private int _highscore;
    
    // On awakening, make sure to have only one instance of this class
    private void Awake()
    {
        if (!Instance)
            Instance = this;
        else
            Destroy(gameObject);
        
        // Load the last data
        Load();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    // Get last keyword
    public string GetKeyword()
    {
        return _keyword;
    }
    
    // Get player's amount of coins
    public int GetCoins()
    {
        return _coins;
    }
    
    // Get player's score
    public int GetScore()
    {
        return _score;
    }
    
    // Get highscore
    public int GetHighscore()
    {
        return _highscore;
    }

    // Save player's data
    private void Save()
    {
        PlayerPrefs.SetInt("coins", _coins);
        PlayerPrefs.SetInt("score", _score);
        PlayerPrefs.SetInt("highscore", _highscore);
    }
    
    // Load the data
    private void Load()
    {
        _coins = PlayerPrefs.GetInt("coins", 100);
        _score = PlayerPrefs.GetInt("score", _score);
        _highscore = PlayerPrefs.GetInt("highscore", _highscore);
    }

    // Increase current amount of coins
    public void IncreaseCoins(int amount)
    {
        _coins += amount;
        // Save new data
        Save();
    }
    
    // Remove coins from current amount
    public void RemoveCoins(int amount)
    {
        // Make sure not to have less than 0 coins
        _coins = Mathf.Max(0, _coins - amount);
        // Save current data
        Save();
    }
    
    // Increase player's score
    public void IncreaseScore(int amount)
    {
        _score += amount;
        
        // If current score is better than highscore, update it
        if (_highscore < _score)
            _highscore = _score;
        
        // Save data
        Save();
    }
}
