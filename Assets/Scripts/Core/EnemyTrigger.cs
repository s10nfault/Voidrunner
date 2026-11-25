using Data.Player;
using EventSO;
using UnityEngine;

namespace Core
{
    public class EnemyTrigger : MonoBehaviour
    {
        [Header("Damage Configuration")] [SerializeField]
        private int damageAmount = 1;

        [Min(0f)] [SerializeField] private float damageInterval = 1.0f;

        [Header("Broadcast Events")] [SerializeField]
        private IntEventSO playerDamageEventSO;

        [Header("Player Velocity Data")] [SerializeField]
        private PlayerVelocitySO playerVelocitySO;

        [Header("Player Defeat Data")] [SerializeField]
        private float defeatedVelocity = 5f;

        [Header("Receive Events")] [SerializeField]
        private GameStateEventSO gameStateEventSO;

        private float _lastDamageTime;

        private bool _isGameStopped;

        private void Awake()
        {
            if (playerDamageEventSO == null)
            {
                Debug.LogError("PlayerDamageEventSO is not assigned in the inspector.");
            }

            if (playerVelocitySO == null)
            {
                Debug.LogError("PlayerVelocitySO is not assigned in the inspector.");
            }

            if (gameStateEventSO == null)
            {
                Debug.LogError("GameStateEventSO is not assigned in the inspector.");
            }
        }

        private void Start()
        {
            _lastDamageTime = Time.time - damageInterval;
        }

        private void OnEnable()
        {
            gameStateEventSO.OnEvent += OnGameStateChanged;
        }

        private void OnDisable()
        {
            gameStateEventSO.OnEvent -= OnGameStateChanged;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (_isGameStopped)
            {
                return;
            }

            if ((Time.time - _lastDamageTime >= damageInterval) && other.CompareTag("Player"))
            {
                // Ensure that the damage amount is at least 1 to avoid negative or zero damage
                playerDamageEventSO?.Invoke(damageAmount > 0 ? damageAmount : 1);

                _lastDamageTime = Time.time;
            }


            if (playerVelocitySO.velocity.magnitude >= defeatedVelocity)
            {
                EnemyDefeated();
            }
        }

        private void OnGameStateChanged(GameState newState)
        {
            if (newState != GameState.Playing)
            {
                _isGameStopped = true;
            }
        }

        private void EnemyDefeated()
        {
            gameObject.SetActive(false);
        }
    }
}