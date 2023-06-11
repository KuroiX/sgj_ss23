using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    private Vector3 _prevPos;
    private Animator _animator;
    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _prevPos = Vector3.zero;
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        _prevPos = transform.position;
    }

    void LateUpdate()
    {
        if (transform.position.x != _prevPos.x || transform.position.z != _prevPos.z)
        {
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }

        if (transform.position.x < _prevPos.x)
        {
            _spriteRenderer.flipX = false;
        }
        
        if (transform.position.x > _prevPos.x)
        {
            _spriteRenderer.flipX = true;
        }

        //_prevPos = transform.position;
    }
}
