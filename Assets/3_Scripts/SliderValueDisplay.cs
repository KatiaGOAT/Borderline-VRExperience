using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SliderValueDisplay : MonoBehaviour
{
    public Slider slider;                   // Assign your Slider in Inspector
    public TMP_Text valueText;             // Assign your TextMeshPro text in Inspector

    void Start()
    {
        UpdateValueText(slider.value);  // Set initial value display
        slider.onValueChanged.AddListener(UpdateValueText);  // Listen for changes
    }

    void UpdateValueText(float value)
    {
        valueText.text = value.ToString("0");  // No decimal places; use "0.0" for 1 decimal place
    }
}
