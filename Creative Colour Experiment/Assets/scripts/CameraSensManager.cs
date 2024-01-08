using UnityEngine;

public class CameraSensitivityManager : MonoBehaviour
{
    public playerMovement PlayerMovementScript;
    public PlayerSettings PlayerSettingsScript;

    void Update()
    {
        // Synchronize sensitivity value from PlayerSettings to playerMovement
        if (PlayerMovementScript && PlayerSettingsScript)
        {
            float currentSensitivity = PlayerSettingsScript.GetSensitivity();
            PlayerMovementScript.sensitivity = currentSensitivity * 200;
            Debug.Log("Applied Sensitivity: " + currentSensitivity);
        }
    }
}

