using Controller.Characters;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    private Vector3 _velocity;
    private int damage;

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }

    private void Start()
    {
        player = GameObject.Find("Player");
        Destroy(gameObject, 3);
    }

    private void FixedUpdate()
    {
        transform.position += _velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            other.gameObject.GetComponentInChildren<DefaultActions>().OnHit(damage, gameObject.CompareTag("EnemyAbility"));
            Destroy(gameObject);
        }
        
    }
    
}
