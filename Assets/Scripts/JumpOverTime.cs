using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpOverTime : MonoBehaviour
{
    public float timeUntilJump = 3f;
    public Jumper jumper;

    private float timeSinceJump = 0f;

    // Update is called once per frame
    void Update()
    {
        timeSinceJump += Time.deltaTime;
        print(timeSinceJump);

        if( timeSinceJump >= timeUntilJump )
        {
            jumper.Jump();
            timeSinceJump = 0f;
        }
    }
}
