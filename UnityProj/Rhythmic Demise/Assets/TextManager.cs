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
    AudioSource gameAudio, tutorialAudio;

    public TextAsset tutorialText;
    string[] content;
    int currentLine, endLine;
    bool showText;
    GameObject textPanel;
    Text panelText;
    GameObject troopArrows, controlsArrows, resourceArrows, pauseArrows;

    //for second tutorial scene
    GameObject explainPanel, enemyPanel, riskPanel, selectPanel;

    void Start()
    {
        tutManager = GameObject.Find("Tutorial Manager");
        tutorialCanvas = GameObject.Find("Tutorial Canvas").GetComponent<Canvas>();
        notePosition = GameObject.Find("Note Position");
        iconPosition = GameObject.Find("IconPosition");
        gameAudio = GameObject.Find("Audio Source").GetComponent<AudioSource>();

        if (PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth && PlayerScript.playerdata.clickedStageNumber == 1)
        {
            if (PlayerScript.playerdata.firstTut1)
            {
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
                
                tutorialAudio = GameObject.Find("Tutorial Audio").GetComponent<AudioSource>();

                textPanel.SetActive(true);
                panelText.text = content[currentLine];

                troopArrows.SetActive(false);
                controlsArrows.SetActive(false);
                resourceArrows.SetActive(false);
                pauseArrows.SetActive(false);

                gameAudio.Stop();
                tutorialAudio.Stop();
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

        if(PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth && PlayerScript.playerdata.clickedStageNumber == 2)
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
        if(currentLine != 0)
        {
            gameAudio.Stop();
            tutorialAudio.Play();
        }
    }

    void HidePanel()
    {
        textPanel.SetActive(false);
        Time.timeScale = 1f;

        if (tutorialAudio.isPlaying)
        {
            tutorialAudio.Stop();
            gameAudio.Play();
        }
    }

    //tutorial 2
    void ShowTutorial2Panel()
    {
        explainPanel.SetActive(true);
        //pause the game
        Time.timeScale = 0f;
        gameAudio.Pause();
    }

    void HideTutorial2Panel(GameObject panel, bool music)
    {
        panel.SetActive(false);
        Destroy(panel.gameObject);

        if (music)
        {
            if (!gameAudio.isPlaying)
                gameAudio.Play();
            Time.timeScale = 1f;
        }
        else
        {
            if (gameAudio.isPlaying)
                gameAudio.Pause();
        }

    }

    public void Tutorial2Explain_Click()
    {
        enemyPanel.SetActive(true);
        HideTutorial2Panel(explainPanel, false);
    }

    public void Tutorial2Enemy_Click()
    {
        riskPanel.SetActive(true);
        HideTutorial2Panel(enemyPanel, false);
    }

    public void Tutorial2Risk_Click()
    {
        selectPanel.SetActive(true);
        HideTutorial2Panel(riskPanel, false);
    }
    
    public void Tutorial2Top_Click()
    {
        tutManager.SendMessage("PlayMoveUp");
        HideTutorial2Panel(selectPanel, true);
    }

    public void Tutorial2Forward_Click()
    {
        tutManager.SendMessage("PlayMoveRight");
        HideTutorial2Panel(selectPanel, true);
    }

    public void Tutorial2Bottom_Click()
    {
        tutManager.SendMessage("PlayMoveDown");
        HideTutorial2Panel(selectPanel, true);
    }

    //tutorial 1
    public void Tutorial1Panel_Click()
    {
        if (PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth)
        {
            print(currentLine);
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
                        tutorialAudio.Play();
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
                        //call show first
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
            else
                showText = false;
        }
    }
}
