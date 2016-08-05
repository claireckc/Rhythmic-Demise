using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class SkillsManager : MonoBehaviour {

    Canvas skillCanvas, chooseCanvas;
    GameObject knightPanel, archerPanel, priestPanel;

    Image knightIcon1, knightIcon2, knightIcon3;
    Image archerIcon1, archerIcon2, archerIcon3;
    Image priestIcon1, priestIcon2, priestIcon3;

    Text knightSkillName1, knightSkillName2, knightSkillName3;
    Text archerSkillName1, archerSkillName2, archerSkillName3;
    Text priestSkillName1, priestSkillName2, priestSkillName3;

    Text knightSkillDesc1, knightSkillDesc2, knightSkillDesc3;
    Text archerSkillDesc1, archerSkillDesc2, archerSkillDesc3;
    Text priestSkillDesc1, priestSkillDesc2, priestSkillDesc3;

    UnityEngine.UI.Button knightSel1, knightSel2, knightSel3;
    UnityEngine.UI.Button archerSel1, archerSel2, archerSel3;
    UnityEngine.UI.Button priestSel1, priestSel2, priestSel3;
    
    // Use this for initialization
    void Start () {
        skillCanvas = GameObject.Find("LeaderSkillsCanvas").GetComponent<Canvas>();
        chooseCanvas = GameObject.Find("LeaderSkillsCanvas").GetComponent<Canvas>();

        knightPanel = GameObject.Find("KnightSkillsPanel");
        knightIcon1 = knightPanel.transform.Find("Content Grid/Skill 1/Skill Icon").GetComponent<Image>();
        knightIcon2 = knightPanel.transform.Find("Content Grid/Skill 2/Skill Icon").GetComponent<Image>();
        knightIcon3 = knightPanel.transform.Find("Content Grid/Skill 3/Skill Icon").GetComponent<Image>();

        archerPanel = GameObject.Find("ArcherSkillsPanel");
        archerIcon1 = archerPanel.transform.Find("Content Grid/Skill 1/Skill Icon").GetComponent<Image>();
        archerIcon2 = archerPanel.transform.Find("Content Grid/Skill 2/Skill Icon").GetComponent<Image>();
        archerIcon3 = archerPanel.transform.Find("Content Grid/Skill 3/Skill Icon").GetComponent<Image>();

        priestPanel = GameObject.Find("PriestSkillsPanel");
        priestIcon1 = priestPanel.transform.Find("Content Grid/Skill 1/Skill Icon").GetComponent<Image>();
        priestIcon2 = priestPanel.transform.Find("Content Grid/Skill 2/Skill Icon").GetComponent<Image>();
        priestIcon3 = priestPanel.transform.Find("Content Grid/Skill 3/Skill Icon").GetComponent<Image>();

        knightSkillName1 = knightPanel.transform.Find("Content Grid/Skill 1/Skill Name").GetComponent<Text>();
        knightSkillName2 = knightPanel.transform.Find("Content Grid/Skill 2/Skill Name").GetComponent<Text>();
        knightSkillName3 = knightPanel.transform.Find("Content Grid/Skill 3/Skill Name").GetComponent<Text>();

        knightSkillDesc1 = knightPanel.transform.Find("Content Grid/Skill 1/Skill Desc").GetComponent<Text>();
        knightSkillDesc2 = knightPanel.transform.Find("Content Grid/Skill 2/Skill Desc").GetComponent<Text>();
        knightSkillDesc3 = knightPanel.transform.Find("Content Grid/Skill 3/Skill Desc").GetComponent<Text>();

        archerSkillName1 = archerPanel.transform.Find("Content Grid/Skill 1/Skill Name").GetComponent<Text>();
        archerSkillName2 = archerPanel.transform.Find("Content Grid/Skill 2/Skill Name").GetComponent<Text>();
        archerSkillName3 = archerPanel.transform.Find("Content Grid/Skill 3/Skill Name").GetComponent<Text>();

        archerSkillDesc1 = archerPanel.transform.Find("Content Grid/Skill 1/Skill Desc").GetComponent<Text>();
        archerSkillDesc2 = archerPanel.transform.Find("Content Grid/Skill 2/Skill Desc").GetComponent<Text>();
        archerSkillDesc3 = archerPanel.transform.Find("Content Grid/Skill 3/Skill Desc").GetComponent<Text>();

        priestSkillName1 = priestPanel.transform.Find("Content Grid/Skill 1/Skill Name").GetComponent<Text>();
        priestSkillName2 = priestPanel.transform.Find("Content Grid/Skill 2/Skill Name").GetComponent<Text>();
        priestSkillName3 = priestPanel.transform.Find("Content Grid/Skill 3/Skill Name").GetComponent<Text>();

        priestSkillDesc1 = priestPanel.transform.Find("Content Grid/Skill 1/Skill Desc").GetComponent<Text>();
        priestSkillDesc2 = priestPanel.transform.Find("Content Grid/Skill 2/Skill Desc").GetComponent<Text>();
        priestSkillDesc3 = priestPanel.transform.Find("Content Grid/Skill 3/Skill Desc").GetComponent<Text>();

        knightSel1 = knightPanel.transform.Find("Content Grid/Skill 1/Select Button").GetComponent<UnityEngine.UI.Button>();
        knightSel2 = knightPanel.transform.Find("Content Grid/Skill 2/Select Button").GetComponent<UnityEngine.UI.Button>();
        knightSel3 = knightPanel.transform.Find("Content Grid/Skill 3/Select Button").GetComponent<UnityEngine.UI.Button>();

        archerSel1 = archerPanel.transform.Find("Content Grid/Skill 1/Select Button").GetComponent<UnityEngine.UI.Button>();
        archerSel2 = archerPanel.transform.Find("Content Grid/Skill 2/Select Button").GetComponent<UnityEngine.UI.Button>();
        archerSel3 = archerPanel.transform.Find("Content Grid/Skill 3/Select Button").GetComponent<UnityEngine.UI.Button>();

        priestSel1 = priestPanel.transform.Find("Content Grid/Skill 1/Select Button").GetComponent<UnityEngine.UI.Button>();
        priestSel2 = priestPanel.transform.Find("Content Grid/Skill 2/Select Button").GetComponent<UnityEngine.UI.Button>();
        priestSel3 = priestPanel.transform.Find("Content Grid/Skill 3/Select Button").GetComponent<UnityEngine.UI.Button>();
    }

    void UpdateContent()
    {
        //update the content of the desc, skill and icons
        knightSkillName1.text = archerSkillName1.text = priestSkillName1.text = "Skill Name 1";
        knightSkillName2.text = archerSkillName2.text = priestSkillName2.text = "Skill Name 2";
        knightSkillName3.text = archerSkillName3.text = priestSkillName3.text = "Skill Name 3";

        knightSkillDesc1.text = archerSkillDesc1.text = priestSkillDesc1.text = "Skill Description 1";
        knightSkillDesc2.text = archerSkillDesc2.text = priestSkillDesc2.text = "Skill Description 2";
        knightSkillDesc3.text = archerSkillDesc3.text = priestSkillDesc3.text = "Skill Description 3";

        //check what is the selected leader type
        Enums.JobType leaderType = PlayerScript.playerdata.leaderType;

        switch (leaderType)
        {
            case Enums.JobType.Knight:
                archerSel1.interactable = archerSel2.interactable = archerSel3.interactable = false;
                priestSel1.interactable = priestSel2.interactable = priestSel3.interactable = false;
                break;
            case Enums.JobType.Archer:
                knightSel1.interactable = knightSel2.interactable = knightSel3.interactable = false;
                priestSel1.interactable = priestSel2.interactable = priestSel3.interactable = false;
                break;
            case Enums.JobType.Priest:
                archerSel1.interactable = archerSel2.interactable = archerSel3.interactable = false;
                knightSel1.interactable = knightSel2.interactable = knightSel3.interactable = false;
                break;
            default:    //no leader selected, so all disabled
                knightSel1.interactable = knightSel2.interactable = knightSel3.interactable = false;
                archerSel1.interactable = archerSel2.interactable = archerSel3.interactable = false;
                priestSel1.interactable = priestSel2.interactable = priestSel3.interactable = false;
                break;

        }
    }
    void Show()
    {
        skillCanvas.enabled = true;
        UpdateContent();
    }

    void Hide_BackPress()
    {
        //disable all interactable in skill canvas
        knightSel1.interactable = knightSel2.interactable = knightSel3.interactable = false;
        archerSel1.interactable = archerSel2.interactable = archerSel3.interactable = false;
        priestSel1.interactable = priestSel2.interactable = priestSel3.interactable = false;
        //activate interactable in choose canvas

        skillCanvas.enabled = false;
    }

    public void KnightSel1_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.KnightHigh;
    }

    public void KnightSel2_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.KnightDefbuff;
    }

    public void KnightSel3_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.KnightCharge;
    }

    public void ArcherSel1_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.ArcherAOE;
    }

    public void ArcherSel2_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.ArcherAtkBuff;
    }

    public void ArcherSel3_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.ArcherHigh;
    }

    public void PriestSel1_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.PriestHealBuff;
    }

    public void PriestSel2_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.PriestHex;
    }

    public void PriestSel3_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.PriestCurse;
    }
}
