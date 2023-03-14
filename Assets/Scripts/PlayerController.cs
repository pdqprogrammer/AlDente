using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Mover mover;
    public Jumper jumper;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public PlayerAudioController playerAudioController;
    public Rigidbody2D playerRigidBody;

    private bool setPaused = false;

    private void Start()
    {
        if (GameManager.CurrentGameState == GameStates.MENU)
        {
            animator.SetBool("IsOnGround", false);
            animator.SetFloat("YVelocity", -0.21f);
        }
    }

    // Update is called once per frame, around 60 times a second
    private void Update()
    {
        if (GameManager.CurrentGameState != GameStates.INGAME)
        {
            if (!setPaused)
            {
                if (GameManager.CurrentGameState == GameStates.MENU)
                {
                    animator.updateMode = AnimatorUpdateMode.UnscaledTime;
                }
                else
                {
                    animator.updateMode = AnimatorUpdateMode.Normal;
                }

                setPaused = true;

                if(GameManager.CurrentGameState == GameStates.PAUSE)
                {
                    Time.timeScale = 0;
                }
            }

            return;
        }

        if (setPaused)
        {
            setPaused = false;
            Time.timeScale = 1;
        }

        animator.SetFloat("YVelocity", jumper.Velocity.y);
        animator.SetBool("IsOnGround", jumper.IsOnGround());

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Landing"))
        {
            playerAudioController.PlayAudio(SoundStates.LAND);
        }

        //Listen for key presses and move left
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            mover.AccelerateInDirection(new Vector2(-1, 0));
            spriteRenderer.flipX = true;
            animator.SetBool("Walking", true);
            if (jumper.IsOnGround())
            {
                playerAudioController.PlayAudio(SoundStates.WALK);
            }
        }

        //Listen for key presses and move right
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            mover.AccelerateInDirection(new Vector2(1, 0));
            spriteRenderer.flipX = false;
            animator.SetBool("Walking", true);
            if (jumper.IsOnGround())
            {
                playerAudioController.PlayAudio(SoundStates.WALK);
            }
        }

        if(Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A) ||
            Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            mover.StopAcceleration();
            playerAudioController.StopWalkAudio();
            animator.SetBool("Walking", false);
        }

        //Listen for key presses and jump
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space))
        {
            jumper.Jump();

            //Play a Jump Sound
            playerAudioController.PlayAudio(SoundStates.JUMP);
            playerAudioController.StopWalkAudio();
        }

        //TODO see what this does
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space))
        {
            jumper.SetGravityReduced(true);
        }
        else
        {
            jumper.SetGravityReduced(false);
        }
    }

    public void Died()
    {
        playerRigidBody.bodyType = RigidbodyType2D.Static;
        animator.SetBool("IsOnGround", false);
        animator.SetFloat("YVelocity", -0.21f);
        spriteRenderer.flipY = true;
        playerAudioController.PlayDeathAudio();
    }

    public void Win()
    {
        Time.timeScale = 0;
        playerAudioController.PlayWinAudio();
    }
}
