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

    public FMOD.Studio.EventInstance walkInstance;
    public FMOD.Studio.EventInstance landInstance;

    private int currJumpSound = 0;

    public void PlayAudio(SoundStates soundState)
    {
        switch (soundState)
        {
            case SoundStates.JUMP:
                FMODUnity.RuntimeManager.PlayOneShot(jumpSound);
                break;
            case SoundStates.WALK:
                walkInstance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE playBackState);

                if (playBackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                {
                    walkInstance = FMODUnity.RuntimeManager.CreateInstance(walkSound);
                    walkInstance.start();
                }
                break;
            case SoundStates.LAND:
                landInstance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE landPlayBackState);

                if (landPlayBackState == FMOD.Studio.PLAYBACK_STATE.STOPPED)
                {
                    landInstance = FMODUnity.RuntimeManager.CreateInstance(landSound);
                    landInstance.start();
                    landInstance.release();
                }
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

    //function used to stop walking animation
    public void StopWalkAudio()
    {
        walkInstance.getPlaybackState(out FMOD.Studio.PLAYBACK_STATE playBackState);

        if (playBackState == FMOD.Studio.PLAYBACK_STATE.PLAYING)
        {
            //walkInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        }

        walkInstance.release();
    }
}

public enum SoundStates
{
    JUMP,
    WALK,
    LAND,
    COLLECT,
}
