using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMan : MonoBehaviour
{

    public MoveAndCollide2D movement;
    public float xInput;
    public float yInput;

    public float wallSlideSpeed;
    public float wallRecoverySpeed;
    public float moveSpeed;
    public float jumpForce;

    public int wallJumpDirection = 0;
    public int faceDirection = 0;
    public int jumpsRemaining = 0;
    public int maxJumpCount = 2;

    public bool wallSliding;
    public bool wallSlideLeft;
    public bool wallSlideRight;

    public bool wallJumping;
    public float wallJumpDuration = 0.5f;
    private float wallJumpT;

    public bool jumpDashing;
    public float jumpDashDuration = 0.8f;
    private float jumpDashT;



    public void Awake()
    {
        movement.SetYOverride(false);
    }

    // Start is called before the first frame update
    void Start()
    {

    }
    // Test
    // Update is called once per frame
    void Update()
    {
        wallSliding = wallSlideLeft = wallSlideRight = false;

        Vector2 playerInput = new Vector2(Input.GetAxis("Horizontal"), 0);
        Vector2 moveDirection = Vector2.zero;

        if (playerInput.x < 0)
        {
            //Player was last facing left
            faceDirection = -1;
        }
        else if (playerInput.x > 0)
        {
            //Player was last facing right
            faceDirection = 1;
        }

        if (movement.grounded || movement.wallCollision)
        {
            // If we are grounded or next to wall, reset jumpCount
            jumpsRemaining = maxJumpCount;
        }

        if (movement.onWall && playerInput.x > 0)
        {
            //Wall is to the right of the player
            wallSlideRight = true;
        }
        else if (movement.onWall && playerInput.x < 0)
        {
            //Wall is to the left of the player
            wallSlideLeft = true;
        }

        if (!wallJumping && !jumpDashing)
        {
            //Basic move speed with no actions
            moveDirection = playerInput * moveSpeed;

        }
        else if (wallJumping)
        {
            //wallJump away from wall
            moveDirection = Vector2.right * moveSpeed * wallJumpDirection;
            wallJumpT += Time.deltaTime;

            if (wallJumpT > wallJumpDuration)
            {
                //WallJump time has elapsed, stop wallJumping
                wallJumpT = 0;
                wallJumping = false;
            }
        }
        else if (jumpDashing)
        {
            // jumpDash in direction player is facing
            moveDirection = Vector2.right * moveSpeed * 3 * faceDirection;
            jumpDashT += Time.deltaTime;

            if (jumpDashT > jumpDashDuration)
            {
                //jumpDash time has elapsed, stop jumpDashing
                jumpDashT = 0;
                jumpDashing = false;
            }
        }

        if (Input.GetButtonDown("Jump") && ((movement.grounded || movement.onWall)
            || jumpsRemaining > 0))
        {
            // Space was pressed and player can still jump
            if (!wallJumping)
            {
                Jump();
            }

            if (movement.wallCollision)
            {
                // Player is wallJumping, get direction to push off wall
                wallJumping = true;
                wallJumpDirection = GetWallJumpDirection();
            }

        }

        if ((movement.grounded || wallJumping) && Input.GetButtonDown("Fire3"))
        {
            // left shift is being pressed, start sprinting
            moveSpeed *= 2;
        }
        else if (!movement.grounded && !wallJumping && Input.GetButtonDown("Fire3"))
        {
            // player is jumping and has pressed left shift
            jumpDashing = true;
        }

        if (Input.GetButtonUp("Fire3"))
        {
            // Player is not sprinting
            moveSpeed = 7;
        }

        movement.SetVelocity(moveDirection);  //Velocity equals moveDirection

    }

    /**
    GetWallJumpDirection determines what direction to push off wall
    @returns -1 to push towards right and 1 to push towards left
     */
    public int GetWallJumpDirection()
    {
        int direction = 1;

        if (wallSlideLeft)
        {
            direction = 1;
        }
        else if (wallSlideRight)
        {
            direction = -1;
        }

        //Debug.Log(direction);
        return direction;
    }

    /**
    Jump method handles calculating the jumping velocity and decreasing 
    the jumpsRemaining.
    */
    private void Jump()
    {
        movement.velocity = new Vector2(movement.velocity.x, jumpForce);
        jumpsRemaining--;
    }
}
