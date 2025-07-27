using UnityEngine;
using Yarn.Unity;
using System.Collections.Generic;
using System.Collections;

public class YarnCommandsHolder : MonoBehaviour
{
    public InMemoryVariableStorage yarnVariableStorage;         // Reference to the variable storage for Yarn commands
    public DialogueRunner dialogueRunner;                       // Reference to the DialogueRunner component
    public AudioSource monologueAudio;                          // AudioSource with the monologue clip
    public CanvasGroup fadeCanvasGroup;                         // For fade in/out (optional)

    void Awake()
    {
        // Register Yarn commands
        dialogueRunner.AddCommandHandler("startIntro", StartIntroSequence);
    }

    [YarnCommand("startIntro")] // Yarn command to start the intro sequence
    void StartIntroSequence()
    {
        StartCoroutine(IntroCoroutine());
    }

    IEnumerator IntroCoroutine()
    {
        // Fade screen to black (optional)
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 1f;
            fadeCanvasGroup.gameObject.SetActive(true);
        }

        // Play monologue
        if (monologueAudio != null)
            monologueAudio.Play();

        yield return new WaitForSeconds(60f); // Wait during monologue

        // Fade screen back in
        if (fadeCanvasGroup != null)
        {
            fadeCanvasGroup.alpha = 0f;
            fadeCanvasGroup.gameObject.SetActive(false);
        }

        // Start Yarn dialogue after intro
        //var dialogueRunner = FindObjectOfType<DialogueRunner>();
        dialogueRunner.StartDialogue("InterrogationStart");
    }
}
