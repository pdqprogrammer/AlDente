using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follower : MonoBehaviour
{
    //The thing we want to follow
    public Transform followedTransform;
    public Vector3 offSet = Vector3.zero;

    // Update is called once per frame
    void Update()
    {
        if(GameManager.CurrentGameState == GameStates.GAMEOVER)
        {
            return;
        }
        //Set our position equal to the position of the player
        if (followedTransform != null)
        {
            Vector3 newPosition = followedTransform.position + offSet;
            newPosition.z = transform.position.z;
            newPosition.x = transform.position.x;

            //Set our position equal to the new position
            transform.position = newPosition;
        }
    }
}
