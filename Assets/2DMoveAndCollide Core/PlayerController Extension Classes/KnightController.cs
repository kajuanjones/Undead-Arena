using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightController : PlayerController
{

    public ShieldState shieldState;
    public bool shielded = false;

    public override void InitializeMoveStates(){
        // Initializes Free Move State, sets starting move state to player.freeMoveState, initializes Attack State
        freeMoveState = new KnightFreeMoveState(this);
        currentMovementState = freeMoveState;
        attackState = new AttackState(this);
        shieldState = new ShieldState(this);
    }

    public void SetShielded(){
        shielded = true;
    }
}

public class KnightFreeMoveState : FreeMoveState{
    private KnightController knight;
    public KnightFreeMoveState(KnightController player) : base (player){
        knight = player;
    }

    public override void Tick(){
        if (player.yInput <= -0.7f){
            player.SetMoveState(knight.shieldState); // Cache needed
            player.anim.Play("StartShield");
        }
    }
}

// Knight's Shield State
public class ShieldState : PlayerMovementState {
    // Specific Class functionality
    private KnightController knight;
    public ShieldState (KnightController player) : base (player){
        knight = player;
    }

    public override void Tick(){
        if (player.yInput > -0.7f){
            player.SetMoveState(knight.freeMoveState); // cacheNeeded;
            player.anim.Play("Idle");
        } 
    }

    public override void OnStateExit(){
        knight.shielded = false;
    }

    public override float GetXVelocity(){
        return 0f;
    }
}
