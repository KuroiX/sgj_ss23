﻿using UnityEngine;

namespace Controller.Characters
{
    
    public class GengarActions : DefaultActions
    {
        
        public override void OnHit()
        {
            Switch<GengarActions>();
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
            
            Debug.Log(gameObject + " SHOOTS PROJECTILE at " + direction + "!");
            GameObject projectileInstance = Instantiate(((GengarStats1) stats).projectile,
                transform.position + new Vector3(direction.x * transform.localScale.x, direction.y * transform.localScale.y),
                Quaternion.identity);
            
            GameObject projectileInstanceUp = Instantiate(((GengarStats1) stats).projectile,
                transform.position + new Vector3(upDir.x * transform.localScale.x, upDir.y * transform.localScale.y),
                Quaternion.identity);
            
            GameObject projectileInstanceDown = Instantiate(((GengarStats1) stats).projectile,
                transform.position + new Vector3(downDir.x * transform.localScale.x, downDir.y * transform.localScale.y),
                Quaternion.identity);
            
            projectileInstance.GetComponent<Projectile>().SetVelocity(direction * ((GengarStats1) stats).velocity);
            projectileInstanceUp.GetComponent<Projectile>().SetVelocity(upDir * ((GengarStats1) stats).velocity);
            projectileInstanceDown.GetComponent<Projectile>().SetVelocity(downDir * ((GengarStats1) stats).velocity);
        }
    
        protected override void Ability2(Vector2 direction)
        {
            
            Debug.Log(gameObject + ": Ranged Ability2");
            
        }
        
    }
    
}
