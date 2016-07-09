using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager_Start : MonoBehaviour
{

    static UIManager_Start instance;
    //Canvas
    public Canvas optionCanvas, aboutCanvas, volumeCanvas, startCanvas;
    public Slider sfxSlider, bgSlider, buttonSlider;
    public Text startText;

    void Awake()
    {
        startText = startText.GetComponent<Text>();
        optionCanvas = optionCanvas.GetComponent<Canvas>();
        aboutCanvas = aboutCanvas.GetComponent<Canvas>();
        volumeCanvas = volumeCanvas.GetComponent<Canvas>();

        sfxSlider = sfxSlider.GetComponent<Slider>();
        bgSlider = bgSlider.GetComponent<Slider>();
        buttonSlider = buttonSlider.GetComponent<Slider>();

        sfxSlider.onValueChanged.AddListener(delegate { SfxSliderChange(); });
        bgSlider.onValueChanged.AddListener(delegate { BackgroundSliderChange(); });
        buttonSlider.onValueChanged.AddListener(delegate { ButtonSiderChange(); });

    }

    void Start()
    {
        print("Start: " + PlayerData.playerdata.pathogenType);
        startCanvas.enabled = true;
        optionCanvas.enabled = false;
        aboutCanvas.enabled = false;
        volumeCanvas.enabled = false;

        if (PlayerData.playerdata.pathogenType != Enums.CharacterType.None)
            startText.text = "Resume Game";
        else
            startText.text = "Start New Game";

        sfxSlider.value = PlayerData.playerdata.effectsVolume;
        bgSlider.value = PlayerData.playerdata.globalVolume;
        buttonSlider.value = PlayerData.playerdata.buttonAlpha;
    }

    /*******************************Start components******************************/
    public void Start_ExitPress()
    {
        Application.Quit();
    }
    public void Start_StartPress()
    {
        if (startText.text == "Resume Game")
            Application.LoadLevel("MainMapOverview");
        else
            Application.LoadLevel("TroopSelection_Start");

    }

    public void Start_OptionPress()
    {
        //disable components in canvas
        // startCanvas.enabled = false;
        optionCanvas.enabled = true;
        startCanvas.enabled = false;

    }

    /*******************************Option components******************************/

    public void Option_BackPress()
    {
        optionCanvas.enabled = false;
        startCanvas.enabled = true;
    }

    public void Option_ErasePress()
    {
        print("before: " + PlayerData.playerdata.pathogenType);
        if (PlayerData.playerdata.pathogenType != Enums.CharacterType.None)
        {
            PlayerData.playerdata.pathogenType = Enums.CharacterType.None;
            startText.text = "Start New Game";
            print("Inside");

        }
        print("After: " + PlayerData.playerdata.pathogenType);
    }

    public void Option_VolumePress()
    {
        volumeCanvas.enabled = true;
        optionCanvas.enabled = false;
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
        PlayerData.playerdata.effectsVolume = sfxSlider.value;
    }

    public void BackgroundSliderChange()
    {
        PlayerData.playerdata.globalVolume = bgSlider.value;
    }

    public void ButtonSiderChange()
    {
        PlayerData.playerdata.buttonAlpha = buttonSlider.value;
    }

    public void Volume_BackPress()
    {
        volumeCanvas.enabled = false;
        optionCanvas.enabled = true;
        AudioListener.volume = bgSlider.value;
    }
}