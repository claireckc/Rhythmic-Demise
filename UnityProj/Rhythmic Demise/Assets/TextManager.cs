using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using UnityEngine.UI;

public class TextManager : MonoBehaviour
{

    AudioSource gameAudio, tutorialAudio;

    public TextAsset tutorialText;
    string[] content;
    int currentLine, endLine;
    bool showText;
    GameObject textPanel;
    Text panelText;
    GameObject troopArrows, controlsArrows, resourceArrows, pauseArrows;
    GameObject tutManager;

    // Use this for initialization
    void Start()
    {
        if (PlayerScript.playerdata.firstTut1)
        {
            tutManager = GameObject.Find("Tutorial Manager");

            if (tutorialText != null)
                content = (tutorialText.text.Split('\n'));

            if (PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth && PlayerScript.playerdata.clickedStageNumber == 1)
            {
                currentLine = 0;
                endLine = 9;
            }

            textPanel = GameObject.Find("Tutorial Canvas/Panel");
            panelText = GameObject.Find("Tutorial Canvas/Panel/Text").GetComponent<Text>();

            troopArrows = GameObject.Find("Tutorial Canvas/TroopArrow");
            controlsArrows = GameObject.Find("Tutorial Canvas/ControlsArrows");
            resourceArrows = GameObject.Find("Tutorial Canvas/ResourceArrow");
            pauseArrows = GameObject.Find("Tutorial Canvas/Pause Arrow");

            gameAudio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
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

            textPanel.SetActive(false);

            troopArrows.SetActive(false);
            controlsArrows.SetActive(false);
            resourceArrows.SetActive(false);
            pauseArrows.SetActive(false);

        }
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

    public void PanelClick_1()
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
