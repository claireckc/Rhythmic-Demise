using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.Events;

public class TutorialManager_Map : MonoBehaviour
{

    Canvas tutorialCanvas;
    GameObject firstPanel, secondPanel;
    GameObject eventHandler;

    AudioSource selectClick, bgmUI, normalStage;

    void Start()
    {
        Debug.Log("DAMAGE: " + PlayerScript.playerdata.troopData[0].damage);
        tutorialCanvas = GameObject.Find("Tutorial Canvas").GetComponent<Canvas>();
        firstPanel = GameObject.Find("Tutorial Canvas/FirstPanel");
        secondPanel = GameObject.Find("Tutorial Canvas/SecondPanel");
        eventHandler = GameObject.Find("EventHandler");

        SetupAudio();
        if (PlayerScript.playerdata.firstMap)
        {
            //havent started playing

            eventHandler.SendMessage("BlockInteraction");

            firstPanel.SetActive(true);
            secondPanel.SetActive(false);
        }
        else
        {
            DestroyAll();
        }

        if (IsTutorialComplete())
        {
            //destroy all
            DestroyAll();
        }
    }

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
    
    void DestroyAll()
    {
        Destroy(tutorialCanvas.gameObject);
        Destroy(gameObject);

    }

    bool IsTutorialComplete()
    {
        bool complete = false;

        if (!PlayerScript.playerdata.firstTut3 && !PlayerScript.playerdata.firstResource && !PlayerScript.playerdata.firstMap)
            complete = true;

        return complete;
    }

    public void FirstPanel_Click()
    {
        PlaySelectAudio();
        secondPanel.SetActive(true);
        HidePanel(firstPanel);
    }

    public void SecondPanel_Click()
    {
        //activate mouth glowing
        PlaySelectAudio();
        eventHandler.SendMessage("SetAccess", "Mouth");
        HidePanel(secondPanel);
        PlayerScript.playerdata.firstMap = false;
        eventHandler.SendMessage("AllowInteraction");
    }

    void HidePanel(GameObject panel)
    {
        panel.SetActive(false);
        Destroy(panel.gameObject);
    }
}
