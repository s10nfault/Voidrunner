using Data.Player;
using UnityEngine;

namespace Core
{
    public class EnemyDefeatTrigger : MonoBehaviour
    {
        [Header("Player Velocity Data")]
        [SerializeField] private PlayerVelocitySO playerVelocitySO;
        
        [Header("Player Defeat Data")]
        [SerializeField] private float defeatedVelocity = 5f;

        private void Awake()
        {
            if (playerVelocitySO == null)
            {
                Debug.LogError("PlayerVelocitySO is not assigned in the inspector.");
            }
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                if (playerVelocitySO == null)
                {
                    Debug.LogError("PlayerVelocitySO is not assigned in the inspector.");
                    return;
                }

                if (playerVelocitySO.velocity.magnitude >= defeatedVelocity)
                {
                    EnemyDefeated();
                }
            }
        }
        
        private void EnemyDefeated()
        {
            gameObject.SetActive(false);
        }
    }
}