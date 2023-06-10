using System;
using System.Collections;
using System.Collections.Generic;
using Controller;
using Controller.Characters;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


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
    [SerializeField] protected SEEState currentSeeState; 
    [SerializeField] protected IDLEState currentIdleState;
    [SerializeField] protected float seeRange;
    [SerializeField] protected float chaseSpeed;
    [SerializeField] protected float attackRange;
    [SerializeField] protected float attackSpeed;
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float fleeRange;
    [SerializeField] protected float fleeSpeed;
    [SerializeField] protected float idleMoveRange;
    [SerializeField] protected float idleSpeed;
    [SerializeField] protected float searchSpeed;
    [SerializeField] protected float searchTime;
    [SerializeField] protected Transform playerTransform;
    [SerializeField] protected float actionsDelay;

    #region IdleRegion
    private Vector2 _idlePoint;
    protected float _idleTime;
    protected float _idleTimeToReach;
    protected bool _idleIsSet;
    private Vector2 _idlePointToReach;
    public Vector2 _idleDirection;
    private float _attackCooldownTime;
    private float _searchTime;
    private Vector2 _searchPosition;

    private float _rangeBuffer;
    #endregion

    protected virtual void Start()
    {
        GetComponent<DefaultActions>().Input = this;
        currentState = AIState.IDLE;
        currentSeeState = SEEState.CHASE;
        currentIdleState = IDLEState.WAIT;
        _idlePoint = transform.position;
        _idleIsSet = false;
        _attackCooldownTime = 0;
        _searchTime = 0;
        _searchPosition = Vector2.zero;
        _rangeBuffer = 0.1f;
    }

    private void Update()
    {
        if (playerIsDestroyed())
        {
            MoveDirection = Vector2.zero;
            return;
        }
        switch (currentState)
        {
            case AIState.IDLE: manageIdle(); break;
            case AIState.SEE: manageSee(); break;
            case AIState.SEARCH: manageSearch(); break;
        }
         _attackCooldownTime -= Time.deltaTime;
         _searchTime -= Time.deltaTime;
    }

    private bool playerIsDestroyed()
    {
        return ((object)playerTransform) != null && !playerTransform;
    }

    private Vector2 vectorToPlayer()
    {
        if (playerIsDestroyed())
        {
            Debug.LogError("Player is missing");
            return Vector2.zero;
        }
        return (Vector2) (playerTransform.position - this.transform.position) ;
    }

    private bool canSeePlayer()
    {
        Vector2 vectorToPlayer  = this.vectorToPlayer();
        if (vectorToPlayer.magnitude > seeRange)
        {
            return false;
        }
        var hit = Physics2D.Raycast(this.transform.position, vectorToPlayer.normalized, seeRange + _rangeBuffer);
        return !hit.transform.CompareTag("Obstacle");
    }

    private void manageIdle()
    {
        if (canSeePlayer())
        {
            this.currentState = AIState.SEE;
            this.currentSeeState = SEEState.CHASE;
            return;
        }
        
        //do idle things
        IdleMovement();
    }

    protected virtual Vector2 generateNextIdlePoint()
    {
        return new Vector2(Random.Range(_idlePoint.x - idleMoveRange, _idlePoint.x + idleMoveRange),
            Random.Range(_idlePoint.y - idleMoveRange, _idlePoint.y + idleMoveRange));
    }

    protected virtual void IdleMovement()
    {
        switch (currentIdleState)
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
                    currentIdleState = IDLEState.MOVE;
                }
                
                break;
            case IDLEState.MOVE:
                if (!_idleIsSet)
                {
                    Vector2 pointToReach = generateNextIdlePoint();
                    _idleDirection = (pointToReach - (Vector2)transform.position).normalized;
                    _idleTimeToReach = Random.Range(1, 3);
                    _idleIsSet = true;
                }

                //Move towards direction
                MoveDirection = _idleDirection * idleSpeed;
                //Move for time
                _idleTime += Time.deltaTime;
                
                
                //Point is reached
                if(_idleTime >= _idleTimeToReach || (_idlePointToReach - (Vector2)transform.position).magnitude <= 0.2f)
                {
                    _idleTime = 0;
                    _idleIsSet = false;
                    MoveDirection = Vector2.zero;
                    currentIdleState = IDLEState.WAIT;
                }
                
                break;
        }
    }
    

    private void manageSee()
    {
        if (!canSeePlayer())
        {
            this.currentState = AIState.SEARCH;
            _searchTime = searchTime;
            _searchPosition = (Vector2) this.playerTransform.position;
            return;
        }

        SeeMovement();
    }

    protected virtual void performAttack(Vector2 vectorToPlayer)
    {
        Ability1Direction = vectorToPlayer.normalized;
    }

    protected virtual void SeeMovement()
    {
        Vector2 vectorToPlayer = this.vectorToPlayer();
        switch (currentSeeState)
        {
            case SEEState.CHASE:
                
                MoveDirection = vectorToPlayer.normalized * chaseSpeed;

                if (vectorToPlayer.magnitude < attackRange - _rangeBuffer)
                {
                    this.currentSeeState = SEEState.ATTACK;
                    if (_attackCooldownTime < 0)
                    {
                        _attackCooldownTime = 0;
                    }
                    _attackCooldownTime += actionsDelay;
                }
                break;
            
            case SEEState.ATTACK:
                
                MoveDirection = vectorToPlayer.normalized * attackSpeed;

                if(_attackCooldownTime < 0)
                {
                    performAttack(vectorToPlayer);
                    _attackCooldownTime = attackCooldown;
                }

                
                if (vectorToPlayer.magnitude > attackRange + _rangeBuffer)
                {
                    this.currentSeeState = SEEState.CHASE;
                }else if (vectorToPlayer.magnitude < fleeRange - _rangeBuffer)
                {
                    this.currentSeeState = SEEState.FLEE;
                }
                break;
            
            case SEEState.FLEE:
                MoveDirection = vectorToPlayer.normalized  * (fleeSpeed * -1);

                if (vectorToPlayer.magnitude > fleeRange + _rangeBuffer)
                {
                    this.currentSeeState = SEEState.ATTACK;
                }
                
                break;
        }
    }

    private void manageSearch()
    {
        if (canSeePlayer())
        {
            this.currentState = AIState.SEE;
            this.currentSeeState = SEEState.CHASE;
            return;
        }
        
        Vector2 vectorToSearchPos = _searchPosition - (Vector2)this.transform.position;

        if (_searchTime < 0 || vectorToSearchPos.magnitude < 0.1)
        {
            this.currentState = AIState.IDLE;
            this.currentIdleState = IDLEState.WAIT;
            _idlePoint = transform.position;
            return;
        }
        
        SearchMovement();
    }

    protected virtual void SearchMovement()
    {
        Vector2 vectorToSearchPos = _searchPosition - (Vector2)this.transform.position;
        
        MoveDirection = vectorToSearchPos.normalized * searchSpeed;
    }
}
