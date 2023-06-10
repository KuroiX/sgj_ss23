using Controller.Characters;
using UnityEngine;

namespace Controller
{
    public class AIInputDummy : MonoBehaviour, InputInterface
    {
    
        public Vector2 MoveDirection { get; set; }
        public Vector2 Ability1Direction { get; set; }
        public Vector2 Ability2Direction { get; set; }

        private void Start()
        {
            // TODO
            GetComponent<DefaultActions>().Input = this;
        }

    }
}
