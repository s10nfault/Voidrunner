using EventSO;
using UnityEngine;

namespace Core
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class OnPlayerExitBorderDetector : MonoBehaviour
    {
        [SerializeField] private VoidEventSO onPlayerExitBorder;
        
        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                onPlayerExitBorder?.Invoke();
            }
        }
    }
}