using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "PlayerMovement", menuName = "Player/Player Movement", order = 0)]
    public class PlayerMovementSO : ScriptableObject
    {
        public float moveSpeed = 5f;
        public float jumpForce = 7f;
        public float speedAdjustmentMultiplier = 1.5f;
        public float maxSpeed = 10f;
    }
}