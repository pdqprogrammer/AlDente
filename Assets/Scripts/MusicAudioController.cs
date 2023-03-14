using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicAudioController : MonoBehaviour
{
    [SerializeField]
    private FMODUnity.StudioEventEmitter emitter;

    // Update is called once per frame
    private void Update()
    {
        if(GameManager.CurrentGameState == GameStates.GAMEOVER || GameManager.CurrentGameState == GameStates.WIN)
        {
            if (emitter.IsPlaying())
            {
                emitter.AllowFadeout = true;
                emitter.Stop();
            }
        }
    }
}
