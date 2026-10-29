using Core;
using EventSO;
using UnityEngine;

namespace Test.Core
{
    public class GameStateLogger : MonoBehaviour
    {
        [Header("Received Events")] [SerializeField]
        GameStateEventSO gameStateEvent;

        private void OnEnable()
        {
            gameStateEvent.OnEvent += OnGameStateChanged;
        }

        private void OnDisable()
        {
            gameStateEvent.OnEvent -= OnGameStateChanged;
        }

        private void OnGameStateChanged(GameState state)
        {
            Debug.Log($"Game State Changed: {state}");
        }
    }
}