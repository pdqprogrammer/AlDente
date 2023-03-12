using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public FMODUnity.EventReference jumpSound;
    public FMODUnity.EventReference walkSound;
    public FMODUnity.EventReference landSound;
    public FMODUnity.EventReference collect1Sound;
    public FMODUnity.EventReference collect2Sound;
    public FMODUnity.EventReference collect3Sound;

    private int currJumpSound = 0;

    public void PlayAudio(SoundStates soundState)
    {
        switch (soundState)
        {
            case SoundStates.JUMP:
                FMODUnity.RuntimeManager.PlayOneShot(jumpSound);
                break;
            case SoundStates.WALK:
                FMODUnity.RuntimeManager.PlayOneShot(walkSound);
                break;
            case SoundStates.LAND:
                FMODUnity.RuntimeManager.PlayOneShot(landSound);
                break;
            case SoundStates.COLLECT:
                if(currJumpSound == 0)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(collect1Sound);
                }
                else if(currJumpSound == 1)
                {
                    FMODUnity.RuntimeManager.PlayOneShot(collect2Sound);
                }
                else
                {
                    FMODUnity.RuntimeManager.PlayOneShot(collect3Sound);
                }

                currJumpSound++;

                if(currJumpSound > 2)
                {
                    currJumpSound = 1;
                }
                break;
        }
    }
}

public enum SoundStates
{
    JUMP,
    WALK,
    LAND,
    COLLECT,
}
