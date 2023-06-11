using System;
using UnityEngine;

namespace Controller.Characters
{
    
    public abstract class DefaultActions : MonoBehaviour
    {

        public InputInterface Input { get; set; }
        
        [SerializeField]
        public DefaultStats stats; // todo stats need ability cooldowns
        
        protected Rigidbody2D _rigidbody2D;

        private float lastAb1 = float.MinValue;
        private float lastAb2 = float.MinValue;
        
        private GameObject player;

        private int _currentHealth;

        [Header("Enemy Specific")]
        [SerializeField] private EnemyHealth health;

        private RoomBehaviour _room;
        private int _roomSpawnIndex;
        
        
        private AbilityCooldown ability1Cooldown;
        private AbilityCooldown ability2Cooldown;

        private float ability1Countdown;
        private float ability2Countdown;

        public int actionIndex;

        [SerializeField] private GameObject particles;
        

        private void Start()
        {
            _rigidbody2D = GetComponentInParent<Rigidbody2D>();
            player = GameObject.Find("Player");
            ability1Cooldown = GameObject.Find("GreyOut1").GetComponent<AbilityCooldown>();
            //ability2Cooldown = GameObject.Find("GreyOut2").GetComponent<AbilityCooldown>();
            _currentHealth = stats.health;
            ability1Countdown = 0;
            ability2Countdown = 0;
            //GameObject.Find("UI").GetComponent<UIManager>().SwitchAbility(actionIndex);
        }

        private void Update()
        {
            
            Move(Input.MoveDirection);
            
            if (ability1Countdown > 0) 
                ability1Countdown -= Time.deltaTime;
            if (ability2Countdown > 0) 
                ability2Countdown -= Time.deltaTime;
            if (Input.Ability1Direction.magnitude > 0 && ability1Countdown <= 0)
            {
                ability1Countdown = stats.ability1Cooldown;
                ability1Countdown -= Time.deltaTime;
                Ability1(Input.Ability1Direction);
                Input.Ability1Direction = new Vector2(0f, 0f);
                if (Input.GetType() == typeof(PlayerInput))
                {
                    ability1Cooldown.StartCooldown(stats.ability1Cooldown);
                }
            }

            /*if (Input.Ability2Direction.magnitude > 0 && ability1Countdown <= 0)
            {
                ability2Countdown = stats.ability2Cooldown;
                ability2Countdown -= Time.deltaTime;
                Ability2(Input.Ability2Direction);
                if (Input.GetType() == typeof(PlayerInput))
                {
                    ability2Cooldown.StartCooldown(stats.ability2Cooldown);
                }
            }*/

        }

        private void Move(Vector2 direction)
        {
            _rigidbody2D.velocity = direction * stats.speed;
        }

        protected void takeDamage<T>(int damage, string whoAmI) where T: DefaultActions
        {
            _currentHealth -= damage; 
            
            MusicAndSound.Instance.PlayEnemyHit();
            
            if (particles != null)
                particles.GetComponent<ParticleSystem>().Play();
            Debug.Log(damage);//todo custom damage for each enemy
            if (Input.GetType() == typeof(PlayerInput))
            {
                player.GetComponentInParent<PlayerHealth>().TakeDamage(damage, 1);
            }
            else
            {
                health.TakeDamage(damage,1);
            }
            if (_currentHealth <= 0)
            {
                player.GetComponent<PlayerHealth>().UpdateHealth();
                player.GetComponent<PlayerHealth>().UpdateKillCount();
                ability1Cooldown = GameObject.Find("GreyOut1").GetComponent<AbilityCooldown>();
                //ability2Cooldown = GameObject.Find("GreyOut2").GetComponent<AbilityCooldown>();
                
                _currentHealth = stats.health;
                Input = player.GetComponent<InputInterface>();
                _rigidbody2D = player.GetComponent<Rigidbody2D>();

                GameObject enemyParent = transform.parent.gameObject;
                GameObject playerChild = player.GetComponentInChildren<DefaultActions>().gameObject;
                
                transform.SetParent(player.transform);
                transform.localPosition = new Vector3(0f, 0f, 0f);
                
                GameObject.Find("UI").GetComponent<UIManager>().SwitchAbility(actionIndex);
                
                if (_room) _room.EnemyAtIndexWasKilled(_roomSpawnIndex);
                
                Destroy(playerChild);
                Destroy(enemyParent);

            }
        }
        
        public abstract void OnHit(int damage, bool enemyAbility);
        
        protected abstract void Ability1(Vector2 direction);

        protected abstract void Ability2(Vector2 direction);

        public void InjectRoom(RoomBehaviour room, int index)
        {
            _room = room;
            _roomSpawnIndex = index;
        }
        
    }
    
}
