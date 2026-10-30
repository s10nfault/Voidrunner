using EventSO;
using UnityEngine;

namespace Test.Core
{
    public class EventSOReceiver : MonoBehaviour
    {
        [SerializeField] private VoidEventSO eventSO;
        
        private void OnEventRaised()
        {
            Debug.Log("Event raised!");
        }

        private void OnEnable()
        {
            eventSO.OnEvent += OnEventRaised;
        }

        private void OnDisable()
        {
            eventSO.OnEvent -= OnEventRaised;
        }
        }
}