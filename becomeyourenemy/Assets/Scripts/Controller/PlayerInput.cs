using Controller.Characters;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Controller
{
    public class PlayerInput : MonoBehaviour, InputInterface
    {
    
        public Vector2 MoveDirection { get; set; }
        public Vector2 Ability1Direction { get; set; }
        public Vector2 Ability2Direction { get; set; }
    
        public InputSystem _characterInput;

        private void Awake()
        {
            _characterInput = new InputSystem();
            _characterInput.Keyboard.Enable();
            _characterInput.Mouse.Enable();
        }

        private void Start()
        {
            // TODO
            GetComponentInChildren<DefaultActions>().Input = this;
        }

        private void OnEnable()
        {
            _characterInput.Keyboard.Move.performed += MoveOnPerformed;
            _characterInput.Keyboard.Move.canceled += MoveOnPerformed;
        
            _characterInput.Mouse.Ability1.performed += Ability1OnPerformed;
            _characterInput.Mouse.Ability1.canceled += Ability1OnCanceled;
            _characterInput.Mouse.Ability2.performed += Ability2OnPerformed;
            _characterInput.Mouse.Ability2.canceled += Ability2OnCanceled;
        }

        private void OnDisable()
        {
            _characterInput.Keyboard.Move.performed -= MoveOnPerformed;
            _characterInput.Keyboard.Move.canceled -= MoveOnPerformed;
        
            _characterInput.Mouse.Ability1.performed -= Ability1OnPerformed;
            _characterInput.Mouse.Ability1.canceled -= Ability1OnCanceled;
            _characterInput.Mouse.Ability2.performed -= Ability2OnPerformed;
            _characterInput.Mouse.Ability1.canceled -= Ability2OnCanceled;

        }

        private void MoveOnPerformed(InputAction.CallbackContext obj)
        {
            MoveDirection = obj.ReadValue<Vector2>();
        }

        private void Ability1OnPerformed(InputAction.CallbackContext obj)
        {
            Ability1Direction = ((Vector2) (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
        }

        private void Ability1OnCanceled(InputAction.CallbackContext obj)
        {
            Ability1Direction = Vector2.zero;
        }

        private void Ability2OnPerformed(InputAction.CallbackContext obj)
        {
            Ability2Direction = ((Vector2) (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position)).normalized;
        }
        
        private void Ability2OnCanceled(InputAction.CallbackContext obj)
        {
            Ability1Direction = Vector2.zero;
        }


    }
}
