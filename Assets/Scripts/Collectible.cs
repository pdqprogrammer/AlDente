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

    private bool isCollected = false;

    //Very simple script just listens to see if something touches it
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //If it does touch something, print a message, update score, and destroy object
        if (collision.gameObject.tag.Equals("Player"))
        {
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
