using EventSO;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core
{
    public class GameLevelManager : MonoBehaviour
    {
        [Header("Configuration")]
        [SerializeField] private string nextLevelName;
        
        [Header("Received Events")]
        [SerializeField] private VoidEventSO gameTransitionEvent;
        
        [Header("Broadcasted Events")]
        [SerializeField] private VoidEventSO gameSavingEvent;
        [SerializeField] private VoidEventSO gameLoadingEvent;
        
        private bool _isTransitioning;
        
        private void OnEnable()
        {
            gameTransitionEvent.OnEvent += OnGameTransition;
        }

        private void OnDisable()
        {
            gameTransitionEvent.OnEvent -= OnGameTransition;
        }
        
        private void OnGameTransition()
        {
            if (_isTransitioning) return;
            _isTransitioning = true;

            gameSavingEvent.Invoke();
            gameLoadingEvent.Invoke();
            SceneManager.LoadSceneAsync(nextLevelName);
        }
    }
}