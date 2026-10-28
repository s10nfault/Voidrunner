using EventSO;
using UnityEngine;

namespace Core
{
    public class EnemyDamageTrigger : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private int damageAmount = 1;
        [Min(0f)]
        [SerializeField] private float damageInterval = 1.0f;

        [Header("Broadcast Events")]
        [SerializeField] private IntEventSO playerDamageEventSO;
        
        private float _lastDamageTime;

        private void Start()
        {
            _lastDamageTime = Time.time;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!(Time.time - _lastDamageTime >= damageInterval) || !other.CompareTag("Player")) return;
            
            // Ensure that the damage amount is at least 1 to avoid negative or zero damage
            playerDamageEventSO?.Invoke(damageAmount >= 0 ? damageAmount : 1);
            _lastDamageTime = Time.time;
        }
    }
}