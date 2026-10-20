using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "PlayerMaxHealth", menuName = "Player/MaxHealth", order = 0)]
    public class PlayerMaxHealthSO : ScriptableObject
    {
        [Min(0)]
        public int maxHealth = 1;
    }
}