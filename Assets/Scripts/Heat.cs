using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heat : MonoBehaviour
{
    [SerializeField]
    private float heatSpeed = 1.0f;

    [SerializeField]
    private float timeUntilStart = 1.0f;

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.CurrentGameState == GameStates.INGAME)
        {
            Vector3 direction = new Vector3(0, Time.deltaTime * heatSpeed, 0);
            Vector3 updatedPosition = transform.position + direction;
            transform.position = updatedPosition;
        }
    }
}
