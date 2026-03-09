using UI;
using UnityEngine;

namespace Obstacles
{
    public class Obstacle : MonoBehaviour
    {

        [SerializeField] private int moveSpeed;
        private bool _isGameRunning = true;

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
    
        private void OnGameStart()
        {
            _isGameRunning = true;
        }

        private void OnGameOver()
        {
            _isGameRunning = false;
        }

        private void Update()
        {
            if (_isGameRunning)
            {
                transform.Translate(Vector2.left * (moveSpeed * Time.deltaTime));
            }
        }
    }
}
