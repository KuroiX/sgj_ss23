using System;
using System.Collections;
using System.Collections.Generic;
using Controller.Characters;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    private float _lifeTime;
    private float _currentlifeTime;

    public void SetLifeTime(float lifeTime)
    {
        this._lifeTime = lifeTime;
    }
    
    private void Start()
    {
        player = GameObject.Find("Player");
        //TODO maybe anders?
        gameObject.transform.parent = player.transform;
    }

    private void Update()
    {
        _currentlifeTime += Time.deltaTime;

        if (_currentlifeTime >= _lifeTime)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            other.gameObject.GetComponent<DefaultActions>().OnHit();
        }
    }
}
