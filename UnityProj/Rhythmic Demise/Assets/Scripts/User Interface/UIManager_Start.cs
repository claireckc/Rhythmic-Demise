using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager_Start : MonoBehaviour
{
    //Canvas
    public Canvas optionCanvas, aboutCanvas, volumeCanvas, startCanvas;
    public Slider sfxSlider, bgSlider, buttonSlider;
    Text startText;

    UnityEngine.UI.Button confirmButton, cancelButton;
    GameObject eraseModel;

    AudioSource selectClick, bgmUI, stageMusic, bossMusic;


    void Awake()
    {
        sfxSlider = sfxSlider.GetComponent<Slider>();
        bgSlider = bgSlider.GetComponent<Slider>();
        buttonSlider = buttonSlider.GetComponent<Slider>();

        sfxSlider.onValueChanged.AddListener(delegate { SfxSliderChange(); });
        bgSlider.onValueChanged.AddListener(delegate { BackgroundSliderChange(); });
        buttonSlider.onValueChanged.AddListener(delegate { ButtonSiderChange(); });
        eraseModel = GameObject.Find("OptionCanvas/ConfirmPanel");
        confirmButton = GameObject.Find("OptionCanvas/ConfirmPanel/YesButton").GetComponent<UnityEngine.UI.Button>();
        cancelButton = GameObject.Find("OptionCanvas/ConfirmPanel/NoButton").GetComponent<UnityEngine.UI.Button>();


        startText = GameObject.Find("Start/StartButton").GetComponent<Text>();
    }

    void Start()
    {
        SetupAudio();
        startCanvas.enabled = true;
        optionCanvas.enabled = false;
        aboutCanvas.enabled = false;
        volumeCanvas.enabled = false;
        eraseModel.SetActive(false);

        if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.None)
        {
            startText.text = "Start New Game";
        }
        else
        {
            startText.text = "Continue Game";
        }

        sfxSlider.value = PlayerScript.playerdata.effectsVolume;
        bgSlider.value = PlayerScript.playerdata.globalVolume;
        buttonSlider.value = PlayerScript.playerdata.buttonAlpha;

    }
    void SetupAudio()
    {
        selectClick = GameObject.Find("UI Music/Select").GetComponent<AudioSource>();
        bgmUI = GameObject.Find("UI Music/BGM").GetComponent<AudioSource>();
        stageMusic = GameObject.Find("UI Music/Stage Music").GetComponent<AudioSource>();
        bossMusic = GameObject.Find("UI Music/Boss Music").GetComponent<AudioSource>();

        selectClick.volume = PlayerScript.playerdata.effectsVolume;
        bgmUI.volume = PlayerScript.playerdata.globalVolume;

        if (!bgmUI.isPlaying)
            bgmUI.Play();
    }

    void PlaySelectAudio()
    {
        selectClick.Play();
    }

    void DisableAllInteractions(Canvas canvas)
    {
        UnityEngine.UI.Button[] allButtons = canvas.GetComponentsInChildren<UnityEngine.UI.Button>();

        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].interactable = false;
        }
    }

    void EnableAllInteractions(Canvas canvas)
    {
        UnityEngine.UI.Button[] allButtons = canvas.GetComponentsInChildren<UnityEngine.UI.Button>();

        for (int i = 0; i < allButtons.Length; i++)
        {
            allButtons[i].interactable = true;
        }
    }
    /*******************************Start components******************************/
    public void Start_ExitPress()
    {
        PlaySelectAudio();
        Application.Quit();
    }
    public void Start_StartPress()
    {
        PlaySelectAudio();

        if (PlayerScript.playerdata.pathogenType != Enums.CharacterType.None)
            Application.LoadLevel("MainMapOverview");
        else
            Application.LoadLevel("TroopSelection_Start");

    }

    public void Start_OptionPress()
    {
        PlaySelectAudio();
        //disable components in canvas
        DisableAllInteractions(startCanvas);
        optionCanvas.enabled = true;

    }

    /*******************************Option components******************************/

    public void Option_BackPress()
    {
        PlaySelectAudio();
        optionCanvas.enabled = false;
        startCanvas.enabled = true;
        EnableAllInteractions(startCanvas);
    }

    public void Option_ErasePress()
    {
        PlaySelectAudio();
        //create modal for confirmation here
        SaveLoadManager.EraseInformation();
        PlayerScript.playerdata = new PlayerData();
        startText.text = "Start New Game";
        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);

        optionCanvas.enabled = false;
        startCanvas.enabled = true;
        EnableAllInteractions(startCanvas);
    }

    public void Option_VolumePress()
    {
        PlaySelectAudio();
        volumeCanvas.enabled = true;
        optionCanvas.enabled = false;
        DisableAllInteractions(optionCanvas);
    }

    public void Option_AboutPress()
    {
        PlaySelectAudio();
        aboutCanvas.enabled = true;
        optionCanvas.enabled = false;
    }

    public void ShowEraseModal()
    {
        PlaySelectAudio();
        eraseModel.SetActive(true);
        DisableAllInteractions(optionCanvas);
        confirmButton.interactable = true;
        cancelButton.interactable = true;
    }

    public void CancelDelete()
    {
        PlaySelectAudio();
        eraseModel.SetActive(false);
        EnableAllInteractions(optionCanvas);
    }

    /***************************About Component**********************************/
    public void About_BackPress()
    {
        PlaySelectAudio();
        aboutCanvas.enabled = false;
        optionCanvas.enabled = true;
        EnableAllInteractions(optionCanvas);
    }

    /*******************************Volume components******************************/
    public void SfxSliderChange()
    {
        PlayerScript.playerdata.effectsVolume = sfxSlider.value;
        UpdateVolume();
    }

    public void BackgroundSliderChange()
    {
        PlayerScript.playerdata.globalVolume = bgSlider.value;
        UpdateVolume();
    }

    public void ButtonSiderChange()
    {
        PlayerScript.playerdata.buttonAlpha = buttonSlider.value;
    }

    public void Volume_BackPress()
    {
        PlaySelectAudio();
        volumeCanvas.enabled = false;
        optionCanvas.enabled = true;
        EnableAllInteractions(optionCanvas);
        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
    }

    void UpdateVolume()
    {
        selectClick.volume = PlayerScript.playerdata.effectsVolume;
        bgmUI.volume = PlayerScript.playerdata.globalVolume;
        stageMusic.volume = PlayerScript.playerdata.globalVolume / 2;
        bossMusic.volume = PlayerScript.playerdata.globalVolume / 2;

    }
}