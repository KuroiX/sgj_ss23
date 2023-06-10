using UnityEngine;

namespace Controller
{

    public abstract class Character : MonoBehaviour
    {

        [SerializeField]
        protected float speed;
        [SerializeField]
        protected float damage;

        public InputInterface Input { get; set; }

        protected CharacterController Controller;

        private void Start()
        {
            Controller = GetComponent<CharacterController>();
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

        protected abstract void Move(Vector2 direction);

        protected abstract void Ability1(Vector2 direction);

        protected abstract void Ability2(Vector2 direction);
        
    }
    
}
