using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float gravityModifier; // allows us to scale the gravity using a float, 0.5f = half gravity
    public Vector2 velocity; // should be protected instead of public, due to inheritance
    protected Rigidbody2D rb2d;

    [SerializeField]
    public Vector2 targetVelocity; // store incoming values

    protected const float minMoveDistance = 0.001f;

    public bool grounded;
    protected Vector2 groundNormal;

    public bool onWall;
    public bool wallCollision;

    public ContactFilter2D contactFilter;

    protected RaycastHit2D[] hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> hitBufferList = new List<RaycastHit2D>(16);

    protected const float shellRadius = 0.01f;

    public float minGroundNormalY = 0.6f;

    public Collider2D col;

    private bool groundedLastFrame;

    public bool justLanded;

    public virtual void OnLand(){
        Debug.Log("Landed");
    }

    // Start is called before the first frame update

    // ON Enable could be useful for hitboxes!
    void OnEnable(){
        rb2d = GetComponent<Rigidbody2D>();
    }
    public virtual void Start()
    {
        col = GetComponent<Collider2D>();

        contactFilter.useTriggers = false;
        contactFilter.useLayerMask = true;
        contactFilter.SetLayerMask(Physics2D.GetLayerCollisionMask(gameObject.layer)); // WHat are we doing here? We're getting a layermask from the project settings for physics2D.
        

        onWall = false;
        wallCollision = false;

        targetVelocity = Vector2.zero;
    }

    // Update is called once per frame
    public virtual void Update()
    {
        //targetVelocity = Vector2.zero; // zero out our targetvelocity every frame, so as not to use data from the previous frame.
        ComputeVelocity();
    }

    public virtual void ComputeVelocity(){

    }

    public virtual void CalculateYVelocity(){
        velocity += gravityModifier * Physics2D.gravity * Time.deltaTime;
    }

    void FixedUpdate(){

        // Update Y Velocity, if you want to have the children of the class take care of this, give them the means to set yVelocity directly, perhaps child classes Update the X and the Y in each frame
        // Another way to say it, this CalculateYVelocity() function could be replaced with something like..
        // velocity.y = targetVelocity.y;
        CalculateYVelocity();

        // Assign the x value of our velocity vector equal to the value from our player input vector
        velocity.x = targetVelocity.x;

        grounded = false; // until a collision is registered that frame, grounded will be considered false.

        onWall = false;
        wallCollision = false;
        
        

        Vector2 deltaPosition = velocity * Time.deltaTime; // The Change in Position
        // now that we have a new position, we want to use that for movement

        Vector2 moveAlongGround = new Vector2(groundNormal.y, -groundNormal.x); // a perpendicular vector to our ground.

        Vector2 move = moveAlongGround * deltaPosition.x;

        Movement(move, false);// this is what we'll pass to our movement function; We run this function twice in the frame, first on the X axis, then on the y.

        move = Vector2.up * deltaPosition.y;// This level of the class hierarchy only deals with vertical movement.

        Movement(move, true); // this implementation makes 2 distinct calls to the movementfunction,
        // 1 for vertical movement, and 1 for horizontal movement, this makes it easier for handling slopes, as well as seperating horizontal and vertical collision checks
    }

    void Movement(Vector2 move, bool yMovement){

        justLanded = false;

        // Moves the object based on the values calculated, do this by setting the position of our objects rigidbody2d.

        float distance = move.magnitude; // the distance we are attempting to move, important to save performance when only trying to move tiny bits when standing still

        if (distance > minMoveDistance){ // otherwise we won't check for collisions.
            // Use RigidBody2D.Cast to check if any of the colliders attached to our rigidbody are going to overlap where we want to go next frame.
            // Important to reference Rb2d.Cast API in order to deepen understanding of how it works.
            int count = col.Cast(move, contactFilter, hitBuffer, distance + shellRadius);
            hitBufferList.Clear();
            // we're only gonna copy the elements of the hitbuffer array that actually hit something, so we'll have a list just of our current active contacts.
            for (int i = 0; i < count; i++){
                // each entry from the array gets added to the hitbuffer list
                hitBufferList.Add(hitBuffer[i]);
            }

            

            for (int i = 0; i < hitBufferList.Count; i++){
                Vector2 currentNormal = hitBufferList[i].normal;
                // check the normal of each of the raycasthit2d's in our list, and compare it to a minimum value.
                // We're trying to determin if the player is grounded or not, the main manifestation of that is either they'll play falling or idling animation.

                if (Mathf.Abs(currentNormal.x) > 0){
                    onWall = true;
                    wallCollision = true;
                }

                if (currentNormal.y > minGroundNormalY){
                    //we'll use this to set the player's grounded state.
                    // Does the angle of this collision mean it's a sensible ground for the player?

                    grounded = true;
                    onWall = false;

                    

                    if (yMovement){ // if we are moving along the y
                        groundNormal = currentNormal;
                        currentNormal.x = 0;

                        if (!groundedLastFrame){
                        justLanded = true;
                    }

                    }
                }

                float projection = Vector2.Dot(velocity, currentNormal);
                if (projection < 0) // returning negative
                {
                    // then 
                    velocity = velocity - projection * currentNormal; // cancel out the part of our velocity that would be stopped by the collision.
                }
                

                float modifiedDistance = hitBufferList[i].distance - shellRadius;

                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }

        }

        if (justLanded){
            OnLand();
            Debug.Log("Just Landed");
        }

        //Debug.Log(grounded);

        if (yMovement){
            groundedLastFrame = grounded;
        }
        

        rb2d.position = rb2d.position + move.normalized * distance;// We are adding the position of our movement vector to the positon of our rigidbody2d, every frame;
        
    }
}
