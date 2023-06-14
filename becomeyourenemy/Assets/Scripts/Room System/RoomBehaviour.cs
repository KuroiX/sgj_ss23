using System.Collections;
using System.Collections.Generic;
using Controller.Characters;
using Unity.VisualScripting;
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

    //TODO:Enemies Scripts?
    private List<Vector3> enemiesMeleeSP = new List<Vector3>();
    private List<Vector3> enemiesRangedSP = new List<Vector3>();

    [Header("Create empty Gameobjects under Spawnpoints and assign them here")]
    [Header("These are the positions where the enemies will spawn!")]
    public List<GameObject> enemies;
    public List<Transform> enemySpawnPositions;
    private bool[] _killedEnemies;

    private List<GameObject> remainingEnemies = new List<GameObject>();

    void Start()
    {
        //templates = GameObject.FindGameObjectWithTag("Rooms").GetComponent<RoomTemplates>();
        //templates.rooms.Add(this.gameObject);

        //OnInit();
        _killedEnemies = new bool[enemies.Count];
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

        //Random Positions!
        //SpawnEnemies(enemiesMeleeSP, enemiesRangedSP);
        //SpawnEnemies(enemyRanged, enemiesRangedSP);

        //Hardcoded Positions
        StartCoroutine(SpawnTimer());
        //SpawnEnemies();
    }

    void OnFinish()
    {
        StopAllCoroutines();
        
        remainingEnemies.Clear();
        //Delete enemies remaining under the GameObject
        foreach (Transform child in enemyParent.transform)
        {
            remainingEnemies.Add(child.gameObject);
            child.GetComponent<BossOnDestroy>()?.Disable();
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

    void SpawnEnemies()
    {
        //Debug.Log("Spawn?");
        for (int i = 0; i < enemies.Count; i++)
        {
            //Debug.Log("Enemy: " + i);
            if (_killedEnemies[i]) continue;
            
            GameObject gO = Instantiate(enemies[i], enemySpawnPositions[i].position, Quaternion.identity,
                enemyParent.transform);
            
            gO.transform.GetChild(0).GetComponent<DefaultActions>().InjectRoom(this, i);
            
            remainingEnemies.Add(gO);
        }
    }

    private IEnumerator SpawnTimer()
    {
        yield return new WaitForSeconds(0.1f);
        SpawnEnemies();
    }

    public void EnemyAtIndexWasKilled(int index)
    {
        _killedEnemies[index] = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        OnInit();
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        Debug.Log("Despawned/Killed");
        OnFinish();
    }
}
