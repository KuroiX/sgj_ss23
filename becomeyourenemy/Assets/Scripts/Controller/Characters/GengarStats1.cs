using UnityEngine;

namespace Controller.Characters
{
    
    [CreateAssetMenu(fileName = "GengarStats", menuName = "ScriptableObject/Stats/Gengar", order = 100)]
    public class GengarStats1 : DefaultStats
    {
        
        public float velocity;
        public float angleOffset;
        public GameObject projectile;
        
    }
    
}
