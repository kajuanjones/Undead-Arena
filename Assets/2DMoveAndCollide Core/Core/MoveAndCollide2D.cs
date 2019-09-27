using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // to be not needed after refactoring
using UnityEngine.Events;

public class MoveAndCollide2D : PhysicsObject
{

    public UnityEvent onLand;

    public Vector2 local_TargetVelocity = new Vector2(0, 0);
    public bool _yOverride = true;

    public void SetYOverride(bool overrideYVelocity){
        _yOverride = overrideYVelocity;
    }

    public override void OnLand(){
        onLand.Invoke();
    }

    // Find a better solution for this manual velocity switching.

    public void SetVelocity(Vector2 velocity){
        this.local_TargetVelocity = velocity;
    }

    // Called in fixed update, handles Y Velocity
    public override void CalculateYVelocity(){

        if (_yOverride){
            // this is a manual setting, so we need a way for the player controller to frequently set the yOverride of the move and Collide 2D Object
            velocity.y = local_TargetVelocity.y;
            return;
        }

        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
    }

    // Internal Update
    public override void Update(){

        ComputeVelocity();
    }

    // Called in Update, sets Target Velocity for horizontal Movement
    public override void ComputeVelocity(){ // called every UPDATE frame by the parent class. Sets PhysicsObject.targetvelocity = move;

        Vector2 move = Vector2.zero;
        // In this implementation, target velocity is specifically for the X;

        move.x = local_TargetVelocity.x;

        targetVelocity.x = move.x;
        // we can find a way to make simple for functions indicating, IsOnWall() or IsOnWallToRight();
    }
}
