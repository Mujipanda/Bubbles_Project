using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject PauseUI;
    public bool gamePaused = false;

    void OnPause()
    {
        Debug.Log("pause");
        switch (gamePaused)
        {
            case true:
                PauseUI.SetActive(false);
                Time.timeScale = 1f;
                gamePaused = false;
                break;


            case false:
                PauseUI.SetActive(true);
                Time.timeScale = 0f;
                gamePaused = true;
                break;
        }
    }
}
