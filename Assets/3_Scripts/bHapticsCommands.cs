using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bhaptics.SDK2;

public class bHapticsCommands : MonoBehaviour
{
    public Coroutine playingCoroutine;

    private float loopLength;


    private void Start()
    {
        SetLoopLength(1.0f); // Default loop length of 1 second
        PlayHapticLoop("normal_heartbeat");
    }


    public void PlayHaptic(string haptic)
    {
        BhapticsLibrary.Play(haptic);
    }

    public void SetLoopLength(float _loopLength)
    {
        loopLength = _loopLength;
    }

    public void FastLoop()
    {
        loopLength = 0.5f; // Set loop length to 0.5 seconds for fast loop  
    }

    public void NormalLoop()
    {
        loopLength = 1f; // Set loop length to 0.5 seconds for fast loop  
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
