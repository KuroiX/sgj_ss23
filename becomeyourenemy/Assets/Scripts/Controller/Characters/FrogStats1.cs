using UnityEngine;

namespace Controller.Characters
{
    
    [CreateAssetMenu(fileName = "FrogStats", menuName = "ScriptableObject/Stats/Frog", order = 100)]
    public class FrogStats1 : DefaultStats
    {
        public float lifeTime;
        public GameObject meleeAttack;
    }
    
}
