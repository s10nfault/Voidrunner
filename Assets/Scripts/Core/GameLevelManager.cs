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
        [SerializeField] private VoidEventSO gameRestartEvent;
        
        [Header("Broadcasted Events")]
        [SerializeField] private VoidEventSO gameSavingEvent;
        [SerializeField] private VoidEventSO gameLoadingEvent;
        
        private bool _isTransitioning;
        
        private void OnEnable()
        {
            gameTransitionEvent.OnEvent += OnGameTransition;
            gameRestartEvent.OnEvent += OnGameRestart;
        }

        private void OnDisable()
        {
            gameTransitionEvent.OnEvent -= OnGameTransition;
            gameRestartEvent.OnEvent -= OnGameRestart;
        }
        
        private void OnGameRestart()
        {
            if (_isTransitioning) return;

            _isTransitioning = true;

            try
            {
                gameSavingEvent.Invoke();
                gameLoadingEvent.Invoke();
                
                var currentSceneName = SceneManager.GetActiveScene().name;
                var operation = SceneManager.LoadSceneAsync(currentSceneName);

                if (operation == null)
                {
                    _isTransitioning = false;
                    Debug.LogError(
                        $"Failed to start loading scene '{currentSceneName}'.",
                        this);
                }
            }
            catch (System.Exception e)
            {
                _isTransitioning = false;
                Debug.LogException(e, this);
            }
        }
        
        private void OnGameTransition()
        {
            if (_isTransitioning) return;

            if (string.IsNullOrWhiteSpace(nextLevelName) ||
                !Application.CanStreamedLevelBeLoaded(nextLevelName))
            {
                Debug.LogError(
                    $"Cannot load scene '{nextLevelName}'. Check the scene configuration.",
                    this);
                return;
            }

            _isTransitioning = true;

            try
            {
                gameSavingEvent.Invoke();
                gameLoadingEvent.Invoke();

                var operation = SceneManager.LoadSceneAsync(nextLevelName);

                if (operation == null)
                {
                    _isTransitioning = false;
                    Debug.LogError(
                        $"Failed to start loading scene '{nextLevelName}'.",
                        this);
                }
            }
            catch (System.Exception e)
            {
                _isTransitioning = false;
                Debug.LogException(e, this);
            }
        }
    }
}