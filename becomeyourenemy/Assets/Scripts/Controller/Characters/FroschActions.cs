
using System;
using System.Collections;
using UnityEngine;

namespace Controller.Characters
{
    public class FroschActions : DefaultActions
    {
        //[SerializeField] private GameObject stomp;
        [SerializeField] private Animator _animator; 
        private void Awake()
        {
            actionIndex = 3; //stomp
        }

        public override void OnHit(int damage, bool enemyAbility)
        {
            takeDamage<FroschActions>(damage, enemyAbility);
        }

        protected override void Ability1(Vector2 direction)
        {

            Debug.Log(gameObject + " jumps to " + direction + "!");

            StartCoroutine(Dash(direction));
        }
        
        private IEnumerator Dash(Vector2 direction)
        {
            float startTime = Time.time;
            Debug.Log("Dashing");
            while(Time.time < startTime + ((FroschStats1)stats).dashTime)
            {
                _rigidbody2D.position += direction.normalized * (((FroschStats1)stats).dashSpeed * Time.deltaTime);

                yield return null;
            }
            
            Debug.Log("Stomping");
            _animator.SetTrigger("StompTrigger");
            
            float radius = ((FroschStats1) stats).stompRadius;
            for (int i = 0; i < 8; i++)
            {
                float angle = i * Mathf.PI*2f / 8;
                Vector3 newPos = new Vector3(Mathf.Cos(angle)*radius, Mathf.Sin(angle)*radius);
                GameObject stompInstance = Instantiate(((FroschStats1) stats).stompObject, transform.position + newPos, Quaternion.identity);
                stompInstance.GetComponent<MeleeAttack>().SetParent(this.transform);
                stompInstance.GetComponent<MeleeAttack>().SetLifeTime(((FroschStats1)stats).stompLifeTime);
                stompInstance.GetComponent<MeleeAttack>().damage = stats.damage;
                if (!CompareTag("Player"))
                {
                    stompInstance.tag = "EnemyAbility";
                }
            }
        }

       
        protected override void Ability2(Vector2 direction)
        {
            

        }
    }
}