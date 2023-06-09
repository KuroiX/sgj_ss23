using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInput : MonoBehaviour, InputInterface
{
    
    public Vector2 MoveDirection { get; set; }
    public bool Ability1 { get; set; }
    public bool Ability2 { get; set; }
    
    private InputSystem _characterInput;

    private void Awake()
    {
        _characterInput = new InputSystem();
        _characterInput.Keyboard.Enable();
        _characterInput.Mouse.Enable();
    }

    private void OnEnable()
    {
        _characterInput.Keyboard.Move.performed += MoveOnPerformed;
        
        _characterInput.Mouse.Ability1.performed += Ability1OnPerformed;
        _characterInput.Mouse.Ability1.canceled += Ability1OnPerformed;
        _characterInput.Mouse.Ability2.performed += Ability2OnPerformed;
        _characterInput.Mouse.Ability2.canceled += Ability2OnPerformed;
    }

    private void OnDisable()
    {
        _characterInput.Keyboard.Move.performed -= MoveOnPerformed;
        
        _characterInput.Mouse.Ability1.performed -= Ability1OnPerformed;
        _characterInput.Mouse.Ability1.canceled += Ability1OnPerformed;
        _characterInput.Mouse.Ability2.performed -= Ability2OnPerformed;
        _characterInput.Mouse.Ability2.canceled += Ability2OnPerformed;
    }

    private void MoveOnPerformed(InputAction.CallbackContext obj)
    {
        MoveDirection = obj.ReadValue<Vector2>();
        Debug.Log(MoveDirection);
    }

    private void Ability1OnPerformed(InputAction.CallbackContext obj)
    {
        Ability1 = obj.ReadValueAsButton();
        Debug.Log(Ability1);
    }

    private void Ability2OnPerformed(InputAction.CallbackContext obj)
    {
        Ability2 = obj.ReadValueAsButton();
        Debug.Log(Ability2);
    }

}
