using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemyAI : MonoBehaviour
{
    private enum AIState
    {
        SEE, SEARCH, IDLE
    }

    private enum SEEState
    {
        CHASE, ATTACK, FLEE
    }
    
    [SerializeField] private AIState currentState;
    [SerializeField] private SEEState currSeeState;
    
    // Start is called before the first frame update
    void Start()
    {
        currentState = AIState.IDLE;
        currSeeState = SEEState.CHASE;
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState)
        {
            case AIState.IDLE: manageIdle(); break;
            case AIState.SEE: manageSee(); break;
            case AIState.SEARCH: manageSearch(); break;
        }
    }

    Vector2 dirToPlayer()
    {
        return Vector2.zero;
    }

    void manageIdle()
    {
        Vector2 dirToPlayer  = this.dirToPlayer();

        if (dirToPlayer != Vector2.zero)
        {
            this.currentState = AIState.SEE;
            return;
        }
        
        
        //do idle things
    }

    void manageSee()
    {
        Vector2 dirToPlayer = this.dirToPlayer();

        if (dirToPlayer == Vector2.zero)
        {
            this.currentState = AIState.IDLE;
            return;
        }

        switch (currSeeState)
        {
            
        }
    }

    void manageSearch()
    {
        
    }
}
