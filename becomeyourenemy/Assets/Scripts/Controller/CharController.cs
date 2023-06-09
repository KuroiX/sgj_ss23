using UnityEngine;

public abstract class CharController : MonoBehaviour
{
    
    public InputInterface Input { get; set; }

    protected CharacterController Controller;

    private void Start()
    {
        Controller = GetComponent<CharacterController>();
    }

    private void FixedUpdate()
    {
        Move(Input.MoveDirection);
    }

    protected abstract void Move(Vector2 direction);

    protected abstract void Ability1();
    
    protected abstract void Ability2();

}
