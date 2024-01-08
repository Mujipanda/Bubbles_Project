using UnityEngine;
using UnityEngine.UI;

public class SliderFill : MonoBehaviour
{
    public Slider sensitivitySlider;
    public PlayerSettings playerSettings;

    private void Start()
    {
        sensitivitySlider.value = playerSettings.GetSensitivity();
        sensitivitySlider.onValueChanged.AddListener(UpdateSensitivity);
    }

    private void UpdateSensitivity(float newSensitivity)
    {
        playerSettings.UpdateSensitivity(newSensitivity);
    }
}
