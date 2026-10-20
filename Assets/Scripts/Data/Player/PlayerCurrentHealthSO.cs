using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "PlayerCurrentHealth", menuName = "Player/CurrentHealth", order = 0)]
    public class PlayerCurrentHealthSO : ScriptableObject
    {
        public int currentHealth;
    }
}