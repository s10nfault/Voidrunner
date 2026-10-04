using Data.Player;
using EventSO;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(GroundDetector))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Vector2EventSO moveInput;
        [SerializeField] private VoidEventSO jumpInput;

        [SerializeField] private PlayerMovementSO playerMovementData;

        private Rigidbody2D _rb;
        private GroundDetector _groundDetector;
        private float _moveDirection;

        private bool _ownsMovementData;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _groundDetector = GetComponent<GroundDetector>();

            if (playerMovementData != null) return;
            
            Debug.LogWarning("PlayerMovementSO is not assigned in the inspector. It will create a new instance.");
            
            playerMovementData = ScriptableObject.CreateInstance<PlayerMovementSO>();
            
            _ownsMovementData = true;
        }

        private void OnEnable()
        {
            moveInput.OnEvent += Move;
            jumpInput.OnEvent += Jump;
        }

        private void OnDisable()
        {
            moveInput.OnEvent -= Move;
            jumpInput.OnEvent -= Jump;
            
            _moveDirection = 0f;
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
            _rb.linearVelocity = new Vector2(_moveDirection * playerMovementData.moveSpeed, _rb.linearVelocity.y);
        }

        private void Jump()
        {
            if (_groundDetector.IsGrounded)
                _rb.AddForce(Vector2.up * playerMovementData.jumpForce, ForceMode2D.Impulse);
        }

        private void Move(Vector2 direction)
        {
            _moveDirection = direction.x;
        }
    }
}