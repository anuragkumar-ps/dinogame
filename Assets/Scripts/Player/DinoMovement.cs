using System;
using UI;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class DinoMovement : MonoBehaviour
    {
        private static readonly int IsJumpingParam = Animator.StringToHash("IsJumping");
        private static readonly int IsDeadParam = Animator.StringToHash("IsDead");

        [SerializeField] private float jumpHeight = 2f;
        [SerializeField] private Animator dinoAnimator;

        private InputSystem_Actions _inputActions;
        private Rigidbody2D _rigidBody;

        public static event Action OnDeath;

        private bool _isJumping;
        private bool IsJumping
        {
            get => _isJumping;
            set
            {
                _isJumping = value;
                dinoAnimator.SetBool(IsJumpingParam, value);
            }
        }

        private bool _isDead;
        private bool IsDead
        {
            get => _isDead;
            set
            {
                _isDead = value;
                dinoAnimator.SetBool(IsDeadParam, value);
            }
        }

        private void Awake()
        {
            _rigidBody = GetComponent<Rigidbody2D>();
        }

        private void OnEnable()
        {
            _inputActions = new InputSystem_Actions();
            _inputActions.Enable();
            _inputActions.Player.Jump.performed += OnJumpAction;
            StartMenuManager.OnGameStart += OnGameStart;
        }

        private void OnDisable()
        {
            _inputActions.Player.Jump.performed -= OnJumpAction;
            _inputActions.Disable();
            StartMenuManager.OnGameStart -= OnGameStart;
        }

        private void OnGameStart()
        {
            IsDead = false;
            IsJumping = false;
        }

        public void OnJumpButtonPressed()
        {
            TryJump();
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Ground")) return;
            IsJumping = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Obstacle")) return;
            IsDead = true;
            OnDeath?.Invoke();
        }

        private void OnJumpAction(InputAction.CallbackContext context)
        {
            TryJump();
        }

        private void TryJump()
        {
            if (_isJumping || _isDead) return;

            IsJumping = true;
            _rigidBody.AddForce(Vector2.up * jumpHeight, ForceMode2D.Impulse);
        }
    }
}
