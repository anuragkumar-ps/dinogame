using System;
using UnityEngine;

namespace UI
{
    public class GameOverManager : MonoBehaviour
    {
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject gameOverMenu;
        [SerializeField] private GameObject gameMenu;

        public static Action OnGameRestart;
        
        public void OnRestartButtonPressed()
        {
            gameOverMenu.SetActive(false);
            gameMenu.SetActive(true);
            OnGameRestart?.Invoke();
        }

        public void OnMenuButtonPressed()
        {
            gameOverMenu.SetActive(false);
            startMenu.SetActive(true);
        }
    }
}
