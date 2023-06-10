using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

public class AnimationScriptFlipped : MonoBehaviour
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
        if (transform.position != _prevPos)
        {
            _animator.SetBool("IsWalking", true);
        }
        else
        {
            _animator.SetBool("IsWalking", false);
        }

        if (transform.position.x < _prevPos.x)
        {
            _spriteRenderer.flipX = true;
        }
        
        if (transform.position.x > _prevPos.x)
        {
            _spriteRenderer.flipX = false;
        }

        //_prevPos = transform.position;
    }
}
