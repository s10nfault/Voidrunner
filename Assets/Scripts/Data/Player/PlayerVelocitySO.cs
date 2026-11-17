using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "PlayerVelocity", menuName = "Player/PlayerVelocity", order = 0)]
    public class PlayerVelocitySO : ScriptableObject
    {
        public Vector2 velocity;
    }
}