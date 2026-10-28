using EventSO;
using UnityEngine;

namespace Core
{
    public class EnemyDamageTrigger : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private int damageAmount = 1;
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
            
            playerDamageEventSO?.Invoke(damageAmount);
            _lastDamageTime = Time.time;
        }
    }
}