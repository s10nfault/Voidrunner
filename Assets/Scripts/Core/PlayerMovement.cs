using EventSO;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private Vector2EventSO moveInput;
        [SerializeField] private VoidEventSO jumpInput;
        
        [SerializeField] private float moveSpeed = 5f;
        [SerializeField] private float jumpForce = 1f;

        private Rigidbody2D _rb;
        
        private float _moveDirection;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
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
            _rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        private void Move(Vector2 direction)
        {
            _moveDirection = direction.x;
        }
    }
}