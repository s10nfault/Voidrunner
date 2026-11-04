using EventSO;
using UnityEngine;

namespace Test.Core
{
    public class GameRestartTester : MonoBehaviour
    {
        [Header("Broadcasted Events")]
        [SerializeField] private VoidEventSO gameRestartEvent;
        
        [ContextMenu("Test Game Restart")]
        private void TestGameRestart()
        {
            gameRestartEvent.Invoke();
        }
    }
}