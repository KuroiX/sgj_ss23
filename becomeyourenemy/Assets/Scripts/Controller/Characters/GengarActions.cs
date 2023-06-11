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
            takeDamage<GengarActions>(damage, "Gengar");
        }

        protected override void Ability1(Vector2 direction)
        {
            MusicAndSound.Instance.PlayWave();
            
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
            Quaternion lookQuaternionUp = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, -90) * upDir);
            Quaternion lookQuaternionDown = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, -90) * downDir);

            // Debug.Log(gameObject + " SHOOTS PROJECTILE at " + direction + "!");
            Transform parentTransform = transform.parent;
            GameObject projectileInstance = Instantiate(((GengarStats1) stats).projectile,
                transform.position + new Vector3(direction.x * parentTransform.localScale.x * 1.0f, direction.y * parentTransform.localScale.y * 1.0f),
                lookQuaternion);
            
            GameObject projectileInstanceUp = Instantiate(((GengarStats1) stats).projectile,
                transform.position + new Vector3(upDir.x * parentTransform.localScale.x * 1.0f, upDir.y * parentTransform.localScale.y * 1.0f),
                lookQuaternionUp);
            
            GameObject projectileInstanceDown = Instantiate(((GengarStats1) stats).projectile,
                transform.position + new Vector3(downDir.x * parentTransform.localScale.x * 1.0f, downDir.y * parentTransform.localScale.y * 1.0f),
                lookQuaternionDown);

            projectileInstance.GetComponent<Projectile>().damage = stats.damage;
            projectileInstanceUp.GetComponent<Projectile>().damage = stats.damage;
            projectileInstanceDown.GetComponent<Projectile>().damage = stats.damage;
            
            projectileInstance.GetComponent<Projectile>().SetVelocity(direction * ((GengarStats1) stats).velocity);
            projectileInstanceUp.GetComponent<Projectile>().SetVelocity(upDir * ((GengarStats1) stats).velocity);
            projectileInstanceDown.GetComponent<Projectile>().SetVelocity(downDir * ((GengarStats1) stats).velocity);

            if (((GengarStats1)stats).manyBullets)
            {
                float angleUp_2 = angle + ((GengarStats1)stats).angleOffset/2;
                float angleDown_2 =  angle - ((GengarStats1)stats).angleOffset/2;
                Vector2 upDir_2 = new Vector2(Mathf.Cos(angleUp_2 * Mathf.Deg2Rad), Mathf.Sin(angleUp_2* Mathf.Deg2Rad));
            Vector2 downDir_2 = new Vector2(Mathf.Cos(angleDown_2* Mathf.Deg2Rad), Mathf.Sin(angleDown_2* Mathf.Deg2Rad));

            if (direction.y < 0)
            {
                upDir_2.y = -upDir_2.y;
                downDir_2.y = -downDir_2.y;
            }
            Quaternion lookQuaternionUp_2 = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, -90) * upDir_2);
            Quaternion lookQuaternionDown_2 = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, -90) * downDir_2);

            // Debug.Log(gameObject + " SHOOTS PROJECTILE at " + direction + "!");
            Transform parentTransform_2 = transform.parent;
            
            
            GameObject projectileInstanceUp_2 = Instantiate(((GengarStats1) stats).projectile,
                transform.position + new Vector3(upDir_2.x * parentTransform_2.localScale.x * 1.0f, upDir_2.y * parentTransform_2.localScale.y * 1.0f),
                lookQuaternionUp_2);
            
            GameObject projectileInstanceDown_2 = Instantiate(((GengarStats1) stats).projectile,
                transform.position + new Vector3(downDir_2.x * parentTransform_2.localScale.x * 1.0f, downDir_2.y * parentTransform_2.localScale.y * 1.0f),
                lookQuaternionDown_2);

            projectileInstanceUp_2.GetComponent<Projectile>().damage = stats.damage;
            projectileInstanceDown_2.GetComponent<Projectile>().damage = stats.damage;
            
            projectileInstanceUp_2.GetComponent<Projectile>().SetVelocity(upDir_2 * ((GengarStats1) stats).velocity);
            projectileInstanceDown_2.GetComponent<Projectile>().SetVelocity(downDir_2 * ((GengarStats1) stats).velocity);
            if (!CompareTag("Player"))
            {
               
                projectileInstanceUp_2.tag = "EnemyAbility";
                projectileInstanceDown_2.tag = "EnemyAbility";
            }
            
            }
            
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
