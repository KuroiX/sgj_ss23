using UnityEngine;

namespace Controller.Characters
{
    
    [CreateAssetMenu(fileName = "GostStats", menuName = "ScriptableObject/Stats/Gost", order = 100)]
    public class GostStats1 : DefaultStats
    {
        public float velocity;
        public GameObject projectile;
    }
    
}
