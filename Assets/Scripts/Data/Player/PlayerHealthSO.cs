using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "PlayerHealth", menuName = "Player/Health", order = 0)]
    public class PlayerHealthSO : ScriptableObject
    {
        public int maxHealth = 1;
    }
}