﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager_Start : MonoBehaviour
{

    static UIManager_Start instance;
    //Canvas
    public Canvas optionCanvas, aboutCanvas, volumeCanvas, startCanvas;
    public Slider sfxSlider, bgSlider, buttonSlider;
    UnityEngine.UI.Button newGameButton, continueButton;

    UnityEngine.UI.Button[] startCanvasButtons, optionCanvasButtons;

    void Awake()
    {
        //optionCanvas = optionCanvas.GetComponent<Canvas>();
        aboutCanvas = aboutCanvas.GetComponent<Canvas>();
        volumeCanvas = volumeCanvas.GetComponent<Canvas>();

        sfxSlider = sfxSlider.GetComponent<Slider>();
        bgSlider = bgSlider.GetComponent<Slider>();
        buttonSlider = buttonSlider.GetComponent<Slider>();

        sfxSlider.onValueChanged.AddListener(delegate { SfxSliderChange(); });
        bgSlider.onValueChanged.AddListener(delegate { BackgroundSliderChange(); });
        buttonSlider.onValueChanged.AddListener(delegate { ButtonSiderChange(); });

        newGameButton = GameObject.Find("Start/StartButton").GetComponent<UnityEngine.UI.Button>();
        continueButton = GameObject.Find("Start/ContinueButton").GetComponent<UnityEngine.UI.Button>();
    }

    void Start()
    {
        print("Start: " + PlayerScript.playerdata.pathogenType);
        startCanvas.enabled = true;
        optionCanvas.enabled = false;
        aboutCanvas.enabled = false;
        volumeCanvas.enabled = false;

        newGameButton.enabled = continueButton.enabled = false;

        if (PlayerScript.playerdata.pathogenType != Enums.CharacterType.None)
        {
            continueButton.enabled = true;
            newGameButton.enabled = false;
            newGameButton.gameObject.SetActive(false);
        }
        else
        {
            newGameButton.enabled = true;
            continueButton.enabled = false;
            continueButton.gameObject.SetActive(false);
        }

        sfxSlider.value = PlayerScript.playerdata.effectsVolume;
        bgSlider.value = PlayerScript.playerdata.globalVolume;
        buttonSlider.value = PlayerScript.playerdata.buttonAlpha;

        GetStartComponents();
        GetOptionComponents();
    }
    
    void GetOptionComponents()
    {
        optionCanvasButtons = optionCanvas.GetComponentsInChildren<UnityEngine.UI.Button>();
    }

    void GetStartComponents()
    {
        startCanvasButtons = startCanvas.GetComponentsInChildren<UnityEngine.UI.Button>();
    }

    void SetButtons(Canvas canvas, bool set)
    {
        if (canvas == startCanvas)
        {
            for (int i = 0; i < startCanvasButtons.Length; i++)
            {
                startCanvasButtons[i].interactable = set;
            }
        }
        else if (canvas == optionCanvas)
        {
            for (int i = 0; i < optionCanvasButtons.Length; i++)
            {
                optionCanvasButtons[i].interactable = set;
            }
        }
    }

    /*******************************Start components******************************/
    public void Start_ExitPress()
    {
        Application.Quit();
    }
    public void Start_StartPress()
    {
        if (PlayerScript.playerdata.pathogenType != Enums.CharacterType.None)
            Application.LoadLevel("MainMapOverview");
        else
            Application.LoadLevel("TroopSelection_Start");

    }

    public void Start_OptionPress()
    {
        //disable components in canvas
        SetButtons(startCanvas, false);
        optionCanvas.enabled = true;

    }

    /*******************************Option components******************************/

    public void Option_BackPress()
    {
        optionCanvas.enabled = false;
        startCanvas.enabled = true;
        SetButtons(startCanvas, true);
    }

    public void Option_ErasePress()
    {
        //create modal for confirmation here
        print("before: " + PlayerScript.playerdata.pathogenType);
        if (PlayerScript.playerdata.pathogenType != Enums.CharacterType.None)
        {
            SaveLoadManager.EraseInformation();
            PlayerScript.playerdata = new PlayerData();
            print("Inside");

        }
        print("After: " + PlayerScript.playerdata.pathogenType);
    }

    public void Option_VolumePress()
    {
        volumeCanvas.enabled = true;
        optionCanvas.enabled = false;
        SetButtons(optionCanvas, false);
    }

    public void Option_AboutPress()
    {
        aboutCanvas.enabled = true;
        optionCanvas.enabled = false;
    }

    /***************************About Component**********************************/
    public void About_BackPress()
    {
        aboutCanvas.enabled = false;
        optionCanvas.enabled = true;
        print("Clicked back in about page");
    }

    /*******************************Volume components******************************/
    public void SfxSliderChange()
    {
        PlayerScript.playerdata.effectsVolume = sfxSlider.value;
    }

    public void BackgroundSliderChange()
    {
        PlayerScript.playerdata.globalVolume = bgSlider.value;
    }

    public void ButtonSiderChange()
    {
        PlayerScript.playerdata.buttonAlpha = buttonSlider.value;
    }

    public void Volume_BackPress()
    {
        volumeCanvas.enabled = false;
        optionCanvas.enabled = true;
        SetButtons(optionCanvas, true);
        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
       // AudioListener.volume = bgSlider.value;
    }
}