using UnityEngine;

namespace Controller.Characters
{
    
    [CreateAssetMenu(fileName = "FroschStats", menuName = "ScriptableObject/Stats/Frosch", order = 100)]
    public class FroschStats1 : DefaultStats
    {
        public float dashTime;
        public float dashSpeed;
        public float stompLifeTime;
        public float stompRadius;
        public GameObject stompObject;
    }
    
}
