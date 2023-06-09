using Player;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBehaviour : MonoBehaviour, InputInterface
{
    
    private CharacterInput _characterInput;

    private void Awake()
    {
        _characterInput = new CharacterInput();
        _characterInput.Keyboard.Enable();
        _characterInput.Mouse.Enable();
    }

    private void OnEnable()
    {
        _characterInput.Keyboard.Move.performed += MoveOnperformed;
        _characterInput.Mouse.Ability1.performed += Ability1Onperformed;
        _characterInput.Mouse.Ability1.canceled += Ability1Onperformed;
        _characterInput.Mouse.Ability2.performed += Ability2Onperformed;
        _characterInput.Mouse.Ability2.canceled += Ability2Onperformed;
    }

    private void OnDisable()
    {
        _characterInput.Keyboard.Move.performed -= MoveOnperformed;
        _characterInput.Mouse.Ability1.performed -= Ability1Onperformed;
        _characterInput.Mouse.Ability1.canceled += Ability1Onperformed;
        _characterInput.Mouse.Ability2.performed -= Ability2Onperformed;
        _characterInput.Mouse.Ability2.canceled += Ability2Onperformed;
    }

    private void MoveOnperformed(InputAction.CallbackContext obj)
    {
        InputInterface.MoveDirection = obj.ReadValue<Vector2>();
    }

    private void Ability1Onperformed(InputAction.CallbackContext obj)
    {
        InputInterface.Ability1 = obj.ReadValueAsButton();
        Debug.Log(InputInterface.Ability1);
    }

    private void Ability2Onperformed(InputAction.CallbackContext obj)
    {
        InputInterface.Ability2 = obj.ReadValueAsButton();
        Debug.Log(InputInterface.Ability2);
    }
    
}
