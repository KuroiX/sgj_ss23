using System.Collections.Generic;
using UnityEngine;

public class RoomBehaviour : MonoBehaviour
{
    //Entry room and Boss room should not have more enemies!
    public bool hasNoEnemies;

    private RoomTemplates templates;

    //Do we need this even if we use OnInit() and OnFinish()
    private bool active;

    public GameObject enemyParent;

    public int enemyMelee;
    public int enemyRanged;

    public EnemyTypes placeholder;

    //TODO:Enemies Scripts?
    private List<Vector3> enemiesMeleeSP = new List<Vector3>();
    private List<Vector3> enemiesRangedSP = new List<Vector3>();

    private List<GameObject> remainingEnemies = new List<GameObject>();

    void Start()
    {
        templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        templates.rooms.Add(this.gameObject);

        //OnInit();
    }

    void OnInit()
    {
        if (hasNoEnemies) return;

        //TODO: Spawn Obstacles and enemies
        //for (int i = 0; i < Random.Range(1, 5); i++)
        for (int i = 0; i < enemyMelee; i++)
        {
            enemiesMeleeSP.Add(new Vector3(enemyParent.transform.position.x + Random.Range(-3.75f, 3.75f), enemyParent.transform.position.y + Random.Range(-3.75f, 3.75f), 0));
            //Instantiate(placeholder, new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y + Random.Range(-2.5f, 2.5f), 0), Quaternion.identity, this.gameObject.transform);
        }

        //for (int i = 0; i < Random.Range(1, 5); i++)
        for (int i = 0; i < enemyRanged; i++)
        {
            enemiesRangedSP.Add(new Vector3(enemyParent.transform.position.x + Random.Range(-3.75f, 3.75f), enemyParent.transform.position.y + Random.Range(-3.75f, 3.75f), 0));
            //Instantiate(placeholder.enemyType1, new Vector3(transform.position.x + Random.Range(-2.5f, 2.5f), transform.position.y + Random.Range(-2.5f, 2.5f), 0), Quaternion.identity, this.gameObject.transform);
        }

        SpawnEnemies(enemiesMeleeSP, enemiesRangedSP);
        //SpawnEnemies(enemyRanged, enemiesRangedSP);

    }

    void OnFinish()
    {
        remainingEnemies.Clear();
        //Delete enemies remaining under the GameObject
        foreach (Transform child in enemyParent.transform)
        {
            remainingEnemies.Add(child.gameObject);
            Destroy(child.gameObject);
        }
    }

    //Can also be an Enum to determine which type of enemy will be instantiated
    //Got rid of this because it is gonna be hardcoded at the end

    //void SpawnEnemies(int enemyType, List<Vector3> enemyList)
    //{
    //    foreach (Vector3 go in enemyList)
    //    {
    //        switch (enemyType)
    //        {
    //            case 0:
    //                Instantiate(placeholder.enemyType1, go, Quaternion.identity, enemyParent.transform);
    //                break;
    //            case 1:
    //                Instantiate(placeholder.enemyType2, go, Quaternion.identity, enemyParent.transform);
    //                break;
    //            //case 2:
    //            //    Instantiate(placeholder, go, Quaternion.identity, enemyParent.transform);
    //            //    break;
    //            default:
    //                Instantiate(placeholder.enemyType1, go, Quaternion.identity, enemyParent.transform);
    //                break;
    //        }
    //    }
    //}

    //Because maybe there are gonna be only one type of enemy per tier, i left it like this but it can be changed to different types

    void SpawnEnemies(List<Vector3> enemyMeleeList, List<Vector3> enemyRangedList)
    {
        foreach (Vector3 go in enemyMeleeList)
        {
            Instantiate(placeholder.moreEnemytypes[0], go, Quaternion.identity, enemyParent.transform);
        }

        foreach (Vector3 go in enemyMeleeList)
        {
            Instantiate(placeholder.moreEnemytypes[1], go, Quaternion.identity, enemyParent.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") OnInit();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player") OnFinish();
    }
}
