using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameScene : MonoBehaviour
{
    
    private CanvasGroup fadeGroup;
    private float fadeInDuration = 2;
    private bool isReady = false;

    private void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1.0f;
    }

    private void Update()
    {
        if (Time.timeSinceLevelLoad <= fadeInDuration)
        {
            fadeGroup.alpha = 1 - (Time.timeSinceLevelLoad / fadeInDuration);
        }
        else if (!isReady)
        {
            fadeGroup.alpha = 0;
            isReady = true;
        }
    }

    public void CompleteLevel()
    {
        SaveManager.Instance.CompleteLevel(Manager.Instance.currentLevel);

        Manager.Instance.menuFocus = 1;

        ExitScene();
    }

    public void ExitScene()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
