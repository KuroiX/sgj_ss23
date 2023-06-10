using UnityEngine;

namespace Controller.Characters
{
    
    public class GostActions : DefaultActions
    {
        
        public override void OnHit()
        {
            Switch<GostActions>();
        }

        protected override void Ability1(Vector2 direction)
        {
            
            Debug.Log(gameObject + " SHOOTS PROJECTILE at " + direction + "!");
            GameObject projectileInstance = Instantiate(((GostStats1) stats).projectile,
                transform.position + new Vector3(direction.x * transform.localScale.x, direction.y * transform.localScale.y),
                Quaternion.identity);
            projectileInstance.GetComponent<Projectile>().SetVelocity(direction * ((GostStats1) stats).velocity);

        }
    
        protected override void Ability2(Vector2 direction)
        {
            
            Debug.Log(gameObject + ": Ranged Ability2");
            
        }
        
    }
    
}
