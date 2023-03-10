using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private PlatformState platformState = PlatformState.FIRM;

    [SerializeField]
    private float breakTimer = 1.0f;

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

    private float currTime = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //TODO add rotation function
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //TODO break platform if Fragile
        if (platformState == PlatformState.FRAGILE)
        {
            //ROLL the dice and decide if it breaks
        }
    }

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

    private enum PlatformState
    {
        FIRM,
        SOFT,
        FRAGILE,
        WALL
    }
}

