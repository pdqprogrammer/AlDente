using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flag : MonoBehaviour
{
    //We can tell it what scene to switch to
    public string nextScene;

    //Very simple script just listens to see if something touches it and shows a screen
    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Setting scene to " + nextScene);

        SceneManager.LoadScene(nextScene);
    }
}
