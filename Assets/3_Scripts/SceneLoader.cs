using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Call this function from your button with the name of the target scene
    public void GoToStartScene()
    {
        SceneManager.LoadScene("1_StartScene");
    }
    public void GoToOnboardingScene()
    {
        SceneManager.LoadScene("2_OnboardingScene");
    }

    public void GoToCellScene()
    {
        SceneManager.LoadScene("3_HoldingCellScene");
    }

    public void GoToFeedbackScene()
    {
        SceneManager.LoadScene("4_FeedbackScene");
    }
    public void QuitApplication()
    {
        // Quit the application
        Application.Quit();

        // For testing in the Unity Editor, stop Play mode
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

}
