using System;
using UnityEngine;

namespace Controller.Characters
{
    
    public class GengarActions : DefaultActions
    {
        private void Awake()
        {
            actionIndex = 2;
        }

        public override void OnHit(int damage, bool enemyAbility)
        {
            takeDamage<GengarActions>(damage, enemyAbility);
        }

        protected override void Ability1(Vector2 direction)
        {
            float angle = Vector2.Angle(Vector2.right, direction);

            float angleUp = angle + ((GengarStats1)stats).angleOffset;
            float angleDown =  angle - ((GengarStats1)stats).angleOffset;
            

            Vector2 upDir = new Vector2(Mathf.Cos(angleUp * Mathf.Deg2Rad), Mathf.Sin(angleUp* Mathf.Deg2Rad));
            Vector2 downDir = new Vector2(Mathf.Cos(angleDown* Mathf.Deg2Rad), Mathf.Sin(angleDown* Mathf.Deg2Rad));

            if (direction.y < 0)
            {
                upDir.y = -upDir.y;
                downDir.y = -downDir.y;
            }
            Quaternion lookQuaternion = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, -90) * direction);

            // Debug.Log(gameObject + " SHOOTS PROJECTILE at " + direction + "!");
            Transform parentTransform = transform.parent;
            GameObject projectileInstance = Instantiate(((GengarStats1) stats).projectile,
                transform.position + new Vector3(direction.x * parentTransform.localScale.x * 1.0f, direction.y * parentTransform.localScale.y * 1.0f),
                lookQuaternion);
            
            GameObject projectileInstanceUp = Instantiate(((GengarStats1) stats).projectile,
                transform.position + new Vector3(upDir.x * parentTransform.localScale.x * 1.0f, upDir.y * parentTransform.localScale.y * 1.0f),
                lookQuaternion);
            
            GameObject projectileInstanceDown = Instantiate(((GengarStats1) stats).projectile,
                transform.position + new Vector3(downDir.x * parentTransform.localScale.x * 1.0f, downDir.y * parentTransform.localScale.y * 1.0f),
                lookQuaternion);

            projectileInstance.GetComponent<Projectile>().damage = stats.damage;
            projectileInstanceUp.GetComponent<Projectile>().damage = stats.damage;
            projectileInstanceDown.GetComponent<Projectile>().damage = stats.damage;
            
            projectileInstance.GetComponent<Projectile>().SetVelocity(direction * ((GengarStats1) stats).velocity);
            projectileInstanceUp.GetComponent<Projectile>().SetVelocity(upDir * ((GengarStats1) stats).velocity);
            projectileInstanceDown.GetComponent<Projectile>().SetVelocity(downDir * ((GengarStats1) stats).velocity);
            if (!CompareTag("Player"))
            {
                projectileInstance.tag = "EnemyAbility";
                projectileInstanceUp.tag = "EnemyAbility";
                projectileInstanceDown.tag = "EnemyAbility";
            }
        }
    
        protected override void Ability2(Vector2 direction)
        {
            
            Debug.Log(gameObject + ": Ranged Ability2");
            
        }
        
    }
    
}
