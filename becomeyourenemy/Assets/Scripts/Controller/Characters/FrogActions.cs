using UnityEngine;

namespace Controller.Characters
{
    
    public class FrogActions : DefaultActions
    {
        
        public override void OnHit()
        {
            Switch<FrogActions>();
        }

        protected override void Ability1(Vector2 direction)
        {
            Debug.Log(gameObject + " SLASHES for " + stats.damage + " Damage!");
            Quaternion lookQuaternion = Quaternion.LookRotation(Vector3.forward, Quaternion.Euler(0, 0, 120) * direction);
            GameObject meleeInstance = Instantiate(((ShroomStats1) stats).meleeAttack,
                transform.position + new Vector3(direction.x * transform.localScale.x * 1.5f, direction.y * transform.localScale.y * 1.5f),
                lookQuaternion);
            meleeInstance.GetComponent<MeleeAttack>().SetParent(this.transform);

            //GameObject meleeInstance = Instantiate(((FrogStats1)stats).meleeAttack, Vector3.zero, Quaternion.identity);
            //Debug.Log("MeleeInstance: " + meleeInstance);
            meleeInstance.GetComponent<MeleeAttack>().SetLifeTime(((ShroomStats1)stats).lifeTime);
        }
    
        protected override void Ability2(Vector2 direction)
        {
            
            Debug.Log(gameObject + ": Melee Ability2");
            
        }

    }
    
}
