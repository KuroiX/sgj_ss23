using UnityEngine;

public class CharController : MonoBehaviour
{
    
    public InputInterface Input { get; set; }

    private CharacterController _controller;

    private void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        _controller.Move(Input.MoveDirection);
    }

}
