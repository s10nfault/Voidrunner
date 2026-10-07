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
            gameStateEvent.Invoke(GameState.Victory);
        }

        private void OnGameOver()
        {
            gameStateEvent.Invoke(GameState.Defeat);
        }
    }
}