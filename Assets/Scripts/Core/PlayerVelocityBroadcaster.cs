using Data.Player;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerVelocityBroadcaster : MonoBehaviour
    {
        [Header("Player Velocity Data")]
        [SerializeField] private PlayerVelocitySO playerVelocitySO;

        private Rigidbody2D _rigidbody2D;
        
        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            if (!_rigidbody2D)
            {
                Debug.LogError("Rigidbody2D component is missing on the GameObject.");
            }

            if (!playerVelocitySO)
            {
                Debug.LogError("PlayerVelocitySO is not assigned in the inspector.");
            }
            
        }
        
        private void Update()
        {
            if (!playerVelocitySO)
            {
                Debug.LogError("PlayerVelocitySO is not assigned in the inspector.");
                return;
            }

            playerVelocitySO.velocity = _rigidbody2D.linearVelocity;
        }

        private void OnGUI()
        {
            GUI.Label(new Rect(10, 10, 100, 20), playerVelocitySO.velocity.magnitude.ToString());
        }
    }
}