using UnityEngine;

namespace Data.Level
{
    [CreateAssetMenu(fileName = "New Level Config", menuName = "Level/Level Config", order = 0)]
    public class LevelConfigSO : ScriptableObject
    {
        [Header("Player Configuration")]
        public int playerMaxHealth;
        public float playerMaxSpeed;
        public float playerJumpForce;
        public float playerInitSpeed;
        public float playerSpeedAdjustmentMultiplier;
    }
}