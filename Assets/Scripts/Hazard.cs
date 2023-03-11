using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField]
    private HazardType hazardType = HazardType.DEATH;

    //TODO consider value for heat that can impact the player

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //check if player has entered hazard
        if (collision.gameObject.tag.Equals("Player"))
        {
            //if hazard is death then change to death state
            if (hazardType == HazardType.DEATH)
            {
                Debug.Log("Died");

                GameManager.ChangeGameState(GameStates.GAMEOVER);
                //TODO add in UI Manager to set state
                GameManager.ResetGame();
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
    }

    private enum HazardType
    {
        SLOW,
        HURT,
        DEATH,
        HEAT
    }
}