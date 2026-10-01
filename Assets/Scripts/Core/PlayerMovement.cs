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
        
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 1f;

        private Rigidbody2D _rb;
        private GroundDetector _groundDetector;
        private float _moveDirection;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _groundDetector = GetComponent<GroundDetector>();
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
        }
        
        private void FixedUpdate()
        {
            if (_moveDirection != 0f)
                _rb.linearVelocity = new Vector2(_moveDirection * moveSpeed, _rb.linearVelocity.y);
        }
        
        private void Jump()
        {
            if (_groundDetector.IsGrounded)
                _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        private void Move(Vector2 direction)
        {
            _moveDirection = direction.x;
        }
    }
}