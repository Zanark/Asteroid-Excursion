using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MenuLoader : MonoBehaviour
{
    private CanvasGroup fadeGroup;
    private float fadeInSpeed = 0.2f;
    private float selectedScale = 1.8f;

    public RectTransform menuContainer;
    public Transform levelPanel;
    
    public Transform colorPanel;
    public Transform trailPanel;

    public TextMeshProUGUI colorBuySetText;
    public TextMeshProUGUI trailBuySetText;
    public TextMeshProUGUI currencyText;

    private MenuCamera menuCam;

    private int[] colorCost = new int[] { 0, 5, 5, 5, 10, 10, 10, 15, 15, 20 };
    private int[] trailCost = new int[] { 0, 20, 40, 40, 60, 60, 80, 80, 100, 100 };
    private int selectedColorIndex;
    private int selectedTrailIndex;
    private int activeColorIndex;
    private int activeTrailIndex;

    private Vector3 desiredMenuPosition;

    public AnimationCurve enteringLevelZoomCurve;
    private bool isEnteringLevel = false;
    private float zoomDuration = 3.0f;
    private float zoomTransition;

    private void Start()
    {
        menuCam = FindObjectOfType<MenuCamera>();
        
        //Temporary Currency for testing
        SaveManager.Instance.state.currency = 999;

        //Position the camera to the desired menu
        SetCameraTo(Manager.Instance.menuFocus);

        fadeGroup = FindObjectOfType<CanvasGroup>();
        fadeGroup.alpha = 1.0f;

        //Add button on click events
        InitShop();

        //Add buttons on click events
        InitLevel();

        //Update Currency
        UpdateCurrencyText();
    }

    private void Update()
    {
        fadeGroup.alpha = 1 - Time.timeSinceLevelLoad * fadeInSpeed;

        menuContainer.anchoredPosition3D = Vector3.Lerp(menuContainer.anchoredPosition3D, desiredMenuPosition, 0.1f);

        if (isEnteringLevel)
        {
            zoomTransition += (1 / zoomDuration) * Time.deltaTime;

            menuContainer.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 5, enteringLevelZoomCurve.Evaluate(zoomTransition));

            Vector3 newDesiredPosition = desiredMenuPosition * 5;
            RectTransform rt = levelPanel.GetChild(Manager.Instance.currentLevel).GetComponent<RectTransform>();
            newDesiredPosition -= rt.anchoredPosition3D * 5;

            menuContainer.anchoredPosition3D = Vector3.Lerp(desiredMenuPosition, newDesiredPosition, enteringLevelZoomCurve.Evaluate(zoomTransition));

            fadeGroup.alpha = zoomTransition;

            if (zoomTransition >= 1)
            {
                SceneManager.LoadScene("Game");
            }
        }
    }

    private void InitShop()
    {
        if (colorPanel == null || trailPanel == null)
            Debug.Log("Either colorPanel or trailPanel is not assigned");

        int i = 0;
        foreach (Transform t in colorPanel)
        {
            int currentIndex = i;
            
            //Add listeners to Buttons
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnColorSelect(currentIndex));
            
            //Update owned/not owned colors
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.isColorOwned(i) ? Color.white : new Color(0.3f, 0.3f, 0.3f);

            i++;
        }
        i = 0;
        foreach (Transform t in trailPanel)
        {
            int currentIndex = i;
            
            //Add listeners to Buttons
            Button b = t.GetComponent<Button>();
            b.onClick.AddListener(() => OnTrailSelect(currentIndex));

            //Update owned/not owned trails
            Image img = t.GetComponent<Image>();
            img.color = SaveManager.Instance.isTrailOwned(i) ? Color.white : new Color(0.3f, 0.3f, 0.3f);

            i++;
        }

        //Set player prefs
        OnColorSelect(SaveManager.Instance.state.activeColor);
        SetColor(SaveManager.Instance.state.activeColor);
        colorPanel.GetChild(SaveManager.Instance.state.activeColor).GetComponent<RectTransform>().localScale = Vector3.one * selectedScale;
        colorPanel.GetChild(SaveManager.Instance.state.activeColor).GetComponent<Image>().color = Color.white;

        OnTrailSelect(SaveManager.Instance.state.activeTrail);
        SetTrail(SaveManager.Instance.state.activeTrail);
        trailPanel.GetChild(SaveManager.Instance.state.activeTrail).GetComponent<RectTransform>().localScale = Vector3.one * selectedScale;
        trailPanel.GetChild(SaveManager.Instance.state.activeTrail).GetComponent<Image>().color = Color.white;
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
            
            Image img = t.GetComponent<Image>();
            
            if (i <= SaveManager.Instance.state.completedLevel)
            {
                if (i == SaveManager.Instance.state.completedLevel)
                {
                    img.color = Color.white;
                }
                else
                {
                    img.color = Color.magenta;
                }
            }
            else
            {
                b.interactable = false;
                img.color = Color.grey;

            }
            
            i++;
        }
    }

    private void SetCameraTo(int menuIndex)
    {
        NavigateTo(menuIndex);
        menuContainer.anchoredPosition3D = desiredMenuPosition;
    }
    
    private void NavigateTo(int menuIndex)
    {
        switch (menuIndex)
        {
            default:
            case 0: //Main Menu
                desiredMenuPosition = Vector3.zero;
                menuCam.BackToMainMenu();
                break;
            case 1: //Play Menu
                desiredMenuPosition = Vector3.right * 1920;
                menuCam.MoveToLevel();
                break;
            case 2: //Shop Menu
                desiredMenuPosition = Vector3.left * 1920;
                menuCam.MoveToShop();
                break;
        }
    }

    private void SetColor(int index)
    {
        colorBuySetText.text = "Current";
        SaveManager.Instance.state.activeColor = activeColorIndex = index;
        SaveManager.Instance.Save();
    }

    private void SetTrail(int index)
    {
        trailBuySetText.text = "Current";
        SaveManager.Instance.state.activeTrail = activeTrailIndex = index;
        SaveManager.Instance.Save();
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

        if(selectedColorIndex == currentIndex)
            return;

        //Change size of selectd and unselected color
        colorPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * selectedScale;
        colorPanel.GetChild(selectedColorIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        selectedColorIndex = currentIndex;
        if(SaveManager.Instance.isColorOwned(currentIndex))
        {
            if (activeColorIndex == currentIndex)
                colorBuySetText.text = "Current";
            else
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

        if (selectedTrailIndex == currentIndex)
            return;

        //Change size of selectd and unselected color
        trailPanel.GetChild(currentIndex).GetComponent<RectTransform>().localScale = Vector3.one * selectedScale;
        trailPanel.GetChild(selectedTrailIndex).GetComponent<RectTransform>().localScale = Vector3.one;

        selectedTrailIndex = currentIndex;
        if (SaveManager.Instance.isTrailOwned(currentIndex))
        {
            if (activeTrailIndex == currentIndex)
                trailBuySetText.text = "Current";
            else
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
                colorPanel.GetChild(selectedColorIndex).GetComponent<Image>().color = Color.white;
                UpdateCurrencyText();
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
                trailPanel.GetChild(selectedTrailIndex).GetComponent<Image>().color = Color.white;
                UpdateCurrencyText();
            }
            else
            {
                Debug.Log("Not enough currency to buy this trail");
            }
        }
    }

    private void UpdateCurrencyText()
    {
        currencyText.text = SaveManager.Instance.state.currency.ToString();
    }   

    //Level Menu Buttons
    private void OnLevelSelect(int currentIndex)
    {
        Manager.Instance.currentLevel = currentIndex;
        isEnteringLevel = true;
        Debug.Log("You have selected level number : " + currentIndex);
    }
}
