using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    //Set some public variables so we can control our speed
    public float acceleration = 10f;
    public float maximumSpeed = 15f;

    private Rigidbody2D myRigidbody2D;
    private Vector2 velocity = Vector2.zero;

    public Vector2 Velocity => velocity;

    // Start is called before the first frame update
    private void Awake()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
    }

    public void AccelerateInDirection( Vector2 direction )
    {
        //Normalize just means... set between 0 and 1 so that it only represents direction
        direction = Vector3.Normalize(direction);

        //Make our velocity faster depending on acceleration and frame rate
        velocity = myRigidbody2D.velocity +
            (direction * acceleration * Time.deltaTime);

        //Set maximum speeds in both directions
        velocity.x = Mathf.Clamp(velocity.x, -maximumSpeed, maximumSpeed);
        velocity.y = Mathf.Clamp(velocity.y, -maximumSpeed, maximumSpeed);

        myRigidbody2D.velocity = velocity;
    }

    public void StopAcceleration()
    {
        //Stop moving right and left
        velocity.x = 0;

        myRigidbody2D.velocity = velocity;
    }
}
