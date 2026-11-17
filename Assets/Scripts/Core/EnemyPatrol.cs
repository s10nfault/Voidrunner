using EventSO;
using UnityEngine;

namespace Core
{
    public class EnemyPatrol : MonoBehaviour
    { 
        [Header("Boundaries and Speed")]
        [SerializeField] [Min(0)] private float leftBoundary;
        [SerializeField] [Min(0)] private float rightBoundary;
        [SerializeField] [Min(0)] private float speed;
        
        [Header("Received Events")]
        [SerializeField] GameStateEventSO gameStateEventSO;
        
        private float _initialPosition;
        private float _currentPosition;
        
        private int _currentDirection = 1; // 1 for right, -1 for left
        
        private float _fixedLeftBoundary;
        private float _fixedRightBoundary;

        private bool _isStopMovement;

        private void OnEnable()
        {
            if (gameStateEventSO == null)
                Debug.LogError("GameStateEventSO is not assigned in the inspector.");
            gameStateEventSO.OnEvent += OnGameStateEvent;
        }

        private void OnDisable()
        {
            if (gameStateEventSO != null)
                gameStateEventSO.OnEvent -= OnGameStateEvent;
        }

        private void OnGameStateEvent(GameState msg)
        {
            if (msg != GameState.Playing)
            {
                _isStopMovement = true;
            }
        }

        private void Start()
        {
            _currentPosition = transform.position.x;
            _initialPosition = transform.position.x;
            
            _fixedLeftBoundary = _initialPosition - leftBoundary;
            _fixedRightBoundary = _initialPosition + rightBoundary;
        }

        private void Update()
        {
            _fixedLeftBoundary = _initialPosition - leftBoundary;
            _fixedRightBoundary = _initialPosition + rightBoundary;

            if (_isStopMovement) return;
            
            _currentPosition += speed * Time.deltaTime * _currentDirection;
            
            if (_currentPosition <= _fixedLeftBoundary || _currentPosition >= _fixedRightBoundary)
            {
                _currentDirection *= -1;
            }
            
            transform.position = new Vector3(_currentPosition, transform.position.y, transform.position.z);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            
            Gizmos.DrawLine(new Vector3(_fixedLeftBoundary, transform.position.y, transform.position.z), 
                new Vector3(_fixedRightBoundary, transform.position.y, transform.position.z));
        }
    }
}