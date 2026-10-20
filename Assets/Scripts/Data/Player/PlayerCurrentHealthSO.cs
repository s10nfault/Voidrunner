using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "PlayerCurrentHealth", menuName = "Player/Health", order = 0)]
    public class PlayerCurrentHealthSO : ScriptableObject
    {
        public int currentHealth;
    }
}