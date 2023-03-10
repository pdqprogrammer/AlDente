using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    private PlatformState platformState = PlatformState.FIRM;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

