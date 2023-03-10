using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hazard : MonoBehaviour
{
    [SerializeField]
    private HazardType hazardType = HazardType.DEATH;

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
                
                PlayerController player = collision.gameObject.GetComponent<PlayerController>();
                player.ResetPos();
            }
        }
    }

    private enum HazardType
    {
        SLOW,
        HURT,
        DEATH
    }
}