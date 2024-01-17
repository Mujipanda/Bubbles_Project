
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("load", 2f);
    }

 void load()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
