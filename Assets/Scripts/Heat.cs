using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heat : MonoBehaviour
{
    [SerializeField]
    private float heatSpeed = 1.0f;

    [SerializeField]
    private float timeUntilStart = 1.0f;

    [SerializeField]
    private Transform playerTransform = null;

    [SerializeField]
    private FMODUnity.StudioEventEmitter emitter;

    // Update is called once per frame
    private void Update()
    {
        if (GameManager.CurrentGameState == GameStates.INGAME)
        {
            if (timeUntilStart < 0)
            {
                Vector3 direction = new Vector3(0, Time.deltaTime * heatSpeed, 0);
                Vector3 updatedPosition = transform.position + direction;
                transform.position = updatedPosition;
                SetEmitterDistance();
            }
            else
            {
                timeUntilStart -= Time.deltaTime;
            }
        }
    }

    private void SetEmitterDistance()
    {
        Vector2 normalizedDistance = (playerTransform.position - transform.position).normalized;

        emitter.SetParameter("Distance", normalizedDistance.y);
    }
}
