using UnityEngine;

namespace Controller.Characters
{
    
    [CreateAssetMenu(fileName = "ShroomStats", menuName = "ScriptableObject/Stats/Shroom", order = 100)]
    public class ShroomStats1 : DefaultStats
    {
        public float lifeTime;
        public GameObject meleeAttack;
    }
    
}
