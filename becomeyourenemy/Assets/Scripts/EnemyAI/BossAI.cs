using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BossAI : AIInput
{

        [SerializeField] private float idleRectangleSize;
        [SerializeField] private float secondAbilityProbability;
        
        private Vector2[] _idleCorners;
 
        private int _nextCorner;

        private bool _ability_one;
        
        protected override void Start()
        {
                base.Start();
                _idleCorners = new Vector2[4];
                float x = this.transform.position.x;
                float y = this.transform.position.y;
                
                _idleCorners[0].x = x + idleRectangleSize / 2;
                _idleCorners[1].x = x - idleRectangleSize / 2;
                _idleCorners[2].x = x + idleRectangleSize / 2;
                _idleCorners[3].x = x - idleRectangleSize / 2;
                
                _idleCorners[0].y = y + idleRectangleSize / 2;
                _idleCorners[1].y = y - idleRectangleSize / 2;
                _idleCorners[2].y = y - idleRectangleSize / 2;
                _idleCorners[3].y = y + idleRectangleSize / 2;

                _ability_one = true;
        }

        private void chooseNextCorner()
        {
                int corner;
                do
                { 
                        corner = Random.Range(0, 4);
                } while (corner == _nextCorner);

                _nextCorner = corner;
        }

        protected override Vector2 generateNextIdlePoint()
        {
                chooseNextCorner();
                return _idleCorners[_nextCorner];
        }
        
        protected override void performAttack(Vector2 vectorToPlayer)
        {
                if (_ability_one)
                {
                        
                        Ability1Direction = vectorToPlayer.normalized;
                        _ability_one = Random.Range(0.0f, 1.0f) > secondAbilityProbability;
                }
                else
                {
                        Ability2Direction = vectorToPlayer.normalized;
                        _ability_one = Random.Range(0.0f, 1.0f) <= secondAbilityProbability;
                }
        }
}
