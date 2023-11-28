using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    private const string SensitivityKey = "CameraSensitivity";
    [SerializeField]
    private float cameraSensitivity = 100f;

    private void Awake()
    {
        // Load the saved sensitivity value when the game starts
        cameraSensitivity = PlayerPrefs.GetFloat(SensitivityKey, cameraSensitivity);
    }

    public void UpdateSensitivity(float newSensitivity)
    {
        cameraSensitivity = newSensitivity;
        // Save the new sensitivity value
        PlayerPrefs.SetFloat(SensitivityKey, cameraSensitivity);
    }

    public float GetSensitivity()
    {
        return cameraSensitivity;
    }
}