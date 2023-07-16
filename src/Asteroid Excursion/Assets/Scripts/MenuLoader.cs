using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MenuLoader : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.2f;

    public RectTransform menuContainer;
    public Transform levelPanel;
    
    public Transform colorPanel;
    public Transform trailPanel;

    public TextMeshProUGUI colorBuySetText;
    public TextMeshProUGUI trailBuySetText;

    private int[] colorCost = new int[] { 0, 5, 5, 5, 10, 10, 10, 15, 15, 20 };
    private int[] trailCost = new int[] { 0, 20, 40, 40, 60, 60, 80, 80, 100, 100 };
    private int selectedColorIndex;
    private int selectedTrailIndex;

    private Vector3 desiredMenuPosition;

    private void Start()
    {
        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1.0f;

        //Add button on click events
        InitShop();

        //Add buttons on click events
        InitLevel();
    }

    private void Update()
    {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);
    }

    private void InitShop()
    {
        if (colorPanel == null || trailPanel == null)
            Debug.Log("Either colorPanel or trailPanel is not assigned");

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

    private void InitLevel()
    {
        if (levelPanel == null)
            Debug.Log("levelPanel is not assigned");

        int i = 0;
        foreach (Transform t in levelPanel)
        {
            int currentIndex = i;
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnLevelSelect(currentIndex));
            i++;
        }
    }

    private void NavigateTo(int menuIndex)
    {
        switch (menuIndex)
        {
            default:
            case 0: //Main Menu
                desiredMenuPosition = Vector3.zero;
                break;
            case 1: //Play Menu
                desiredMenuPosition = Vector3.right * 1920;
                break;
            case 2: //Shop Menu
                desiredMenuPosition = Vector3.left * 1920;
                break;
        }
    }

    private void SetColor(int index)
    {
        colorBuySetText.text = "Current";
    }

    private void SetTrail(int index)
    {
        trailBuySetText.text = "Current";
    }   

    //Main Menu Buttons
    public void OnPlayClick()
    {
        NavigateTo(1);
        Debug.Log("Play Button is clicked");
    }

    public void OnShopClick()
    {
        NavigateTo(2);
        Debug.Log("Shop Button is clicked");
    }

    public void OnBackClick()
    {
        NavigateTo(0);
        Debug.Log("Back Button is clicked");
    }

    //Color and Trail Menu Buttons
    private void OnColorSelect(int currentIndex)
    {
        Debug.Log("You have selected color number : " + currentIndex);

        selectedColorIndex = currentIndex;
        if(SaveManager.Instance.isColorOwned(currentIndex))
        {
            colorBuySetText.text = "Select";
        }
        else
        {
            colorBuySetText.text = "Buy: " + colorCost[currentIndex].ToString();
        }   
    }

    private void OnTrailSelect(int currentIndex)
    {
        Debug.Log("You have selected trail number : " + currentIndex);

        selectedTrailIndex = currentIndex;
        if (SaveManager.Instance.isTrailOwned(currentIndex))
        {
            trailBuySetText.text = "Select";
        }
        else
        {
            trailBuySetText.text = "Buy: " + trailCost[currentIndex].ToString();
        }
    }

    public void OnColorBuySetClick()
    {
        Debug.Log("BUY/SET COLOR Selected");

        if (SaveManager.Instance.isColorOwned(selectedColorIndex))
        {
            SetColor(selectedColorIndex);
        }
        else
        {
            if(SaveManager.Instance.BuyColor(selectedColorIndex, colorCost[selectedColorIndex]))
            {
                SetColor(selectedColorIndex);
            }
            else
            {
                Debug.Log("Not enough currency to buy this color");
            }
        }
    }

    public void OnTrailBuySetClick()
    {
        Debug.Log("BUY/SET TRAIL Selected");

        if (SaveManager.Instance.isTrailOwned(selectedTrailIndex))
        {
            SetTrail(selectedTrailIndex);
        }
        else
        {
            if (SaveManager.Instance.BuyTrail(selectedTrailIndex, trailCost[selectedTrailIndex]))
            {
                SetTrail(selectedTrailIndex);
            }
            else
            {
                Debug.Log("Not enough currency to buy this trail");
            }
        }
    }

    //Level Menu Buttons

    private void OnLevelSelect(int currentIndex)
    {
        Debug.Log("You have selected level number : " + currentIndex);
    }
}
