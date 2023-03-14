using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public FMODUnity.EventReference jumpSound;
    public FMODUnity.EventReference walkSound;
    public FMODUnity.EventReference landSound;

    public FMOD.Studio.EventInstance walkInstance;
    public FMOD.Studio.EventInstance landInstance;

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
