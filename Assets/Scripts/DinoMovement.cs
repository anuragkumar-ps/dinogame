
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class DinoMovement : MonoBehaviour
{
    private static readonly int IsJumping = Animator.StringToHash("IsJumping");
    private static readonly int IsDead = Animator.StringToHash("IsDead");

    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private Animator dinoAnimator;
    
    private InputSystem_Actions _inputActions;
    private bool _isJumping;
    private bool _isDead;
    private Rigidbody2D _rigidBody;

    public static Action OnDeath;

    private void Awake()
    {
        _rigidBody = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Enable();
        _inputActions.Player.Jump.performed += OnJumpAction;

        GameManager.OnGameStart += OnGameStart;
        
    }

    private void OnDisable()
    {
        _inputActions.Player.Jump.performed -= OnJumpAction;
        _inputActions.Disable();

        GameManager.OnGameStart -= OnGameStart;
    }

    private void OnGameStart()
    {
        _isDead = false;
        _isJumping = false;
        
        
        dinoAnimator.SetBool(IsDead, _isDead);
        dinoAnimator.SetBool(IsJumping, _isJumping);
    }

    public void OnJumpButtonPressed()
    {
        TryJump();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.CompareTag("Ground")) return;
        _isJumping = false;
        dinoAnimator.SetBool(IsJumping, _isJumping);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Obstacle")) return;
        _isDead = true;
        dinoAnimator.SetBool(IsDead, _isDead);
        OnDeath?.Invoke();
    }

    private void OnJumpAction(InputAction.CallbackContext context)
    {
        TryJump();
    }

    private void TryJump()
    {
        if (_isJumping)
        {
            return;
        }
        
        _isJumping = true;
        _rigidBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        // _rigidBody.linearVelocity = new Vector2(_rigidBody.linearVelocity.x, jumpHeight);
        dinoAnimator.SetBool(IsJumping, _isJumping);
    }
}
