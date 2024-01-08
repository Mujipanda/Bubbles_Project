using UnityEngine;

public class PlayerSettings : MonoBehaviour
{
    private const string SensitivityKey = "CameraSensitivity";
    [SerializeField]
    private float cameraSensitivity = 100f;

    private void Awake()
    {
        // Load the saved sensitivity on game launch
        cameraSensitivity = PlayerPrefs.GetFloat(SensitivityKey, cameraSensitivity);
    }

    public void UpdateSensitivity(float newSensitivity)
    {
        cameraSensitivity = newSensitivity;
        // Save new sensitivity value
        PlayerPrefs.SetFloat(SensitivityKey, cameraSensitivity);
        Debug.Log("New Sensitivity: " + cameraSensitivity);
    }

    public float GetSensitivity()
    {
        return cameraSensitivity;
    }
}