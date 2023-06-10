using UnityEngine;

namespace Controller
{
    public class CharacterMelee : Character
    {

        protected override void Move(Vector2 direction)
        {

            Controller.Move(direction * speed);
            
        }

        protected override void Ability1(Vector2 direction)
        {
            
            Debug.Log(gameObject + " SLASHES for " + damage + " Damage!");
            
        }
    
        protected override void Ability2(Vector2 direction)
        {
            
            Debug.Log(gameObject + ": Melee Ability2");
            
        }

    }
}
