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
        //optionCanvas = optionCanvas.GetComponent<Canvas>();
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
        print("Start: " + PlayerScript.playerdata.pathogenType);
        startCanvas.enabled = true;
        //optionCanvas.enabled = false;
        aboutCanvas.enabled = false;
        volumeCanvas.enabled = false;

        if (PlayerScript.playerdata.pathogenType != Enums.CharacterType.None)
            startText.text = "Resume Game";
        else
            startText.text = "Start New Game";

        sfxSlider.value = PlayerScript.playerdata.effectsVolume;
        bgSlider.value = PlayerScript.playerdata.globalVolume;
        buttonSlider.value = PlayerScript.playerdata.buttonAlpha;
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
        print("before: " + PlayerScript.playerdata.pathogenType);
        if (PlayerScript.playerdata.pathogenType != Enums.CharacterType.None)
        {
            SaveLoadManager.EraseInformation();
            PlayerScript.playerdata = new PlayerData();
            startText.text = "Start New Game";
            print("Inside");

        }
        print("After: " + PlayerScript.playerdata.pathogenType);
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
        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
       // AudioListener.volume = bgSlider.value;
    }
}