using EventSO;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class GameWiningTrigger : MonoBehaviour
    {
        [SerializeField] private VoidEventSO gameWinEvent;
        
        private bool _isWinTriggered;

        private void Awake()
        {
            if (gameWinEvent == null)
            {
                Debug.LogError("GameWinEvent is not assigned in the inspector.");
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!_isWinTriggered && !other.CompareTag("Player")) return;
            
            gameWinEvent?.Invoke();
            _isWinTriggered = true;
        }
    }
}