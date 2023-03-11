using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Mover mover;
    public Jumper jumper;
    public SpriteRenderer spriteRenderer;
    public AudioSource audioSource;
    public Animator animator;

    private Vector2 initialPos = Vector2.zero;

    private void Awake()
    {
        initialPos = transform.position;
    }

    // Update is called once per frame, around 60 times a second
    void Update()
    {
        //for use with animator
        //jumper.Velocity
        //mover.Velocity
        //jumper.IsOnGround

        animator.SetFloat("YVelocity", jumper.Velocity.y);

        //Listen for key presses and move left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            mover.AccelerateInDirection(new Vector2(-1, 0));
            spriteRenderer.flipX = true;
            //animator.SetBool("Walking", true);
            
        }

        //Listen for key presses and move right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            mover.AccelerateInDirection(new Vector2(1, 0));
            spriteRenderer.flipX = false;
            //animator.SetBool("Walking", true);
        }

        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            //animator.SetBool("Walking", false);
        }

        //Listen for key presses and jump
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            jumper.Jump();
            animator.SetBool("IsOnGround", !jumper.IsOnGround());

            //Play a Jump Sound
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            jumper.SetGravityReduced(true);
        }
        else
        {
            jumper.SetGravityReduced(false);
        }
    }

    public void ResetPos()
    {
        transform.position = initialPos;
    }
}
