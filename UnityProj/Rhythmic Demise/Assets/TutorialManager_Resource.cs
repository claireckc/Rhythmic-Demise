using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class TutorialManager_Resource : MonoBehaviour
{

    Canvas chooseCanvas, skillsCanvas, mainCanvas;

    //Tutorial panels for main
    GameObject introPanel, slotPanel, plusPanel, slot2Panel, playArrow;
    UnityEngine.UI.Button slot1_button, introPanel_button, play_button;

    //tutorial panels for choose canvas
    GameObject chooseIntroPanel, knightPanel, leaderPanel, skillPanel, chooseBackButtonArrow;
    UnityEngine.UI.Button knightPreview_button, knightLeader_button, skill_button, chooseBackButton;

    //for skill canvas
    GameObject skillIntroPanel, skillChoosePanel, backButtonArrow;
    UnityEngine.UI.Button firstKnightSkill_button, backButton;

    AudioSource selectClick, bgmUI;

    void SetupAudio()
    {
        selectClick = GameObject.Find("UI Music/Select").GetComponent<AudioSource>();
        bgmUI = GameObject.Find("UI Music/BGM").GetComponent<AudioSource>();

        selectClick.volume = PlayerScript.playerdata.effectsVolume;
        bgmUI.volume = PlayerScript.playerdata.globalVolume;

        if (!bgmUI.isPlaying)
            bgmUI.Play();
    }

    void PlaySelectAudio()
    {
        selectClick.Play();
    }

    void Start()
    {

        mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        chooseCanvas = GameObject.Find("ChooseCanvas").GetComponent<Canvas>();
        skillsCanvas = GameObject.Find("LeaderSkillsCanvas").GetComponent<Canvas>();

        InitMainTutorial();
        InitSkillTutorial();
        InitChooseTutorial();

        SetupAudio();

        if (PlayerScript.playerdata.firstResource)
        {
            StartMainTutorial();
            StartSkillTutorial();
            StartChooseTutorial();
        }
        else
        {
            DestroyMainTutorial();
            DestroyChooseTutorial();
            DestroySkillTut();
            Destroy(gameObject);
        }

    }

    void InitChooseTutorial()
    {
        chooseIntroPanel = GameObject.Find("ChooseCanvas/Tutorial/ChooseIntroPanel");
        knightPanel = GameObject.Find("ChooseCanvas/Tutorial/ChooseKnightPanel");
        leaderPanel = GameObject.Find("ChooseCanvas/Tutorial/LeaderPanel");
        skillPanel = GameObject.Find("ChooseCanvas/Tutorial/SkillPanel");

        knightPreview_button = GameObject.Find("ChooseCanvas/KnightPanel/Preview").GetComponent<UnityEngine.UI.Button>();
        knightLeader_button = GameObject.Find("ChooseCanvas/KnightPanel/KnightLeaderButton").GetComponent<UnityEngine.UI.Button>();
        skill_button = GameObject.Find("ChooseCanvas/SkillButton").GetComponent<UnityEngine.UI.Button>();
        chooseBackButton = GameObject.Find("ChooseCanvas/BackButton").GetComponent<UnityEngine.UI.Button>();
        chooseBackButtonArrow = GameObject.Find("ChooseCanvas/Tutorial/BackArrow");

    }

    void StartChooseTutorial()
    {
        chooseIntroPanel.SetActive(false);
        knightPanel.SetActive(false);
        leaderPanel.SetActive(false);
        skillPanel.SetActive(false);
        chooseBackButtonArrow.SetActive(false);
    }

    void DestroyChooseTutorial()
    {
        Destroy(chooseIntroPanel.gameObject);
        Destroy(knightPanel.gameObject);
        Destroy(leaderPanel.gameObject);
        Destroy(skillPanel.gameObject);

        Destroy(chooseBackButtonArrow.gameObject);
    }

    void InitSkillTutorial()
    {
        skillIntroPanel = GameObject.Find("LeaderSkillsCanvas/Tutorial/IntroPanel");
        skillChoosePanel = GameObject.Find("LeaderSkillsCanvas/Tutorial/SkillChoosePanel");
        firstKnightSkill_button = GameObject.Find("LeaderSkillsCanvas/KnightSkillsPanel/Content Grid/Skill 1/Select Button").GetComponent<UnityEngine.UI.Button>();
        backButton = GameObject.Find("LeaderSkillsCanvas/BackButton").GetComponent<UnityEngine.UI.Button>();

        backButtonArrow = GameObject.Find("LeaderSkillsCanvas/Tutorial/BackButton Arrow");

    }

    void StartSkillTutorial()
    {
        skillIntroPanel.SetActive(false);
        skillChoosePanel.SetActive(false);
        backButtonArrow.SetActive(false);
    }

    void DestroySkillTut()
    {
        Destroy(skillIntroPanel.gameObject);
        Destroy(skillChoosePanel.gameObject);
        Destroy(backButtonArrow.gameObject);
    }

    void InitMainTutorial()
    {
        introPanel = GameObject.Find("MainCanvas/Tutorial/IntroPanel");
        slotPanel = GameObject.Find("MainCanvas/Tutorial/SlotClickPanel");
        plusPanel = GameObject.Find("MainCanvas/Tutorial/PlusMinusPanel");
        slot2Panel = GameObject.Find("MainCanvas/Tutorial/SlotClickPanel2");
        playArrow = GameObject.Find("MainCanvas/Tutorial/PlayArrow");


        //enable buttons
        slot1_button = mainCanvas.transform.Find("Slot1").GetComponent<UnityEngine.UI.Button>();
        introPanel_button = mainCanvas.transform.Find("Tutorial/IntroPanel").GetComponent<UnityEngine.UI.Button>();
        play_button = mainCanvas.transform.Find("PlayButton").GetComponent<UnityEngine.UI.Button>();
    }

    void StartMainTutorial()
    {
        DisableAllInteractions(mainCanvas);
        introPanel.SetActive(true);
        slotPanel.SetActive(false);
        plusPanel.SetActive(false);
        slot2Panel.SetActive(false);
        playArrow.SetActive(false);
        introPanel.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    void DestroyMainTutorial()
    {
        Destroy(introPanel.gameObject);
        Destroy(slotPanel.gameObject);
        Destroy(plusPanel.gameObject);
        Destroy(slot2Panel.gameObject);
        Destroy(playArrow.gameObject);
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

    void DestroyAll()
    {
        Destroy(introPanel.gameObject);
        Destroy(slotPanel.gameObject);
    }

    void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
        Destroy(panel.gameObject);
    }

    public void IntroPanel_Click()
    {
        PlaySelectAudio();
        slotPanel.SetActive(true);
        HidePanel(introPanel);
        slot1_button.interactable = true;
    }

    void ActivateChooseTutorial()
    {
        //destroy 
        Destroy(slotPanel.gameObject);
        chooseIntroPanel.SetActive(true);
        DisableAllInteractions(chooseCanvas);
        chooseIntroPanel.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    public void ChooseIntro_Click()
    {
        PlaySelectAudio();
        knightPanel.SetActive(true);
        HidePanel(chooseIntroPanel);
        DisableAllInteractions(chooseCanvas);
        knightPreview_button.interactable = true;
    }

    void ActivateLeaderTutorial()
    {
        Destroy(knightPanel);
        DisableAllInteractions(mainCanvas);
        plusPanel.SetActive(true);
        plusPanel.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    public void PlusPanelClick()
    {
        PlaySelectAudio();
        slot2Panel.SetActive(true);
        DisableAllInteractions(mainCanvas);
        slot1_button.interactable = true;
        HidePanel(plusPanel);
    }

    void ActivateSkillTutorial()
    {
        HidePanel(slot2Panel);
        leaderPanel.SetActive(true);
        DisableAllInteractions(chooseCanvas);
        knightLeader_button.interactable = true;
    }

    void ActivateSkillPanel()
    {
        HidePanel(leaderPanel);
        skillPanel.SetActive(true);
        DisableAllInteractions(chooseCanvas);
        skill_button.interactable = true;
    }

    void ActivateFirstSkillTutorial()
    {
        Destroy(skillPanel);
        DisableAllInteractions(skillsCanvas);
        skillIntroPanel.SetActive(true);
        skillIntroPanel.GetComponent<UnityEngine.UI.Button>().interactable = true;
    }

    void ActivatePlayTutorial()
    {
        Destroy(skillChoosePanel);
        DisableAllInteractions(skillsCanvas);
        backButtonArrow.SetActive(true);
        backButton.interactable = true;
    }

    void DestroySkillTutorial()
    {
        DisableAllInteractions(chooseCanvas);
        chooseBackButtonArrow.SetActive(true);
        chooseBackButton.interactable = true;
    }

    void ActivateFinalTutorial()
    {
        DisableAllInteractions(mainCanvas);
        playArrow.SetActive(true);
        play_button.interactable = true;
    }

    public void SkillIntroPanel_Click()
    {
        PlaySelectAudio();
        skillChoosePanel.SetActive(true);
        HidePanel(skillIntroPanel);
        firstKnightSkill_button.interactable = true;
    }

    void DisableChoose()
    {
        DisableAllInteractions(chooseCanvas);
        chooseBackButton.interactable = true;
    }
}
