using Data.Level;
using Data.Player;
using UnityEngine;

namespace Core
{
    public class GameLevelConfigurator : MonoBehaviour
    {
        [Header("Level Configuration")]
        [SerializeField] private LevelConfigSO levelConfigSO;

        [Header("Player Data")]
        [SerializeField] private PlayerMovementSO playerMovementSO;
        [SerializeField] private PlayerCurrentHealthSO playerCurrentHealthSO;

        private void Awake()
        {
            if (levelConfigSO == null)
            {
                Debug.LogError("LevelConfigSO is not assigned in GameLevelConfigurator.");
                return;
            }

            if (playerMovementSO == null)
            {
                Debug.LogError("PlayerMovementSO is not assigned in GameLevelConfigurator.");
                return;
            }

            if (playerCurrentHealthSO == null)
            {
                Debug.LogError("PlayerCurrentHealthSO is not assigned in GameLevelConfigurator.");
                return;
            }

            // Configure player movement
            playerMovementSO.moveSpeed = levelConfigSO.playerInitSpeed;
            playerMovementSO.jumpForce = levelConfigSO.playerJumpForce;
            playerMovementSO.maxSpeed = levelConfigSO.playerMaxSpeed;
            playerMovementSO.speedAdjustmentMultiplier = levelConfigSO.playerSpeedAdjustmentMultiplier;
            
            // Configure player health
            playerCurrentHealthSO.ResetHealth(levelConfigSO.playerMaxHealth);
        }
    }
}