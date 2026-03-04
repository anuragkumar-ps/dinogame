using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    private int _score = 0;
    private int _highScore = 0;

    [SerializeField] private float scoreUpdateRate = 0.5f;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;

    private void OnEnable()
    {
        StartMenuManager.OnGameStart += OnGameStart;
        GameplayManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        StartMenuManager.OnGameStart -= OnGameStart;
        GameplayManager.OnGameOver -= OnGameOver;
    }

    private void Awake()
    {
        _highScore = PlayerPrefs.GetInt("HighScore");
        highScoreText.text = "High Score: " + _highScore;
    }

    private void OnGameStart()
    {
        _score = 0;
        InvokeRepeating(nameof(UpdateScore), 0f, scoreUpdateRate);
    }

    private void OnGameOver()
    {
        CancelInvoke(nameof(UpdateScore));
    }

    private void UpdateScore()
    {
        _score++;
        scoreText.text = "Score: " + _score;
        _highScore = Math.Max(_score, _highScore);
        highScoreText.text = "High Score: " + _highScore;
        
        PlayerPrefs.SetInt("HighScore", _highScore);
    }
    
}
