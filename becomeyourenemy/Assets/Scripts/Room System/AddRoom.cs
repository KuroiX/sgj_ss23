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

    public GameObject enemyParent;

    public int enemyMelee;
    public int enemyRanged;

    public GameObject placeholder;

    //TODO:Enemies Scripts?
    private List<Vector3> enemiesMeleeSP = new List<Vector3>();
    private List<Vector3> enemiesRangedSP = new List<Vector3>();

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);
    }

    void OnInit()
    {
        if (hasNoEnemies) return;

        //TODO: Spawn Obstacles and enemies
        for (int i = 0; i < Random.Range(1, 5); i++)
        {
            enemiesMeleeSP.Add(new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y + Random.Range(-2.5f, 2.5f), 0));
            //Instantiate(placeholder, new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y + Random.Range(-2.5f, 2.5f), 0), Quaternion.identity, this.gameObject.transform);
        }

        for (int i = 0; i < Random.Range(1, 5); i++)
        {
            enemiesRangedSP.Add(new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y + Random.Range(-2.5f, 2.5f), 0));
            //Instantiate(placeholder, new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y + Random.Range(-2.5f, 2.5f), 0), Quaternion.identity, this.gameObject.transform);
        }

        SpawnEnemies(enemyMelee, enemiesMeleeSP);
        SpawnEnemies(enemyRanged, enemiesRangedSP);

    }

    void OnFinish()
    {
        //Delete enemies remaining under the GameObject
        foreach(Transform child in enemyParent.transform)
        {
            Destroy(child.gameObject);
        }
    }

    //Can also be an Enum to determine which type of enemy will be instantiated
    void SpawnEnemies(int enemyType, List<Vector3> enemyList)
    {
        foreach (Vector3 go in enemyList)
        {
            switch(enemyType)
            {
                case 0:
                    Instantiate(placeholder, go, Quaternion.identity, enemyParent.transform);
                    break;
                case 1:
                    Instantiate(placeholder, go, Quaternion.identity, enemyParent.transform);
                    break;
                    case 2:
                    Instantiate(placeholder, go, Quaternion.identity, enemyParent.transform);
                    break;
                default:
                    Instantiate(placeholder, go, Quaternion.identity, enemyParent.transform);
                    break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") OnInit();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player") OnFinish();
    }
}
