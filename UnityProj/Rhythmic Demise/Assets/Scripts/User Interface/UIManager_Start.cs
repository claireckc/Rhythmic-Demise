using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager_Start : MonoBehaviour {

    static UIManager_Start instance;
    public PlayerData playerdata;

    //Canvas
    private Canvas optionCanvas, aboutCanvas, volumeCanvas;
    private Slider sfxSlider, bgSlider, buttonSlider;
    public Text startText;

    void Awake()
    {
        playerdata = playerdata.GetComponent<PlayerData>();
        startText = startText.GetComponent<Text>();
        aboutCanvas = GameObject.Find("AboutScreen").GetComponent<Canvas>();
        optionCanvas = GameObject.Find("OptionMenu").GetComponent<Canvas>();
        volumeCanvas = GameObject.Find("VolumeScreen").GetComponent<Canvas>();


        sfxSlider = GameObject.Find("VolumeScreen/sfxSlider").GetComponent<Slider>();
        bgSlider = GameObject.Find("VolumeScreen/bgSlider").GetComponent<Slider>();
        buttonSlider = GameObject.Find("VolumeScreen/buttonSlider").GetComponent<Slider>();

        sfxSlider.onValueChanged.AddListener(delegate { SfxSliderChange(); });
        bgSlider.onValueChanged.AddListener(delegate { BackgroundSliderChange(); });
        buttonSlider.onValueChanged.AddListener(delegate { ButtonSiderChange(); });

    }

	void Start () {
        if(instance != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

		optionCanvas.enabled = false;
		aboutCanvas.enabled = false;
        volumeCanvas.enabled = false;

        startText.text = "Start New Game";
        
        /*if (playerdata.pathogenType == Enums.CharacterType.None)
            startText.text = "Start New Game";
        else
            startText.text = "Resume Game";*/

        sfxSlider.value = playerdata.effectsVolume;
        bgSlider.value = playerdata.globalVolume;
        buttonSlider.value = playerdata.buttonAlpha;
    }

	/*******************************Start components******************************/
	public void ExitPress_Start(){
		Application.Quit ();
	}

	public void StartPress_Start(){
        if(startText.text == "Resume Game")
            Application.LoadLevel("MainMapOverview");
        else
            Application.LoadLevel("TroopSelection");
	}

	public void OptionPress_Start(){
        //disable components in canvas 
		optionCanvas.enabled = true;
        
	}

    /*******************************Option components******************************/
    public void BackPress_Opt(){
		optionCanvas.enabled = false;
	}

	public void ErasePress_Opt(){
        //bring out are you sure menu
	}

	public void VolPress_Opt(){
        optionCanvas.enabled = false;
        volumeCanvas.enabled = true;
	}

	public void AboutPress_Opt(){
		optionCanvas.enabled = false;
		aboutCanvas.enabled = true;
	}

	public void AboutBackPress(){
		aboutCanvas.enabled = false;
		optionCanvas.enabled = true;
	}

    /*******************************Volume components******************************/
    public void SfxSliderChange()
    {
        playerdata.effectsVolume = sfxSlider.value;
    }

    public void BackgroundSliderChange()
    {
        playerdata.globalVolume = bgSlider.value;
    }

    public void ButtonSiderChange()
    {
        playerdata.buttonAlpha = buttonSlider.value;
    }

    public void OnBackPress_Vol()
    {
        volumeCanvas.enabled = false;
        optionCanvas.enabled = true;
        AudioListener.volume = bgSlider.value;
    }
}
