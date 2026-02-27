using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static Action OnGameStart;
    public static Action OnGameOver;

    [SerializeField] private GameObject startMenu;
    [SerializeField] private GameObject gameMenu;
    [SerializeField] private GameObject gameOverMenu;
    
    private void OnEnable()
    {
        DinoMovement.OnDeath += OnDinoDeath;
    }

    private void OnDisable()
    {
        DinoMovement.OnDeath -= OnDinoDeath;
    }

    private void OnDinoDeath()
    {
        OnGameOver?.Invoke();
        Invoke(nameof(HandleDeath), 1f);
    }

    private void HandleDeath()
    {
        gameMenu.SetActive(false);
        gameOverMenu.SetActive(true);
        
    }

    public void OnStartButtonPressed()
    {
        startMenu.SetActive(false);
        gameMenu.SetActive(true);
        OnGameStart?.Invoke();
    }

    public void OnRestartButtonPressed()
    {
        gameOverMenu.SetActive(false);
        gameMenu.SetActive(true);
        OnGameStart?.Invoke();
    }

    public void OnMenuButtonPressed()
    {
        gameOverMenu.SetActive(false);
        startMenu.SetActive(true);
    }

    public void TogglePause()
    {
        Time.timeScale = Mathf.Approximately(Time.timeScale, 0) ? 1 : 0;
    }
}
