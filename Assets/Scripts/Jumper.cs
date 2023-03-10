using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jumper : MonoBehaviour
{
    //Our jump force is how high we can actually jump
    public float jumpForce = 6f;

    // Distance from the ground that the raycast to check for ground collision will be
    public float groundCheckOffset = 0f;

    // Length of the raycast to check for ground collision
    public float groundCheckDistance = 0.2f;

    // While the spacebar is held, gravity is reduced. Set this to 1 to disable
    public float gravityScaleWhileJumpHeld = 0.5f;

    //In this script, the other scripts it needs are grabbed automatically. So we can make them private
    //Private variables are not visible in the inspector, but they still exist
    private Rigidbody2D myRigidBody2D;

    void Start()
    {
        //This script needs a rigidbody2d and a ground detector attached to the same object to work
        myRigidBody2D = GetComponent<Rigidbody2D>();
    }

    public void SetGravityReduced(bool isGravityReduced)
    {
        if (isGravityReduced)
        {
            myRigidBody2D.gravityScale = gravityScaleWhileJumpHeld;
        }
        else
        {
            myRigidBody2D.gravityScale = 1;
        }
    }

    public void Jump()
    {
        //As long as we are on the ground
        if (IsOnGround())
        {
            //Jump!
            myRigidBody2D.velocity += new Vector2(0f, jumpForce);

            //If I'm jumping too fast, slow me down
            if (myRigidBody2D.velocity.y > jumpForce)
            {
                myRigidBody2D.velocity = new Vector2(myRigidBody2D.velocity.x, jumpForce);
            }
        }
    }

    // In this function, we raycast downwards to check if we are on the ground
    public bool IsOnGround()
    {
        // Where the raycast starts
        Vector2 startPosition = new Vector2(transform.position.x, transform.position.y + groundCheckOffset);
        
        // Collect a list of all of the things the raycast hits
        RaycastHit2D[] hits = Physics2D.RaycastAll(startPosition, Vector2.down, groundCheckDistance);
        
        // Loop through all of those hits
        foreach (RaycastHit2D hit in hits)
        {
            // If this hit ourself, skip this element in the loop
            if (hit.transform == transform) continue;
            
            // Otherwise, if this hit did not hit ourself, then there must be something else below us
            // This could either be the ground or an enemy, but we'll assume it's the ground for simplicity's sake
            // Programmers, you can improve this by having all of the ground tiles be on a single physics layer that this raycast checks for
            return true;
        }

        // We didn't return true yet, so we aren't on the ground
        return false;
    }

    // This is a tool for the game designer to see where the IsOnGround raycast is casting from
    private void OnDrawGizmos()
    {
        Vector2 startPosition = new Vector2(transform.position.x, transform.position.y + groundCheckOffset);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(startPosition, Vector3.down*groundCheckDistance);
    }
}
