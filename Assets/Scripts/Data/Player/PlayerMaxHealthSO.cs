using System;
using UnityEngine;

namespace Data.Player
{
    [CreateAssetMenu(fileName = "PlayerMaxHealth", menuName = "Player/MaxHealth", order = 0)]
    [Obsolete("Use LevelConfigSO instead.")]
    public class PlayerMaxHealthSO : ScriptableObject
    {
        [Min(1)]
        public int maxHealth = 1;
    }
}