using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Transform))]
    public class GroundDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Vector2 groundCheckSize = new(1f, 0.1f);
        [SerializeField] private Vector2 groundCheckOffset = new(0f, -10f);

        private Transform _transform;

        public bool IsGrounded { get; private set; }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            IsGrounded = Physics2D.OverlapBox(
                _transform.position + (Vector3)groundCheckOffset,
                groundCheckSize,
                0f,
                groundLayer
            );
        }

        private void OnDrawGizmos()
        {
            if (_transform == null) return;
            
            Gizmos.color = Color.red;
            
            Gizmos.DrawWireCube(
                _transform.position + (Vector3)groundCheckOffset,
                groundCheckSize
            );
        }
    }
}