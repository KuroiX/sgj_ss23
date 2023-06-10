using UnityEngine;

namespace Controller
{
    public class CharacterRanged : Character
    {
        
        [SerializeField]
        private GameObject projectile;

        [SerializeField]
        private float velocity;
        
        protected override void Move(Vector2 direction)
        {

            Controller.Move(direction * speed);

        }

        protected override void Ability1(Vector2 direction)
        {
            
            Debug.Log(gameObject + " SHOOTS PROJECTILE at " + direction + "!");
            GameObject projectileInstance = Instantiate(projectile, transform);
            projectileInstance.GetComponent<Projectile>().SetVelocity(direction * velocity);

        }
    
        protected override void Ability2(Vector2 direction)
        {
            
            Debug.Log(gameObject + ": Ranged Ability2");
            
        }
        
    }
    
}
