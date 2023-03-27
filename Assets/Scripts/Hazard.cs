using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField]
    private HazardType hazardType = HazardType.DEATH;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if player has entered hazard
        if (collision.gameObject.tag.Equals("Player"))
        {
            //if hazard is death then change to death state
            if (hazardType == HazardType.DEATH)
            {
                Debug.Log("Died");
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                player.Died();

                GameManager.ChangeGameState(GameStates.GAMEOVER);
            }

            return;
        }

        if (collision.gameObject.tag.Equals("Platform"))
        {
            //if hazard is death then change to death state
            if (hazardType == HazardType.HEAT)
            {
                Platform platform = collision.gameObject.GetComponent<Platform>();
                platform.HeatPlatform();
            }
        }

        if (collision.gameObject.tag.Equals("Collectible"))
        {
            //if hazard is death then change to death state
            if (hazardType == HazardType.DEATH)
            {
                Collectible collectible = collision.gameObject.GetComponent<Collectible>();
                collectible.SetBoiled();
            }
        }
    }

    private enum HazardType
    {
        SLOW,
        HURT,
        DEATH,
        HEAT
    }
}