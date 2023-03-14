using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D collision)
    {
        //If player reaches goal then change to win state
        if (collision.gameObject.tag.Equals("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            player.Win();

            GameManager.ChangeGameState(GameStates.WIN);
        }
    }
}
