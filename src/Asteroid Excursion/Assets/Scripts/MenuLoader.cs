using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuLoader : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.2f;

    public Transform colorPanel;
    public Transform trailPanel;

    private void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1.0f;

        //Add button on click events
        InitShop();
    }

    private void Update()
    {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;
    }

    private void InitShop()
    {
        if (colorPanel == null || trailPanel == null)
            Debug.Log("Either colorPanel or trailPanel is Unassigned");

        int i = 0;
        foreach (Transform t in colorPanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnColorSelect(currentIndex));
            i++;
        }
        i = 0;
        foreach (Transform t in trailPanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnTrailSelect(currentIndex));
            i++;
        }
    }

    //Main Menu Buttons
    public void OnPlayClick()
    {
        Debug.Log("Play Button is clicked");
    }

    public void OnShopClick()
    {
        Debug.Log("Shop Button is clicked");
    }

    private void OnColorSelect(int currentIndex)
    {
        Debug.Log("You have selected color number : " + currentIndex);
    }

    private void OnTrailSelect(int currentIndex)
    {
        Debug.Log("You have selected trail number : " + currentIndex);
    }

    public void OnColorBuySetClick()
    {
        Debug.Log("BUY/SET COLOR Selected");
    }

    public void OnTrailBuySetClick()
    {
        Debug.Log("BUY/SET TRAIL Selected");
    }
}
