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
    public int jumpCount = 0;
    public int maxJumpCount = 2;

    public bool wallSliding;
    public bool wallSlideLeft;
    public bool wallSlideRight;

    public bool wallJumping;
    public float wallJumpDuration = 0.5f;
    private float wallJumpT;


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

        if(movement.grounded || movement.wallCollision)
        {
            jumpCount = maxJumpCount;
        }

        if (movement.onWall && playerInput.x > 0)
        {
            wallSlideRight = true;
        }
        else if (movement.onWall && playerInput.x < 0)
        {
            wallSlideLeft = true;
        }

        if(!wallJumping)
        {
            moveDirection = playerInput * moveSpeed;

        } else if(wallJumping)
        {
            moveDirection = Vector2.right * moveSpeed * wallJumpDirection;
            wallJumpT += Time.deltaTime;

            if(wallJumpT > wallJumpDuration)
            {
                wallJumpT = 0;
                wallJumping = false;
            }
        }

        if(Input.GetButtonDown("Jump") && ((movement.grounded || movement.onWall) || jumpCount > 0))
        {
            if(!wallJumping)
            {
                Jump();
            }

            if (movement.wallCollision)
            {
                wallJumping = true;
                wallJumpDirection = GetWallJumpDirection();
            }

        }
       
        movement.SetVelocity(moveDirection);  //Velocity equals moveDirection

    }

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

        Debug.Log(direction);
        return direction;
    }

    private void Jump()
    {
        movement.velocity = new Vector2(movement.velocity.x, jumpForce);
        jumpCount--;
    }
}
