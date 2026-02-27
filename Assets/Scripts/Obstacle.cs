using UnityEngine;

public class Obstacle : MonoBehaviour
{

    [SerializeField] private int moveSpeed;
    private bool _isGameRunning = true;

    private void OnEnable()
    {
        GameManager.OnGameStart += OnGameStart;
        GameManager.OnGameOver += OnGameOver;
    }
    
    private void OnDisable()
    {
        GameManager.OnGameStart -= OnGameStart;
        GameManager.OnGameOver -= OnGameOver;
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
