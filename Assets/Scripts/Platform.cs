using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private PlatformState platformState = PlatformState.FIRM;

    [SerializeField]
    private float maxBreakTimer = 1.0f;

    [SerializeField]
    private float maxDropTimer = 1.0f;

    [SerializeField]
    private float rotateSpeed = 45.0f;

    [SerializeField]
    private float dropSpeed = 1.0f;

    [SerializeField]
    private bool canHeat = false;

    [SerializeField]
    private bool destroyOnJump = false;

    [SerializeField]
    private bool breakOnStand = false;

    [SerializeField]
    private bool canDrop = false;

    [SerializeField]
    private bool isRotating = false;

    [SerializeField]
    private Animator animator = null;

    [Header("Platform Colors")]
    [SerializeField]
    private Color firmColor = Color.white;

    [SerializeField]
    private Color softColor = Color.white;

    [SerializeField]
    private Color fragileColor = Color.white;

    [SerializeField]
    private Color wallColor = Color.white;

    [SerializeField]
    private SpriteRenderer platformSpriteRenderer = null;

    [Header("TEMP Debug for Values")]
    public float breakTime = 0.0f;//TEMP Public for viewing
    public float dropTime = 0.0f;//TEMP Public for viewing
    public float destroyTime = 0.0f;//TEMP Public for viewing
    public bool isDropping = false;//TEMP Public for viewing
    public bool canDestroy = false;//TEMP Public for viewing

    // Start is called before the first frame update
    private void Start()
    {
        SetSpriteColor();
    }

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.CurrentGameState == GameStates.INGAME)
        {
            if (isDropping && dropTime <= maxDropTimer)
            {
                dropTime += Time.deltaTime;

                Vector3 direction = new Vector3(0, Time.deltaTime * -dropSpeed, 0);
                Vector3 updatedPosition = transform.position + direction;
                transform.position = updatedPosition;

                if (dropTime >= maxDropTimer)
                {
                    Destroy(gameObject);
                }
            }

            if (canDestroy && destroyTime < maxBreakTimer)
            {
                destroyTime += Time.deltaTime;
            }

            if (isRotating)
            {
                Vector3 rotationDirection = Vector3.forward * rotateSpeed * Time.deltaTime;
                transform.Rotate(rotationDirection);
            }

            //check if animator is connected otherwise exit
            if(animator == null)
            {
                return;
            }

            //check animation is playing and if is broken
            AnimatorStateInfo animatorStateInfo = animator.GetCurrentAnimatorStateInfo(0);

            if (animatorStateInfo.IsName("Broken"))
            {
                if (animatorStateInfo.normalizedTime > 1.0f)
                {
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (platformState == PlatformState.FRAGILE)
            {
                //ROLL the dice and decide if it breaks
                int randomValue = Random.Range(0, 20);
                if(randomValue <= 5 && animator != null)
                {
                    animator.SetBool("Breaking", true);
                    animator.SetBool("IsBroken", true);
                }
            }

            if (canDrop)
            {
                isDropping = true;
            }

            if (destroyOnJump)
            {
                canDestroy = true;
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (GameManager.CurrentGameState == GameStates.INGAME)
        {
            //check if player on for certain time then switch to broken animation
            if (collision.gameObject.tag.Equals("Player"))
            {
                if (breakOnStand && breakTime <= maxBreakTimer && animator != null)
                {
                    animator.SetBool("Breaking", true);

                    breakTime += Time.deltaTime;
                    if (breakTime >= maxBreakTimer)
                    {
                        animator.SetBool("IsBroken", true);
                    }
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        //check if player is jumping
        if (collision.gameObject.tag.Equals("Player") && destroyTime >= maxBreakTimer)
        {
            animator.SetBool("Breaking", true);
            animator.SetBool("IsBroken", true);
        }
    }

    private void SetSpriteColor()
    {
        if(platformSpriteRenderer == null)
        {
            Debug.Log("Please set sprite renderer.");
            return;
        }

        switch (platformState)
        {
            case PlatformState.FIRM:
                platformSpriteRenderer.color = firmColor;
                break;
            case PlatformState.SOFT:
                platformSpriteRenderer.color = softColor;
                break;
            case PlatformState.FRAGILE:
                platformSpriteRenderer.color = fragileColor;
                break;
            default:
                platformSpriteRenderer.color = wallColor;
                break;
        }
    }

    /**PUBLIC FUNCTIONS**/
    public bool IsHighJump()
    {
        if(platformState == PlatformState.SOFT)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void HeatPlatform()
    {
        if (canHeat)
        {
            //if can heat then change state of the pasta
            if (platformState == PlatformState.FIRM)
            {
                platformState = PlatformState.SOFT;
            }
            else if (platformState == PlatformState.SOFT)
            {
                platformState = PlatformState.FRAGILE;
            }

            SetSpriteColor();
        }
    }

    private enum PlatformState
    {
        FIRM,
        SOFT,
        FRAGILE,
        WALL
    }
}

