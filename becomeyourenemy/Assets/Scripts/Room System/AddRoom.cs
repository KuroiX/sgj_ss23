using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddRoom : MonoBehaviour
{
    //Entry room and Boss room should not have more enemies!
    public bool hasNoEnemies;

    private RoomTemplates templates;

    //Do we need this even if we use OnInit() and OnFinish()
    private bool active;

    public GameObject placeholder;

    //TODO:Enemies Scripts?
    private List<GameObject> enemies = new List<GameObject>();

    void Start()
    {
        //TODO: Spawn Obstacles and enemies
        for(int i = 0; i < Random.Range(1, 5); i++)
        {
            enemies.Add(new GameObject());
            //Instantiate(placeholder, new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y + Random.Range(-2.5f, 2.5f), 0), Quaternion.identity, this.gameObject.transform);
        }

        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);

        for(int i = 0; i < enemies.Count; i++)
        {
            enemies.Remove(enemies[i]);
        }
    }

    void OnInit()
    {
        foreach(GameObject go in enemies)
        {
            Instantiate(placeholder, new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y + Random.Range(-2.5f, 2.5f), 0), Quaternion.identity, this.gameObject.transform);

        }
    }

    void OnFinish()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") OnInit();
    }
}
