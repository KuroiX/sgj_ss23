using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    private Vector3 _velocity;

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }

    private void FixedUpdate()
    {
        transform.position += _velocity;
    }
    
}
