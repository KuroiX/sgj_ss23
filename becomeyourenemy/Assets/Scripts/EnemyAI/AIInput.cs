using System.Collections;
using System.Collections.Generic;
using Controller;
using Controller.Characters;
using UnityEngine;



public abstract class AIInput : MonoBehaviour, InputInterface
{
    public Vector2 MoveDirection { get; set; }
    public Vector2 Ability1Direction { get; set; }
    public Vector2 Ability2Direction { get; set; }
    
    protected enum AIState
    {
        SEE, SEARCH, IDLE
    }

    protected enum SEEState
    {
        CHASE, ATTACK, FLEE
    }

    protected enum IDLEState
    {
        MOVE, WAIT
    }
    
    [SerializeField] protected AIState currentState;
    [SerializeField] protected SEEState currSeeState;
    [SerializeField] protected IDLEState currIdleState;
    [SerializeField] protected float enemySpeed;
    [SerializeField] protected float seeRange;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float fleeRange;
    [SerializeField] protected float idleMoveRange;
    [SerializeField] protected float idleMoveSpeedFactor;
    [SerializeField] protected Transform playerTransform;


    #region IdleRegion
    private Vector2 _idlePoint;
    private float _idleTime;
    private float _idleTimeToReach;
    private bool _idleIsSet;
    private Vector2 _idlePointToReach;
    #endregion

    private void Start()
    {
        GetComponent<DefaultActions>().Input = this;
        currentState = AIState.IDLE;
        currSeeState = SEEState.CHASE;
        currIdleState = IDLEState.WAIT;
        _idlePoint = new Vector2(transform.position.x, transform.position.y);
        _idleIsSet = false;
    }

    private void Update()
    {
        switch (currentState)
        {
            case AIState.IDLE: manageIdle(); break;
            case AIState.SEE: manageSee(); break;
            case AIState.SEARCH: manageSearch(); break;
        }
    }

    private Vector2 dirToPlayer()
    {
        return Vector2.zero;
    }

    private void manageIdle()
    {
        Vector2 dirToPlayer  = this.dirToPlayer();

        if (dirToPlayer != Vector2.zero)
        {
            this.currentState = AIState.SEE;
            return;
        }
        
        //do idle things
        IdleMovement();
    }

    protected virtual void IdleMovement()
    {
        switch (currIdleState)
        {
            case IDLEState.WAIT:
                if (!_idleIsSet)
                {
                    _idleTimeToReach = Random.Range(1, 3);
                    _idleIsSet = true;
                }
                
                //Wait for Time
                _idleTime += Time.deltaTime;

                // Time is reached
                if (_idleTime >= _idleTimeToReach)
                {
                    _idleTime = 0;
                    _idleIsSet = false;
                    currIdleState = IDLEState.MOVE;
                }
                
                break;
            case IDLEState.MOVE:
                if (!_idleIsSet)
                {
                    _idlePointToReach =
                        new Vector2(Random.Range(_idlePoint.x - idleMoveRange, _idlePoint.x + idleMoveRange),
                            Random.Range(_idlePoint.y - idleMoveRange, _idlePoint.y + idleMoveRange));
                    _idleIsSet = true;
                }

                Debug.Log(_idlePointToReach);

                //Move to point
                Vector2 directionVector = (_idlePointToReach - (Vector2)transform.position).normalized;
                MoveDirection = directionVector * idleMoveSpeedFactor;
                
                
                //Point is reached
                if( (_idlePointToReach - (Vector2)transform.position).magnitude <= 0.2f)
                {
                    _idleIsSet = false;
                    MoveDirection = Vector2.zero;
                    currIdleState = IDLEState.WAIT;
                }
                
                break;
        }
    }
    

    private void manageSee()
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

    private void manageSearch()
    {
        
    }
}
