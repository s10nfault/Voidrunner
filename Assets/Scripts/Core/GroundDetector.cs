using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Transform))]
    public class GroundDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private Vector2 groundCheckSize = new(1f, 0.1f);
        [SerializeField] private Vector2 groundCheckOffset = new(0f, -0.5f);

        public bool IsGrounded { get; private set; }

        private void FixedUpdate()
        {
            IsGrounded = Physics2D.OverlapBox(
                transform.position + (Vector3)groundCheckOffset,
                groundCheckSize,
                0f,
                groundLayer
            );
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;

            Gizmos.DrawWireCube(
                transform.position + (Vector3)groundCheckOffset,
                groundCheckSize
            );
        }
    }
}