using System;
using UnityEngine;

namespace Controller.Characters
{
    
    public abstract class DefaultActions : MonoBehaviour
    {
        
        public InputInterface Input { get; set; }
        
        [SerializeField]
        public DefaultStats stats;

        private Rigidbody2D _rigidbody2D;
        private float ab1cooldown;
        private float ab2cooldown;
        private float lastAb1 = float.MinValue;
        private float lastAb2 = float.MinValue;
        
        private GameObject player;
        private PlayerHealth _playerHealth;

        private int _currentHealth;


        private void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            player = GameObject.Find("Player");
            _playerHealth = player.GetComponent<PlayerHealth>();
            _currentHealth = stats.health;
        }

        private void FixedUpdate()
        {
            float t = Time.time;

            //Debug.Log("Input: "+Input);
            //Debug.Log("MoveDirection: "+Input.MoveDirection);
            
            if (Input.MoveDirection.magnitude > 0)
            {
                Move(Input.MoveDirection);
            }

            if (Input.Ability1Direction.magnitude > 0)
            {
                if (t > lastAb1 + ab1cooldown)
                {
                    Ability1(Input.Ability1Direction);
                    lastAb1 = t;
                    //Tell UI ability 1 was used
                    Input.Ability1Direction = new Vector2(0f, 0f);
                }
            }

            if (Input.Ability2Direction.magnitude > 0)
            {
                if (t > lastAb2 + ab2cooldown)
                {
                    Ability2(Input.Ability2Direction);
                    lastAb2 = t;
                    //Tell UI ability 2 was used
                    Input.Ability2Direction = new Vector2(0f, 0f);
                }
            }

        }

        private void Move(Vector2 direction)
        {
            _rigidbody2D.MovePosition((Vector2)transform.position + direction * stats.speed * 0.05f);
        }

        protected void Switch<T>() where T: DefaultActions
        {
            _currentHealth -= 1; //todo custom damage for each enemy
            if (Input.GetType() == typeof(PlayerInput))
            {
                _playerHealth.TakeDamage(1, 1);
            }
            if (_currentHealth <= 0)
            {
                DefaultActions oldAction = player.GetComponent<DefaultActions>();
                Destroy(oldAction);

                player.AddComponent<T>();
                player.GetComponent<T>().stats = stats;
                // TODO: fix null reference when killing gengar as gengar
                player.GetComponent<T>().Input = player.GetComponent<InputInterface>();
                player.GetComponent<PlayerHealth>().UpdateKillCount();
                Destroy(gameObject);
                player.GetComponent<PlayerHealth>().UpdateHealth();
            }

        }
        
        public abstract void OnHit();
        
        protected abstract void Ability1(Vector2 direction);

        protected abstract void Ability2(Vector2 direction);
        
    }
    
}
