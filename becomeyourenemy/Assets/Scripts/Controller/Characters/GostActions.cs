using System;
using UnityEngine;

namespace Controller.Characters
{
    
    public class GostActions : DefaultActions
    {
        private void Awake()
        {
            actionIndex = 1;
        }

        public override void OnHit(int damage, bool enemyAbility)
        {
            takeDamage<GostActions>(damage, enemyAbility);
        }

        protected override void Ability1(Vector2 direction)
        {
            
            //Debug.Log(gameObject + " SHOOTS PROJECTILE at " + direction + "!");
            Transform parentTransform = transform.parent;
            Quaternion lookQuaternion = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, -90) * direction);
            GameObject projectileInstance = Instantiate(((GostStats1) stats).projectile,
                transform.position + new Vector3(direction.x * parentTransform.localScale.x * 1.0f, direction.y * parentTransform.localScale.y * 1.0f),
                lookQuaternion);
            projectileInstance.GetComponent<Projectile>().damage = stats.damage;
            projectileInstance.GetComponent<Projectile>().SetVelocity(direction * ((GostStats1) stats).velocity);

        }
    
        protected override void Ability2(Vector2 direction)
        {
            
            Debug.Log(gameObject + ": Ranged Ability2");
            
        }
        
    }
    
}
