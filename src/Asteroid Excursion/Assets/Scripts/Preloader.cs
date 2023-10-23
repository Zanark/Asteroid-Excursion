using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Preloader : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float loadTime;
    private float logoStartTime = 3.0f;

    private void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1.0f;

        //Pre load the game

        if (Time.time < logoStartTime)
            loadTime = logoStartTime;
        else
            loadTime = Time.time; //This case is just a fail safe
    }

    private void Update()
    {
        //LOGO Fade-in
        if (Time.time < logoStartTime)
        {
            fadeGroup.alpha = 1.0f - Time.time;
        }

        //LOGO Fade-out
        if (Time.time > logoStartTime)
        {
            fadeGroup.alpha = Time.time - logoStartTime;
            if (fadeGroup.alpha >= 1)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
