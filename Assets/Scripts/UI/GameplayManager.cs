using System;
using Player;
using UnityEngine;

namespace UI
{
    public class GameplayManager : MonoBehaviour
    {
        public static Action OnGameOver;
        
        
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
        
        public void TogglePause()
        {
            Time.timeScale = Mathf.Approximately(Time.timeScale, 0) ? 1 : 0;
        }
    }
}
