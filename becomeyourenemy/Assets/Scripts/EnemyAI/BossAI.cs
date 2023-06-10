using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAI : AIInput
{

        [SerializeField] private float rectSize;
        
        private Vector2[] _idleCorners;
 
        private int _nextCorner;
        
        protected void Start()
        {
                base.Start();
                _idleCorners = new Vector2[4];
                float x = this.transform.position.x;
                float y = this.transform.position.y;
                
                _idleCorners[0].x = x + rectSize / 2;
                _idleCorners[1].x = x - rectSize / 2;
                _idleCorners[2].x = x + rectSize / 2;
                _idleCorners[3].x = x - rectSize / 2;
                
                _idleCorners[0].y = y + rectSize / 2;
                _idleCorners[1].y = y - rectSize / 2;
                _idleCorners[2].y = y - rectSize / 2;
                _idleCorners[3].y = y + rectSize / 2;
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
}
