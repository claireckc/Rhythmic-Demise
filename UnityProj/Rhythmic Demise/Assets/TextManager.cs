using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{
    //general
    Canvas tutorialCanvas;
    GameObject notePosition, iconPosition;
    GameObject tutManager;

    //for first tutorial scene
    AudioSource gameAudio;

    public TextAsset tutorialText;
    string[] content;
    int currentLine, endLine;
    GameObject textPanel;
    Text panelText;
    GameObject troopArrows, controlsArrows, resourceArrows, pauseArrows;

    //for second tutorial scene
    GameObject explainPanel, enemyPanel, riskPanel, selectPanel;

    //for third tutorial scene
    GameObject introPanel, xplainPanel, bossPanel, closePanel;
    AudioSource selectClick;

    void Start()
    {
        tutManager = GameObject.Find("Tutorial Manager");
        tutorialCanvas = GameObject.Find("Tutorial Canvas").GetComponent<Canvas>();
        notePosition = GameObject.Find("Note Position");
        iconPosition = GameObject.Find("IconPosition");
        gameAudio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        selectClick = GameObject.Find("UI Music/Select").GetComponent<AudioSource>();

        if (PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth && PlayerScript.playerdata.clickedStageNumber == 1)
        {
            if (PlayerScript.playerdata.firstTut1)
            {
                gameAudio.Pause();
                if (tutorialText != null)
                    content = (tutorialText.text.Split('\n'));

                currentLine = 0;
                endLine = 9;

                textPanel = GameObject.Find("Tutorial Canvas/Panel");
                panelText = GameObject.Find("Tutorial Canvas/Panel/Text").GetComponent<Text>();

                troopArrows = GameObject.Find("Tutorial Canvas/TroopArrow");
                controlsArrows = GameObject.Find("Tutorial Canvas/ControlsArrows");
                resourceArrows = GameObject.Find("Tutorial Canvas/ResourceArrow");
                pauseArrows = GameObject.Find("Tutorial Canvas/Pause Arrow");

                textPanel.SetActive(true);
                panelText.text = content[currentLine];

                troopArrows.SetActive(false);
                controlsArrows.SetActive(false);
                resourceArrows.SetActive(false);
                pauseArrows.SetActive(false);

            }
            else
            {
                textPanel = GameObject.Find("Tutorial Canvas/Panel");
                panelText = GameObject.Find("Tutorial Canvas/Panel/Text").GetComponent<Text>();

                troopArrows = GameObject.Find("Tutorial Canvas/TroopArrow");
                controlsArrows = GameObject.Find("Tutorial Canvas/ControlsArrows");
                resourceArrows = GameObject.Find("Tutorial Canvas/ResourceArrow");
                pauseArrows = GameObject.Find("Tutorial Canvas/Pause Arrow");

                DestroyAll();
            }
        }
        else if (PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth && PlayerScript.playerdata.clickedStageNumber == 2)
        {
            explainPanel = GameObject.Find("Tutorial Canvas/Explain Panel");
            enemyPanel = GameObject.Find("Tutorial Canvas/Enemy Panel");
            riskPanel = GameObject.Find("Tutorial Canvas/Risk Panel");
            selectPanel = GameObject.Find("Tutorial Canvas/Select Panel");

            if (PlayerScript.playerdata.firstTut2)
            {
                explainPanel.SetActive(false);
                enemyPanel.SetActive(false);
                riskPanel.SetActive(false);
                selectPanel.SetActive(false);
            }
            else
                DestroyAll();
        }
        else if (PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth && PlayerScript.playerdata.clickedStageNumber == 3)
        {
            introPanel = GameObject.Find("Tutorial Canvas/IntroPanel");
            xplainPanel = GameObject.Find("Tutorial Canvas/ExplainPanel");
            bossPanel = GameObject.Find("Tutorial Canvas/BossPanel");
            closePanel = GameObject.Find("Tutorial Canvas/ClosePanel");

            if (PlayerScript.playerdata.firstTut3)
            {
                introPanel.SetActive(false);
                xplainPanel.SetActive(false);
                bossPanel.SetActive(false);
                closePanel.SetActive(false);
            }
            else
                DestroyAll();
        }
    }

    void PlaySelectAudio()
    {
        selectClick.Play();
    }

    void DestroyAll()
    {
        Destroy(tutorialCanvas.gameObject);
        Destroy(notePosition.gameObject);
        Destroy(iconPosition.gameObject);
        Destroy(tutManager.gameObject);
        Destroy(this.gameObject);
    }


    void ShowPanel()
    {
        panelText.text = content[currentLine];
        Time.timeScale = 0f;
        textPanel.SetActive(true);
        /*if(currentLine != 0)
        {
            gameAudio.Pause();
        }*/
    }

    void HidePanel()
    {
        textPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    /*************************************************Tutorial 3*************************************************/
    void ShowTutorial3Panel()
    {
        introPanel.SetActive(true);
        Time.timeScale = 0f;
        gameAudio.Pause();
        GameObject.Find("Game Music").SendMessage("PauseGameMusic");
    }

    void ShowClosePanel()
    {
        closePanel.SetActive(true);
        Time.timeScale = 0f;
        gameAudio.Pause();
        GameObject.Find("Game Music").SendMessage("PauseGameMusic");
    }

    public void IntroPanel_Click()
    {
        PlaySelectAudio();
        xplainPanel.SetActive(true);
        HideTutorialPanel(introPanel, false);
    }

    public void ExplainPanel_Click()
    {
        PlaySelectAudio();
        bossPanel.SetActive(true);
        HideTutorialPanel(xplainPanel, false);
    }

    public void BossPanel_Click()
    {
        PlaySelectAudio();
        HideTutorialPanel(bossPanel, true);
    }

    public void ClosePanel_Click()
    {
        PlaySelectAudio();
        HideTutorialPanel(closePanel, true);
        tutManager.SendMessage("PlaySkill");
        TutorialManager.TutManager.final = true;
    }

    /*************************************************Tutorial 2*************************************************/
    void ShowTutorial2Panel()
    {
        explainPanel.SetActive(true);
        //pause the game
        Time.timeScale = 0f;
        gameAudio.Pause();
        GameObject.Find("Game Music").SendMessage("PauseGameMusic");
    }

    void HideTutorialPanel(GameObject panel, bool music)
    {
        panel.SetActive(false);
        Destroy(panel.gameObject);

        if (music)
        {
            if (!gameAudio.isPlaying)
            {
                gameAudio.Play();
                GameObject.Find("Game Music").SendMessage("UnPauseGameMusic");
            }
            Time.timeScale = 1f;
        }
        else
        {
            if (gameAudio.isPlaying)
            {
                gameAudio.Pause();
                GameObject.Find("Game Music").SendMessage("PauseGameMusic");

            }
        }

    }

    public void Tutorial2Explain_Click()
    {
        PlaySelectAudio();
        enemyPanel.SetActive(true);
        HideTutorialPanel(explainPanel, false);
    }

    public void Tutorial2Enemy_Click()
    {
        PlaySelectAudio();
        riskPanel.SetActive(true);
        HideTutorialPanel(enemyPanel, false);
    }

    public void Tutorial2Risk_Click()
    {
        PlaySelectAudio();
        selectPanel.SetActive(true);
        HideTutorialPanel(riskPanel, false);
    }

    public void Tutorial2Top_Click()
    {
        PlaySelectAudio();
        tutManager.SendMessage("PlayMoveUp");
        HideTutorialPanel(selectPanel, true);
    }

    public void Tutorial2Forward_Click()
    {
        PlaySelectAudio();
        tutManager.SendMessage("PlayMoveRight");
        HideTutorialPanel(selectPanel, true);
    }

    public void Tutorial2Bottom_Click()
    {
        PlaySelectAudio();
        tutManager.SendMessage("PlayMoveDown");
        HideTutorialPanel(selectPanel, true);
    }

    /*************************************************Tutorial 1*************************************************/
    public void Tutorial1Panel_Click()
    {
        if (PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth)
        {
            PlaySelectAudio();
            //maximum line number is 9
            if (currentLine <= endLine)
            {
                switch (currentLine)
                {
                    case 0:
                        currentLine++;  //start saying hi, move to next line(introduce troop)
                        troopArrows.SetActive(true);
                        break;
                    case 1:
                        //move on to line 2(introduce controls)
                        currentLine++;
                        troopArrows.SetActive(false);
                        controlsArrows.SetActive(true);
                        break;
                    case 2:
                        //move on to line 3(introduce resources)
                        currentLine++;
                        controlsArrows.SetActive(false);
                        resourceArrows.SetActive(true);
                        break;
                    case 3:
                        //to line 4(introduce pause)
                        currentLine++;
                        resourceArrows.SetActive(false);
                        pauseArrows.SetActive(true);
                        break;
                    case 4:
                        //to line 5(introduce beat)
                        currentLine++;
                        pauseArrows.SetActive(false);
                        gameAudio.Play();
                        GameObject.Find("Game Music").SendMessage("PlayGameMusic");
                        break;
                    case 5:
                        //to line 6(introduce forward control)
                        currentLine++;
                        break;
                    case 6: //at line 7
                        HidePanel();
                        tutManager.SendMessage("PlayMoveRight");
                        currentLine++;
                        break;
                    case 7:
                        //at line 8(introduce attack)
                        HidePanel();
                        tutManager.SendMessage("PlayAttack");
                        currentLine++;
                        break;
                    case 8:
                        HidePanel();
                        tutManager.SendMessage("PlayMoveRight");
                        currentLine++;
                        break;
                    case 9:
                        HidePanel();
                        currentLine++;
                        break;
                }

                if (textPanel.active)
                    panelText.text = content[currentLine];
            }
        }
    }
}
