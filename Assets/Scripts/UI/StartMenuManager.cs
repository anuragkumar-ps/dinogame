using System;
using UnityEngine;

namespace UI
{
    public class StartMenuManager : MonoBehaviour
    {
        public static Action OnGameStart;
        
        [SerializeField] private GameObject startMenu;
        [SerializeField] private GameObject gameMenu;

        private void OnEnable()
        {
            GameOverManager.OnGameRestart += OnGameRestart;
        }

        private void OnDisable()
        {
            GameOverManager.OnGameRestart -= OnGameRestart;
        }
        
        public void OnStartButtonPressed()
        {
            startMenu.SetActive(false);
            gameMenu.SetActive(true);
            OnGameStart?.Invoke();
        }

        private void OnGameRestart()
        {
            OnStartButtonPressed();
        }
    }
}
