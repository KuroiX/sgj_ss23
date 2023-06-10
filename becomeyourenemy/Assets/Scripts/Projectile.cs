using Controller.Characters;
using UnityEngine;

public class Projectile : MonoBehaviour
{

    [SerializeField]
    private GameObject player;
    private Vector3 _velocity;

    public void SetVelocity(Vector3 velocity)
    {
        _velocity = velocity;
    }

    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void FixedUpdate()
    {
        transform.position += _velocity;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Character"))
        {
            other.gameObject.GetComponent<DefaultActions>().OnHit();
            Destroy(gameObject);
        }
    }
    
}
