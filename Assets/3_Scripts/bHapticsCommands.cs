using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;

public class bHapticsCommands : MonoBehaviour
{
    public Coroutine playingCoroutine;

    private float loopLength;



    public void PlayHaptic(string haptic)
    {
        BhapticsLibrary.Play(haptic);
    }

    public void SetLoopLength(float _loopLength)
    {
        loopLength = _loopLength;
    }


    public void PlayHapticLoop(string hapticLoop)
    {        
        playingCoroutine = StartCoroutine(PlayHapticLoopCoroutine(hapticLoop, loopLength));
    }

    public void StopHapticLoop()
    {
        
        StopCoroutine(playingCoroutine);
    }


    public IEnumerator PlayHapticLoopCoroutine(string hapticLoop, float loopLength)
    {
        BhapticsLibrary.Play(hapticLoop);
        yield return new WaitForSeconds(loopLength);
        PlayHapticLoop(hapticLoop);
    }
}
