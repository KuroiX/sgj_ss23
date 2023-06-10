using System;
using UnityEngine;

namespace Controller.Characters
{
    
    public abstract class DefaultActions : MonoBehaviour
    {
        
        public InputInterface Input { get; set; }
        
        [SerializeField]
        protected DefaultStats stats;

        private CharacterController _controller;

        private void Start()
        {
            _controller = GetComponent<CharacterController>();
        }

        private void FixedUpdate()
        {
            if (Input.MoveDirection.magnitude > 0)
            {
                Move(Input.MoveDirection);
            }

            if (Input.Ability1Direction.magnitude > 0)
            {
                Ability1(Input.Ability1Direction);
                Input.Ability1Direction = new Vector2(0f, 0f);
            }

            if (Input.Ability2Direction.magnitude > 0)
            {
                Ability2(Input.Ability2Direction);
                Input.Ability2Direction = new Vector2(0f, 0f);
            }

        }


        private void Move(Vector2 direction)
        {

            _controller.Move(direction * stats.speed);
            
        }

        protected void Switch<T>() where T: DefaultActions
        {

            stats.health -= 1;
            if (stats.health <= 0)
            {
                GameObject player = GameObject.Find("Player");
                DefaultActions oldAction = player.GetComponent<DefaultActions>();
                Destroy(oldAction);

                player.AddComponent<T>();
                player.GetComponent<T>().stats = stats;
                // TODO: fix null reference when killing gengar as gengar
                player.GetComponent<T>().Input = player.GetComponent<InputInterface>();
                Destroy(gameObject);
            }

        }
        
        public abstract void OnHit();
        
        protected abstract void Ability1(Vector2 direction);

        protected abstract void Ability2(Vector2 direction);
        
    }
    
}
