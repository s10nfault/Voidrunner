using Data.Player;
using EventSO;
using UnityEngine;

namespace Core
{
    public class GamePlayerSpeedAdjuster : MonoBehaviour
    {
        [Header("Input Events")] [SerializeField]
        private PlayerCurrentHealthSO playerCurrentHealthSO;
        
        [SerializeField] private PlayerMovementSO playerMovementSO;
        [SerializeField] private GameStateEventSO gameStateEventSO;
        [SerializeField] private IntEventSO playerDamageEventSO;

        private float _initialMoveSpeed;

        private void Start()
        {
            if (playerCurrentHealthSO == null)
            {
                Debug.LogWarning("PlayerCurrentHealthSO is not assigned in the inspector.");
                return;
            }

            _initialMoveSpeed = playerMovementSO.moveSpeed;
            
            if (_initialMoveSpeed <= 0)
            {
                Debug.LogWarning("Initial move speed is not set or is less than or equal to zero.");
                _initialMoveSpeed = 1;// Set a default value to avoid division by zero or negative speed.
            }
        }

        private void OnEnable()
        {
            if (playerCurrentHealthSO == null)
            {
                Debug.LogWarning("PlayerCurrentHealthSO is not assigned in the inspector.");
                return;
            }

            if (playerMovementSO == null)
            {
                Debug.LogWarning("PlayerMovementSO is not assigned in the inspector.");
                return;
            }

            if (gameStateEventSO == null)
            {
                Debug.LogWarning("GameStateEventSO is not assigned in the inspector.");
                return;
            }

            if (playerDamageEventSO == null)
            {
                Debug.LogWarning("PlayerDamageEventSO is not assigned in the inspector.");
                return;
            }

            playerDamageEventSO.OnEvent += OnPlayerDamage;
            gameStateEventSO.OnEvent += OnGameStateChanged;
        }

        private void OnPlayerDamage(int damageAmount)
        {
            playerMovementSO.moveSpeed *= playerMovementSO.speedAdjustmentMultiplier;
            
            if (playerCurrentHealthSO.CurrentHealth <= 0)
            {
                playerMovementSO.moveSpeed = _initialMoveSpeed;
            }

            playerMovementSO.moveSpeed = Mathf.Min(playerMovementSO.moveSpeed, playerMovementSO.maxSpeed);
        }

        private void OnDisable()
        {
            if (gameStateEventSO != null)
            {
                gameStateEventSO.OnEvent -= OnGameStateChanged;
            }

            if (playerDamageEventSO != null)
            {
                playerDamageEventSO.OnEvent -= OnPlayerDamage;
            }
        }

        private void OnDestroy()
        {
            if (playerMovementSO != null)
            {
                playerMovementSO.moveSpeed = _initialMoveSpeed;
            }
        }

        private void OnGameStateChanged(GameState obj)
        {
            playerMovementSO.moveSpeed = _initialMoveSpeed;
        }
    }
}