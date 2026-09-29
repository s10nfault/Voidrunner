using EventSO;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Core
{
    public class PlayerControlListener : MonoBehaviour, InputSystem.InputSystem.IPlayerActions
    {
        [SerializeField] private Vector2EventSO moveInput;
        [SerializeField] private Vector2EventSO lookInput;
        [SerializeField] private VoidEventSO attackInput;
        [SerializeField] private VoidEventSO interactInput;
        [SerializeField] private VoidEventSO crouchInput;
        [SerializeField] private VoidEventSO jumpInput;
        [SerializeField] private VoidEventSO previousInput;
        [SerializeField] private VoidEventSO nextInput;
        [SerializeField] private VoidEventSO sprintInput;

        private InputSystem.InputSystem _playerInputSystem;
        private InputSystem.InputSystem.PlayerActions _playerActions;
        
        private void Awake()
        {
            _playerInputSystem = new InputSystem.InputSystem();
            _playerActions = _playerInputSystem.Player;
            _playerActions.SetCallbacks(this);
        }

        private void OnDestroy()
        {
            _playerInputSystem.Dispose();
        }

        private void OnEnable()
        {
            _playerActions.Enable();
        }
        
        private void OnDisable()
        {
            _playerActions.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            moveInput?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            lookInput?.Invoke(context.ReadValue<Vector2>());
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            attackInput?.Invoke();
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            interactInput?.Invoke();
        }

        public void OnCrouch(InputAction.CallbackContext context)
        {
            crouchInput?.Invoke();
        }

        public void OnJump(InputAction.CallbackContext context)
        {
            jumpInput?.Invoke();
        }

        public void OnPrevious(InputAction.CallbackContext context)
        {
            previousInput?.Invoke();
        }

        public void OnNext(InputAction.CallbackContext context)
        {
            nextInput?.Invoke();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            sprintInput?.Invoke();
        }
    }
}