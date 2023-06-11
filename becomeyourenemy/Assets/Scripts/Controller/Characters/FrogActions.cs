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
            
        }
    
        protected override void Ability2(Vector2 direction)
        {
            
            Debug.Log(gameObject + ": Melee Ability2");
            
        }

    }
    
}
