using EventSO;
using UnityEngine;

namespace Core
{
    public class GameStateManager : MonoBehaviour
    {
        [Header("Received Events")]
        [SerializeField] private VoidEventSO gameOverEvent;
        [SerializeField] private VoidEventSO gameWinEvent;
        
        [Header("Broadcasted Events")]
        [SerializeField] private GameStateEventSO gameStateEvent;
        
        private GameState _currentGameState;

        private void Awake()
        {
            _currentGameState = GameState.Playing;
        }
        
        private void OnEnable()
        {
            gameOverEvent.OnEvent += OnGameOver;
            gameWinEvent.OnEvent += OnGameWin;
        }
        
        private void OnDisable()
        {
            gameOverEvent.OnEvent -= OnGameOver;
            gameWinEvent.OnEvent -= OnGameWin;
        }
        
        private void OnGameWin()
        {
            _currentGameState = GameState.Victory;
            gameStateEvent.Invoke(_currentGameState);
        }

        private void OnGameOver()
        {
            _currentGameState = GameState.Defeat;
            gameStateEvent.Invoke(_currentGameState);
        }
    }
}