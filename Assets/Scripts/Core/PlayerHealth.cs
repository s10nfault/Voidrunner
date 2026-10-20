using Data.Player;
using EventSO;
using UnityEngine;

namespace Core
{
    public class PlayerHealth : MonoBehaviour
    {
        [Header("Player Health Settings")]
        [SerializeField] private PlayerMaxHealthSO playerMaxHealthSO;
        [Header("Player Health Data")]
        [SerializeField] private PlayerCurrentHealthSO playerCurrentHealthSO;
        [Header("Events broadcasted")]
        [SerializeField] private VoidEventSO gameOverEventSO;
        [Header("Events received")]
        [SerializeField] private VoidEventSO playerDeathEventSO;
        [SerializeField] private IntEventSO playerDamageEventSO;
        
        private void Awake()
        {
            if (playerMaxHealthSO == null || playerCurrentHealthSO == null)
            {
                Debug.LogError("PlayerHealthSO is not assigned in PlayerHealth.");
                return;
            }
            playerCurrentHealthSO.currentHealth = playerMaxHealthSO.maxHealth;
        }

        private void OnEnable()
        {
            if (playerMaxHealthSO == null || playerCurrentHealthSO == null) return;
            
            playerDamageEventSO.OnEvent += (OnDamage);
            playerDeathEventSO.OnEvent += (OnDeath);
        }

        private void OnDisable()
        {
            if (playerMaxHealthSO == null || playerCurrentHealthSO == null) return;
            
            playerDamageEventSO.OnEvent -= (OnDamage);
            playerDeathEventSO.OnEvent -= (OnDeath);
        }

        private void OnDamage(int damage)
        {
            if (playerCurrentHealthSO == null) return;
            
            if (playerCurrentHealthSO.currentHealth <= 0) return;
            
            playerCurrentHealthSO.currentHealth -= damage;
            if (playerCurrentHealthSO.currentHealth > 0) return;
            
            playerCurrentHealthSO.currentHealth = 0;
            gameOverEventSO?.Invoke();
        }
        
        private void OnDeath()
        {
            if (playerCurrentHealthSO == null) return;
            
            if (playerCurrentHealthSO.currentHealth <= 0) return;

            playerCurrentHealthSO.currentHealth = 0;
            gameOverEventSO?.Invoke();
        }
    }
}