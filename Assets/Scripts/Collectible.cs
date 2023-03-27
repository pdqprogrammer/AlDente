using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectible : MonoBehaviour
{
    //score that will be added when the item is collected
    [SerializeField]
    private int score = 10;

    [SerializeField]
    private FMODUnity.EventReference collectSound;

    [SerializeField]
    private float floatingSpeed = 5.0f;

    private bool isCollected = false;
    private bool isBoiled = false;
    private bool isMoving = false;

    public void Update()
    {
        if (isMoving)
        {
            //TODO move position upward
            Vector3 position = transform.position;
            position.y += Time.deltaTime * floatingSpeed;
            transform.position = position;
        }
    }

    /// <summary>
    /// function to set boiled and moving
    /// </summary>
    public void SetBoiled()
    {
        isBoiled = true;
        isMoving = true;

        //TODO set to flipped 
        Quaternion rotation = transform.rotation;
        rotation.z = 180f;
        transform.rotation = rotation;
    }

    /// <summary>
    /// function to stop moving
    /// </summary>
    public void StopMoving()
    {
        isMoving = false;
    }

    //Very simple script just listens to see if something touches it
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //If it does touch something, print a message, update score, and destroy object
        if (collision.gameObject.tag.Equals("Player"))
        {
            if (isBoiled)
            {
                return;
            }

            if (!isCollected)
            {
                GameManager.IncreaseScore(score);
            }
            
            isCollected = true;
            FMODUnity.RuntimeManager.PlayOneShot(collectSound);
            Debug.Log("Score Increased! " + score);
            Destroy(gameObject);
        }
    }
}
