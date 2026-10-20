using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "PlayerMaxHealth", menuName = "Player/Health", order = 0)]
    public class PlayerMaxHealthSO : ScriptableObject
    {
        [Range(0, int.MaxValue)]
        public int maxHealth = 1;
    }
}