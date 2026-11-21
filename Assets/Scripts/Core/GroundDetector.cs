using Data.Player;
using UnityEngine;
using System.Collections.Generic;


namespace Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GroundDetector : MonoBehaviour
    {
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private IsPlayerGroundedSO isPlayerGrounded;

        private Rigidbody2D _rb;
        private ContactFilter2D _filter;
        private readonly List<ContactPoint2D> _contacts = new(8);

        private void Awake()
        {
            _rb = GetComponent<Rigidbody2D>();
            _filter = new ContactFilter2D();
            _filter.SetLayerMask(groundLayer);
        }

        private void FixedUpdate()
        {
            if (!isPlayerGrounded) return;

            _rb.GetContacts(_filter, _contacts);

            bool grounded = false;

            foreach (var contact in _contacts)
            {
                if (contact.enabled && contact.normal.y > 0.5f)
                {
                    grounded = true;
                    break;
                }
            }

            isPlayerGrounded.isGrounded = grounded;
        }
    }
}