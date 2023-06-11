using UnityEngine;

namespace Controller
{
    public interface InputInterface
    {
    
        public Vector2 MoveDirection { get; set; }
        public Vector2 Ability1Direction { get; set; }
        public Vector2 Ability2Direction { get; set; }

    }
}
