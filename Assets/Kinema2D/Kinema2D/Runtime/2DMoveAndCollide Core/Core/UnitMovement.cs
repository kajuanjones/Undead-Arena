using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class UnitMovement : MonoBehaviour
{

    [Header("Player MoveAndCollide2D")]
    public MoveAndCollide2D movement;
    

    public virtual float MoveSpeed()
    {
        return unitStats.baseMoveSpeed;
    }

    [Header("Input")]
    public Vector2 input;

    public UnitMoveStats unitStats;

    [Header("Player State")]
    public UnitMoveState currentMovementState;
    public UnitMoveState previousMoveState;
    public UnitMoveState idleState;

    public UnitMoveState defaultState;


    public bool flipped;

    public Vector2 move;
    //[Space(10)]

    // Start is called before the first frame update
    void Start()
    {
        idleState = new IdleState(this);
    }

    public virtual void ComputeVelocity()
    {
        
    }


    public virtual void SetMoveState(UnitMoveState state)
    {

        // if currentMovementState does not allow overrides, return

        if (currentMovementState != null)
        {
            currentMovementState.OnStateExit();
            previousMoveState = currentMovementState;
        }

        currentMovementState = state;


        if (currentMovementState != null)
        {
            currentMovementState.OnStateEnter();
        }
    }

    public virtual UnitMoveState GetNextState()
    {
        return idleState;
    }
    // Bad Coupling, Should Be Fixed, maybe should be called in Fixed Update, Decouple movestate updating and input reading.
    public void UpdateMoveState()
    {
        if (currentMovementState != null)
        {
            currentMovementState.Tick();
        }

        if (currentMovementState.finished)
        {
            SetMoveState(GetNextState()); // you can use this with a neutral state, AKA Free Move State, and then this coupled move state can branch to new ones.
        }

        move = Vector2.zero;

        // Move.x = currentMovementState.GetXVelocity();
        move.x = currentMovementState.GetXVelocity();
        // Move.y = currentMovementState.GetYVelocity();
        move.y = currentMovementState.GetYVelocity();

        movement.SetYOverride(currentMovementState.overridesYVelocity);
        movement.SetVelocity(move);
    }

    // Update is called once per frame // This Should Be An Update MoveState Method that is called in Update
    public virtual void Update()
    {
        
    }

    public virtual void FixedUpdate()
    {
        if (movement.velocity.x > 0)
        {
            flipped = false;
        } else if (movement.velocity.x < 0)
        {
            flipped = true;
        }
        UpdateMoveState();
    }

    public void SetYVelocity(float yVelocity)
    {
        this.movement.velocity.y = yVelocity;
    }

    public bool IsGrounded()
    {
        return movement.grounded;
    }
}

// Mind Control State, pass an in vector 2, pass inputs!??!

[System.Serializable]
public class UnitMoveState : State
{
    public bool overridesFlipDirection;
    public bool overridesStateChanges;

    public string stateName;

    public bool overridesYVelocity = false;
    protected UnitMovement unit;

    public bool finished;

    public UnitMoveState(UnitMovement _unit)
    {
        this.unit = _unit;
        this.stateName = this.GetType().ToString();
    }

    public virtual float GetXVelocity() { return 0f; }
    public virtual float GetYVelocity() { return 0f; }
}

public class IdleState : UnitMoveState
{
    public IdleState(UnitMovement unit) : base(unit)
    {

    }

    public override float GetXVelocity()
    {
        return 0f;
    }
}

public class KnockBackState : UnitMoveState
{
    private Vector2 initialLaunchVector;
    private float knockBackForce;

    private float initialXVelocity;
    private float stateVelocity;

    private float hitStun;

    private float lerpPercentage;
    
    public KnockBackState(UnitMovement unit, Vector2 launchVector, float knockBackForce, float hitStun = 0f) : base(unit)
    {
        this.knockBackForce = knockBackForce;
        initialLaunchVector = launchVector;

        initialXVelocity = launchVector.x * knockBackForce;

        this.hitStun = hitStun;

    }
    
    public override void OnStateEnter()
    {
        unit.SetYVelocity(initialLaunchVector.y * knockBackForce);
        overridesFlipDirection = true;

    }

    public override void Tick()
    {
        if (hitStun <= 0)
        {
            
            hitStun = Mathf.Min(0, hitStun -= Time.deltaTime);
            
            stateVelocity = Mathf.Lerp(initialXVelocity, (unit.input.x * unit.MoveSpeed()), lerpPercentage);

            lerpPercentage += Time.deltaTime;

            if (lerpPercentage >= 0.5f)
            {
                overridesFlipDirection = false; // return flip control back to momentum
            }

            if (lerpPercentage >= 1.0f) // || unit.IsGrounded()
            {
                unit.SetMoveState(unit.defaultState);
            }
        }
    }


    

    public override float GetXVelocity()
    {
        return stateVelocity;
    }
}

public class RunRightState : UnitMoveState
{

    public RunRightState(UnitMovement unit) : base (unit)
    {
        //this.unit = unit;
    }

    public override float GetXVelocity()
    {
        return 1;
    }
}

public class PatrolState : UnitMoveState
{
    int curDirection = 1;

    bool halfSpeed = false;

    public float patrolDuration = 2.5f;
    public float pauseDuration = 1.5f;

    public bool patrolling = true;

    public float curPatrol;
    public float curPause;

    public PatrolState(UnitMovement unit, bool slowWalk = false) : base(unit)
    {
        curPatrol = patrolDuration;
        curPause = pauseDuration;
        halfSpeed = slowWalk;
    }

    public override void Tick()
    {
        //THINK POINT: should this check be in here by default, at the very least, this shows how movement states can uitlize the object.
        if (!unit.movement.grounded)
        {
            finished = true;
            return;
        }

        if (patrolling)
        {
            curPatrol -= Time.deltaTime;
            if (curPatrol <= 0)
            {
                curPatrol = patrolDuration;
                patrolling = false;
                curDirection *= -1;
            }
        }

        else
        {
            curPause -= Time.deltaTime;
            if (curPause <= 0)
            {
                curPause = pauseDuration;
                patrolling = true;
            }
        }
    }

    public override float GetXVelocity()
    {

        float speed = curDirection * unit.MoveSpeed();

        if (!patrolling)
        {
            return 0f;
        }

        if (halfSpeed)
            speed = speed * 0.5f;

        return speed;
        
    }



}

// This has a lot of potential, maybe the example extended systems should be better designed to accomodate input passing.
// This would come in handy for the 'Squad' Demo, and would likely be a handy feature in general with a wide use case.
// This should definitely be looked into, a standardized input for unit Movement, so that they can be swapped.
public class MindControlState : UnitMoveState
{

    PlayerController pc;

    public MindControlState(UnitMovement unit, in PlayerController pc) : base (unit)
    {
        this.pc = pc;
    }

    public override float GetXVelocity()
    {
        
        return unit.MoveSpeed() * pc.input.x;
    }
}

public class FollowState : UnitMoveState
{
    public float buffer = 1;
    public Transform target;

    public Vector3 lastTargetPos;

    public FollowState(UnitMovement unit, Transform target) : base(unit)
    {
        this.target = target;
        lastTargetPos = target.transform.position;
    }

    public override float GetXVelocity()
    {

        float followSpeed = 0f;
        float deltaDistance = target.transform.position.x - lastTargetPos.x;

        followSpeed = Mathf.Abs(deltaDistance / Time.deltaTime);

        float xDistance = target.transform.position.x - unit.transform.position.x;
        float direction = 0f;

        if (xDistance > 1)
            direction = 1f;
        else if (xDistance < -1)
            direction = -1f;
        


        lastTargetPos = target.transform.position;

        followSpeed = Math.Min(followSpeed, unit.MoveSpeed());

        Debug.Log(xDistance);

        if (Mathf.Abs(xDistance) > 1.5)
        {
            followSpeed = unit.MoveSpeed();
        }

        return direction * Mathf.Abs(followSpeed);

    }
}


public class FleeState : UnitMoveState
{
    int direction;

    public float duration;
    // Seperate duration from curDuration?

    public FleeState(UnitMovement unit, int direction, int _duration = 2) : base (unit)
    {
        this.direction = direction;
        this.duration = _duration;
    }

    public override void OnStateEnter()
    {
     // TEMP CODE
        finished = false;

    }

    public override void Tick()
    {

        duration -= Time.deltaTime;

        if (duration <= 0)
        {
            duration = 0;
            unit.SetMoveState(unit.previousMoveState);
            finished = true;
        }
    }

    public override float GetXVelocity()
    {
        return unit.MoveSpeed() * direction; // unit
    }

}
