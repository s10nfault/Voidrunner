using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Transform))]
    public class GroundDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckDistance = 0.6f;

        private Transform _transform;

        public bool IsGrounded { get; private set; }

        private void Awake()
        {
            _transform = GetComponent<Transform>();
        }

        private void FixedUpdate()
        {
            IsGrounded = Physics2D.Raycast(_transform.position, Vector2.down, groundCheckDistance, groundLayer);
        }

        private void OnDrawGizmosSelected()
        {
            if (_transform == null) return;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(_transform.position, _transform.position + Vector3.down * groundCheckDistance);
        }
    }
}