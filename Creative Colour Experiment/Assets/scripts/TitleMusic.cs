using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMusic : MonoBehaviour
{
    private static TitleMusic backgroundMusic;

    void Awake()
    {
        if (backgroundMusic == null)
        {
            backgroundMusic = this;
            DontDestroyOnLoad(backgroundMusic);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
