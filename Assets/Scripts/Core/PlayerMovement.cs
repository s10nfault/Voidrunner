using Data.Player;
using EventSO;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(GroundDetector))]
    public class PlayerMovement : MonoBehaviour
    {
        [Header("Input Events")]
        [SerializeField] private Vector2EventSO moveInput;
        [SerializeField] private VoidEventSO jumpInput;
        
        [Header("Game State Event")]
        [SerializeField] private GameStateEventSO gameStateEvent;
        
        [Header("Grounded State")]
        [SerializeField] private IsPlayerGroundedSO isPlayerGrounded;
        
        [Header("Ground Detector")]
        [SerializeField] private PlayerMovementSO playerMovementData;

        private Rigidbody2D _rb;
        private float _moveDirection;

        private bool _ownsMovementData;

        private bool _canMove = true;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();

            if (isPlayerGrounded == null)
            {
                Debug.LogWarning("IsPlayerGroundedSO is not assigned in the inspector.");
            }

            if (playerMovementData == null)
            {
                Debug.LogWarning("PlayerMovementSO is not assigned in the inspector. It will create a new instance.");

                playerMovementData = ScriptableObject.CreateInstance<PlayerMovementSO>();

                _ownsMovementData = true;
            }
        }

        private void OnEnable()
        {
            if (moveInput == null)
            {
                Debug.LogWarning("MoveInput event is not assigned in the inspector.");
                return;
            }

            if (jumpInput == null)
            {
                Debug.LogWarning("JumpInput event is not assigned in the inspector.");
                return;
            }

            if (gameStateEvent == null)
            {
                Debug.LogWarning("GameStateEvent event is not assigned in the inspector.");
                return;
            }

            moveInput.OnEvent += Move;
            jumpInput.OnEvent += Jump;
            gameStateEvent.OnEvent += OnGameStateChange;
        }

        private void OnDisable()
        {
            if (moveInput == null || jumpInput == null || gameStateEvent == null) return;
            
            moveInput.OnEvent -= Move;
            jumpInput.OnEvent -= Jump;
            gameStateEvent.OnEvent -= OnGameStateChange;

            _moveDirection = 0f;
        }

        private void OnGameStateChange(GameState obj)
        {
            _canMove = obj == GameState.Playing;
            if (!_canMove)
            {
                _rb.linearVelocity = Vector2.zero;
            }
        }

        private void OnDestroy()
        {
            if (_ownsMovementData && playerMovementData != null)
            {
                Destroy(playerMovementData);
            }
        }

        private void FixedUpdate()
        {
            if (!_canMove) return;

            _rb.linearVelocity = new Vector2(_moveDirection * playerMovementData.moveSpeed, _rb.linearVelocity.y);
        }

        private void Jump()
        {
            if (!_canMove) return;

            if (isPlayerGrounded && isPlayerGrounded.isGrounded)
                _rb.AddForce(Vector2.up * playerMovementData.jumpForce, ForceMode2D.Impulse);
        }

        private void Move(Vector2 direction)
        {
            _moveDirection = direction.x;
        }
    }
}