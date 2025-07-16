using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // Call this function from your button with the name of the target scene
    public void GoToOnboardingScene()
    {
        SceneManager.LoadScene("2_OnboardingScene");
    }

    public void GoToCellScene()
    {
        SceneManager.LoadScene("3_HoldingCellScene");
    }

}
