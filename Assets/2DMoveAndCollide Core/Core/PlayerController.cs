using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    //Beginning MovementController
    
    public PlayerControls controls;
    [Header("Player MoveAndCollide2D")]
    public MoveAndCollide2D movement;
    [Space(10)]

    [Header("Attributes")]
    public float maxSpeed = 7f;
    public float jumpForce = 7f;
    public float wallJumpRecoverySpeed = 3f;
    public float wallSlideSpeed = 2;
    public float maxFallSpeed = 20f;
    [Space(10)]

    [Header("Input")]
    public float xInput;
    public float yInput;
    [Space(10)]

    [Header("Player State")]
    public PlayerMovementState currentMovementState;
    public bool stunned = false;
    public bool flipped;
    [Space(10)]

    [Header ("Player State Management")]
    public FreeMoveState freeMoveState;
    public AttackState attackState;

    [Header("Collision State")]
    public bool onWall;
    public bool wallCollision;
    public bool wallSliding;
    public bool wallSlideLeft;
    public bool wallSlideRight;

    [Space(10)]

    [Header("Component References")]
    public SpriteRenderer spriteRendy;
    public Animator anim;
    public GameObject visuals;
    public Vector2 move;

    public UnityEvent OnJumpEvent;

    private void HandleAttack(InputAction.CallbackContext context){
        SetMoveState(attackState);
        anim.SetTrigger("Attack");
    }

    public void AttackFinish(){
        SetMoveState(freeMoveState); // needs to be cached
    }

    public virtual void HandleJump(InputAction.CallbackContext context){
        if (movement.grounded)
            GroundedJump();
        else if (wallSliding)
        {
            SetMoveState(new WallJumpState(this, GetWallSlideDirection(), wallJumpRecoverySpeed));
        }
    }

    public virtual void Initialize() { }

    public void Awake(){

        controls = new PlayerControls();
        
         var attack = controls.Player.Attack;
            attack.performed += HandleAttack;
            attack.Enable();

        var jump = controls.Player.Jump;
            jump.performed += HandleJump;
            jump.Enable();

        var horizontal = controls.Player.Horizontal;
        
            horizontal.Enable();

        var accept = controls.Player.Accept;
            accept.Enable();

        Initialize();
    }

    // Start is called before the first frame update
    public void Start()
    {
        spriteRendy = visuals.GetComponent<SpriteRenderer>();
        anim = visuals.GetComponent<Animator>();

        InitializeMoveStates();
    }

    public virtual void InitializeMoveStates(){
        freeMoveState = new FreeMoveState(this);
        currentMovementState = freeMoveState;
        attackState = new AttackState(this);
    }

    public void SetMoveState(PlayerMovementState state){
        if (currentMovementState != null){
            currentMovementState.OnStateExit();
        }

        currentMovementState = state;

        if (currentMovementState != null){
            currentMovementState.OnStateEnter();
        }
    }

    public void IAttacked(Vector2 kb, int damage, float power){
        //SetMoveState(new KnockbackState(this, kb, power));
    }

    public virtual void OnLand(){ // invoke this through MoveAndCollideEvents
        anim.SetTrigger("Land");
        AttackFinish();
        Debug.Log("Landed");
    }
    
    public virtual void AbilityInputCheck(){

    }

    public void GroundedJump(){
        movement.velocity.y = jumpForce;
        OnJump();
        anim.SetTrigger("Jump");
        OnJumpEvent.Invoke();
    }

    public virtual void AirJump()
    {
        movement.velocity.y = jumpForce;
        OnJump();
        Debug.Log("AirJump");
        anim.SetTrigger("AirJump");
        OnJumpEvent.Invoke();
    }

    // Internal Update
    public void Update(){

        // Set Input
        Vector2 pInput = controls.Player.Horizontal.ReadValue<Vector2>();
        xInput = pInput.x;
        yInput = pInput.y;

        //Debug.Log(xInput);

        if (currentMovementState != null){
            currentMovementState.Tick();
        }

        AbilityInputCheck();

        ComputeVelocity();

        if (!stunned){

            if (Input.GetKeyDown(KeyCode.JoystickButton3) && movement.grounded){
                GroundedJump();
            } /* else if (controls.Gameplay.Jump.triggered && wallSliding){
                SetMoveState(new WallJumpState(this, GetWallSlideDirection(), wallJumpRecoverySpeed));
            } */
            
            if (Input.GetKeyUp(KeyCode.JoystickButton0)  && !movement.grounded){
                if (movement.velocity.y > 0){
                    movement.velocity.y = movement.velocity.y * 0.5f;
                }
            }
        }

        if (movement.velocity.y < -this.maxFallSpeed)
        {
            movement.velocity.y = -this.maxFallSpeed;
        }

        anim.SetFloat("VelocityX", Mathf.Abs(movement.velocity.x));
        anim.SetFloat("VelocityY", movement.velocity.y);

        anim.SetBool("Grounded", movement.grounded);
        anim.SetBool("OnWall", wallSliding);
        anim.SetBool("Stunned", stunned); 
    }

    public void FixedUpdate()
    {
        if (movement.justLanded)
        {
            this.OnLand();
        }
    }

    // Called in Update
    public void ComputeVelocity(){ // called every UPDATE frame by the parent class. Sets PhysicsObject.targetvelocity = move;

        wallSliding = false;
        wallSlideLeft = false;
        wallSlideRight = false;

        move = Vector2.zero;

        // Move.x = currentMovementState.GetXVelocity();
        move.x = currentMovementState.GetXVelocity();
        // Move.y = currentMovementState.GetYVelocity();
        move.y = currentMovementState.GetYVelocity();

        if (move.x > 0){
            SetFlipped(false);
            /* spriteRendy.flipX = false; */
        } else if (move.x < 0){
            SetFlipped(true);
            /* spriteRendy.flipX = true; */
        }

        if (movement.onWall && move.x > 0){
            SetFlipped(true);
            wallSlideLeft = true;
            /* spriteRendy.flipX = true; */
        } else if (movement.onWall && move.x < 0){
            SetFlipped(false);
            wallSlideRight = true;
            /* spriteRendy.flipX = false; */
        }

        this.wallCollision = movement.wallCollision;
        
        wallSliding = (wallSlideLeft || wallSlideRight);

        if (wallSliding && movement.velocity.y <= -wallSlideSpeed){
            movement.velocity.y = -wallSlideSpeed;
        }

        // In this implementation, target velocity is specifically for the X;
        movement.SetYOverride(currentMovementState.overridesYVelocity);
        movement.SetVelocity(move);

        // we can find a way to make simple for functions indicating, IsOnWall() or IsOnWallToRight();
    }

    public float GetWallSlideDirection(){

        float direction = 0f;

        if (!wallSliding){
            direction = 0f;
        } else if (wallSlideLeft){
            direction = -1f;
        } else if (wallSlideRight) direction = 1f;

        Debug.Log("Direction");
        return direction;
    }

    public int GetDirection()
    {
        if (this.flipped)
        {
            return -1;
        }
        else return 1;
    }

    public void SetFlipped(bool flip){

        if (currentMovementState.overridesFlipDirection)
        {
            return;
        }

        if (flip != this.flipped){
            OnFlipped();
        }

        flipped = flip;

        if (flip){
            visuals.transform.localScale = (new Vector2 (-1, 1));
        } else visuals.transform.localScale = new Vector2(1, 1);
    }

    public void OnFlipped(){
        if (movement.grounded){
            
        }
    }

    public void OnJump(){
        
    }
}

public class WallJumpState : PlayerMovementState
{


    float wallJumpRecoverySpeed = 1f;
    float lerpPercentage;
    float initialJumpVelocity;
    float stateVelocity = 0f;

    public WallJumpState(PlayerController player, float wallJumpDireciton) : base(player)
    {

        lerpPercentage = 0;

        initialJumpVelocity = player.maxSpeed * wallJumpDireciton;

        // Jump the player
        player.movement.velocity.y += player.jumpForce;
    }

    public override void OnStateEnter()
    {
        //SFX_II.instance.Play("Jump");

        overridesFlipDirection = true;
    }

    public WallJumpState(PlayerController player, float wallJumpDireciton, float recoverySpeed) : base(player)
    {
        lerpPercentage = 0;

        initialJumpVelocity = player.maxSpeed * wallJumpDireciton;

        this.wallJumpRecoverySpeed = recoverySpeed;

        player.movement.velocity.y = player.jumpForce;
    }


    public override void Tick()
    {

        stateVelocity = Mathf.Lerp(initialJumpVelocity, (player.xInput * player.maxSpeed), lerpPercentage);

        lerpPercentage += Time.deltaTime * wallJumpRecoverySpeed;

        if (lerpPercentage >= 0.5f)
        {
            overridesFlipDirection = false; // return flip control back to momentum
        }

        if (lerpPercentage >= 1.0f || player.movement.grounded)
        {
            player.SetMoveState(player.freeMoveState);
        }
    }

    public override float GetXVelocity()
    {
        return stateVelocity;
    }
}

[System.Serializable]
public class PlayerMovementState : State {

    public bool overridesFlipDirection;

    public string stateName;

    public bool overridesYVelocity = false;
    public PlayerController player;

    public PlayerMovementState(PlayerController player){
        this.player = player;
        this.stateName = this.GetType().ToString();
    }

    public virtual float GetXVelocity() {return player.move.x;}
    public virtual float GetYVelocity() {return player.move.y;}
}

public class FreeMoveState : PlayerMovementState{

    public FreeMoveState (PlayerController player) : base (player){}

    public override float GetXVelocity(){
        return player.xInput * player.maxSpeed;
    }

}

public class AttackState : PlayerMovementState {
     public AttackState(PlayerController player) : base (player){

     }

     public override float GetXVelocity(){

         if (player.movement.grounded){
             return 0f;
         } else return player.xInput * player.maxSpeed;
         
     }
}
