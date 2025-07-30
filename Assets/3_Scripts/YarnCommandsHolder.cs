using UnityEngine;
using Yarn.Unity;
using System.Collections.Generic;
using System.Collections;

public class YarnCommandsHolder : MonoBehaviour
{
    public InMemoryVariableStorage yarnVariableStorage;         // Reference to the variable storage for Yarn commands
    public DialogueRunner dialogueRunner;                       // Reference to the DialogueRunner component
    public AudioSource monologueAudio;                          // AudioSource with the monologue clip
    //public CanvasGroup fadeCanvasGroup;                         // For fade in/out (optional)

    public bHapticsCommands bhaptics;               // Reference to the bHapticsCommands script for haptic feedback

    public GameObject feedbackTransitionUI;                     // Reference to the UI panel for transitioning to feedback scene
    public GameObject officerNPC;                               // Reference to the officer NPC GameObject
    //void Awake()
    //{
    //    // Register Yarn commands
    //    dialogueRunner.AddCommandHandler("startIntro", StartIntroSequence);
    //}

    [YarnCommand("startIntro")] // Yarn command to start the intro sequence
    public void StartIntroSequence()
    {
        StartCoroutine(IntroCoroutine());
    }

    IEnumerator IntroCoroutine()
    {
        //disable the officer npc
        if (officerNPC != null)
        {
            officerNPC.SetActive(false);
        }

        // Play monologue
        if (monologueAudio != null)
            monologueAudio.Play();

        yield return new WaitForSeconds(60f); // Wait during monologue

        //activate the officer npc
        if (officerNPC != null)
        {
            officerNPC.SetActive(true);
        }

        // Start Yarn dialogue after intro

        //var dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.StartDialogue("InterrogationStart");
    }

    [YarnCommand("transitionToFeedback")] // Yarn command to start the transition to feedback scene
    public void TransitionToFeedback()
    {
        // For example, you might load a new scene or activate a feedback UI panel
        Debug.Log("Transitioning to feedback scene...");

        //disable the officer npc
        if (officerNPC != null)
        {
            officerNPC.SetActive(false);
        }

        //activate ui to take the user to feedback scene
        feedbackTransitionUI.SetActive(true);
    }

    [YarnCommand("fast_heartbeat")]
    public void FastHeartbeat()
    {
        bhaptics.FastLoop(); // Set the loop length to 0.5 seconds for fast heartbeat

    }

    [YarnCommand("normal_heartbeat")]
    public void NormalHeartbeat()
    {
        bhaptics.NormalLoop(); // Set the loop length to 1 seconds for normal heartbeat

    }
}
