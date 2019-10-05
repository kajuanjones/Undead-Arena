using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMan : MonoBehaviour
{

    public MoveAndCollide2D movement;
    public float xInput;
    public float yInput;

    [Header("Set in Inspector")]
    public float baseMoveSpeed;             // Initial player speed
    public float jumpForce;                 // Determines how high player can jump
    public int maxJumpCount = 2;            // Number of continuous jumps allowed
    public int maxDashCount = 1;            // Number of continuous dashes allowed
    public float wallJumpDuration = 0.5f;   
    public float jumpDashDuration = 0.3f;   

    [Header("Set Dynamically")]
    public float moveSpeed;
    public int wallJumpDirection = 0;       // Determines direction to push off wall
    public int faceDirection = 0;           // Direction player character is facing
    public int jumpsRemaining = 0;
    public int dashesRemaining = 0;

    public float wallSlideSpeed;
    public float wallRecoverySpeed;
    private float wallJumpT;
    private float jumpDashT;

    public bool wallSliding;
    public bool wallSlideLeft;
    public bool wallSlideRight;
    private bool groundPounding;
    public bool wallJumping;
    public bool jumpDashing;

    public void Awake()
    {
        movement.SetYOverride(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        faceDirection = 1;
        moveSpeed = baseMoveSpeed;
    }
    // Test
    // Update is called once per frame
    void Update()
    {
        // While jump dashing, negate gravity
        movement.SetYOverride(jumpDashing);

        wallSliding = wallSlideLeft = wallSlideRight = false;

        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), 0);
        Vector2 moveDirection = Vector2.zero;

        FacingDirection(playerInput.x);

        if (movement.grounded || movement.wallCollision)
        {
            // If we are grounded or on wall, reset jumpCount and dashCount
            jumpsRemaining = maxJumpCount;
            dashesRemaining = maxDashCount;
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

        //------------Determine movement-------------\\
        if (!wallJumping && !jumpDashing)
        {
            //Basic move speed with no actions
            groundPounding = false;
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
            // Set y velocity to zero
            movement.velocity.y = 0;
            // jumpDash in direction player is facing
            moveDirection = Vector2.right * moveSpeed * 3 * faceDirection;
            jumpDashT += Time.deltaTime;

            if (jumpDashT > jumpDashDuration || movement.grounded)
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

        if(Input.GetAxis("Vertical") < 0 && !movement.grounded && !wallJumping && !jumpDashing)
        {
            // Down  or S key was pressed while jumping, ground pound.
            groundPounding = true;
            movement.velocity = new Vector2(0, jumpForce * -3.5f);

            //TODO more interesting things with ground pound.
        }

        if ((movement.grounded || wallJumping) && Input.GetButtonDown("Fire3"))
        {
            // left shift is being pressed, start sprinting
            moveSpeed = baseMoveSpeed * 2;
        }
        else if (!movement.grounded && !wallJumping && Input.GetButtonDown("Fire3") 
            && dashesRemaining > 0)
        {
            // player is jumping and has pressed left shift, jumpDash
            jumpDashing = true;
            dashesRemaining--;
        }

        if (Input.GetButtonUp("Fire3"))
        {
            // Player is not sprinting
            moveSpeed = baseMoveSpeed;
        }

        // Set player velocity
        movement.SetVelocity(moveDirection);  

    }

    /**
     * FacingDirection method determines what direction the player character is
     * facing.
     * @param xInput The x axis input value
     */
    public void FacingDirection (float xInput)
    {
        if (xInput < 0)
        {
            //Player is facing left
            faceDirection = -1;
        }
        else if (xInput > 0)
        {
            //Player is facing right
            faceDirection = 1;
        }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
        }
    }

    /* private void OnCollision2D(Collider2D coll) {
     * 
     * if(!coll.gameObject.CompareTag("Breakable floor") || !groundPounding) {
     *      return;
     *   } else {
     *      // Active particle system to show breaking?
     *      // Destroy floor
     *   }
     * }
     */
}
