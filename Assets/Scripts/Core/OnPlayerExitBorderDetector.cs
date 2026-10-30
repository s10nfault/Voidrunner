using EventSO;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class OnPlayerExitBorderDetector : MonoBehaviour
    {
        [SerializeField] private VoidEventSO onPlayerExitBorder;
        
        private bool _isPlayerExitBorder;
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (_isPlayerExitBorder || !other.CompareTag("Player")) return;
            
            onPlayerExitBorder?.Invoke();
            _isPlayerExitBorder = true;
        }
    }
}