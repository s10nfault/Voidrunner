using Data.Player;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class ObstacleRigidbodyAdjuster : MonoBehaviour
    {
        [Header("Receive Player Velocity Data")] [SerializeField]
        private PlayerVelocitySO playerVelocitySO;

        [Header("Settings")] [SerializeField]
        private float blockedVelocityThreshold = 8f; // Threshold to consider the player as blocked

        private Rigidbody2D _rigidbody2D;

        private void Awake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _rigidbody2D.bodyType = RigidbodyType2D.Static;
            _rigidbody2D.gravityScale = 0;
            _rigidbody2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            _rigidbody2D.simulated = true;
        }

        private void Start()
        {
            if (playerVelocitySO == null)
            {
                Debug.LogError($"{nameof(playerVelocitySO)} is null");
            }
        }

        private void Update()
        {
            if (!playerVelocitySO)
            {
                return;
            }

            // If player velocity bigger than the threshold, it will allow player to pass it.
            _rigidbody2D.simulated = !(playerVelocitySO.velocity.magnitude >= blockedVelocityThreshold);
        }
    }
}