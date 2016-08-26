using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Collections;

public class SkillsManager : MonoBehaviour {

    Canvas skillCanvas, chooseCanvas;
    GameObject knightPanel, archerPanel, priestPanel;

    GameObject archerAOE, archerAttkBuff, archerHigh, knightCharge, knightDeffBuff, knightHigh, priestDisable, priestHeal, priestHealBuff;

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
    UnityEngine.UI.Button SkillBackButton;

    UnityEngine.UI.Button choose_SkillButton, choose_backButton;
    UnityEngine.UI.Button choose_KnightLeaderButton, choose_ArcherLeaderButton, choose_PriestLeaderButton;

    GameObject resourceManagement;
    
    // Use this for initialization
    void Start () {
        resourceManagement = GameObject.Find("UI Manager");
        GetChooseCanvasComponents();
        GetSkillCanvasComponents();
        DisableSkillCanvasComponents();
        skillCanvas.enabled = false;
    }
    void EnabledChooseCanvasComponents()
    {
        choose_KnightLeaderButton.interactable = choose_ArcherLeaderButton.interactable = choose_PriestLeaderButton.interactable = true;
        choose_SkillButton.interactable = choose_backButton.interactable = true;
    }

    void DisableChooseCanvasComponents()
    {
        choose_KnightLeaderButton.interactable = choose_ArcherLeaderButton.interactable = choose_PriestLeaderButton.interactable = false;
        choose_SkillButton.interactable = choose_backButton.interactable = false;
    }
    void GetChooseCanvasComponents()
    {
        chooseCanvas = GameObject.Find("ChooseCanvas").GetComponent<Canvas>();
        choose_SkillButton = chooseCanvas.transform.Find("SkillButton").GetComponent<UnityEngine.UI.Button>();
        choose_backButton = chooseCanvas.transform.Find("BackButton").GetComponent<UnityEngine.UI.Button>();

        choose_KnightLeaderButton = chooseCanvas.transform.Find("KnightPanel/KnightLeaderButton").GetComponent<UnityEngine.UI.Button>();
        choose_ArcherLeaderButton = chooseCanvas.transform.Find("ArcherPanel/ArcherLeaderButton").GetComponent<UnityEngine.UI.Button>();
        choose_PriestLeaderButton = chooseCanvas.transform.Find("PriestPanel/PriestLeaderButton").GetComponent<UnityEngine.UI.Button>();
    }
    
    void SetSkillIcon()
    {
        if(PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
        {
            archerAOE = Resources.Load("Prefabs/Icons/Cancer/ArcherAOE") as GameObject;
            archerAttkBuff = Resources.Load("Prefabs/Icons/Cancer/ArcherAttkBuff") as GameObject;
            archerHigh = Resources.Load("Prefabs/Icons/Cancer/ArcherHigh") as GameObject;
            knightCharge = Resources.Load("Prefabs/Icons/Cancer/KnightCharge") as GameObject;
            knightDeffBuff = Resources.Load("Prefabs/Icons/Cancer/KnightDeffBuff") as GameObject;
            knightHigh = Resources.Load("Prefabs/Icons/Cancer/KnightHigh") as GameObject;
            priestDisable = Resources.Load("Prefabs/Icons/Cancer/PriestDisable") as GameObject;
            priestHeal = Resources.Load("Prefabs/Icons/Cancer/PriestHeal") as GameObject;
            priestHealBuff = Resources.Load("Prefabs/Icons/Cancer/PriestHealBuff") as GameObject;

        }
        else
        {
            archerAOE = Resources.Load("Prefabs/Icons/Diabetic/ArcherAOE") as GameObject;
            archerAttkBuff = Resources.Load("Prefabs/Icons/Diabetic/ArcherAttkBuff") as GameObject;
            archerHigh = Resources.Load("Prefabs/Icons/Diabetic/ArcherHigh") as GameObject;
            knightCharge = Resources.Load("Prefabs/Icons/Diabetic/KnightCharge") as GameObject;
            knightDeffBuff = Resources.Load("Prefabs/Icons/Diabetic/KnightDeffBuff") as GameObject;
            knightHigh = Resources.Load("Prefabs/Icons/Diabetic/KnightHigh") as GameObject;
            priestDisable = Resources.Load("Prefabs/Icons/Diabetic/PriestDisable") as GameObject;
            priestHeal = Resources.Load("Prefabs/Icons/Diabetic/PriestHeal") as GameObject;
            priestHealBuff = Resources.Load("Prefabs/Icons/Diabetic/PriestHealBuff") as GameObject;
        }

        //knight skills
        knightPanel.transform.Find("Content Grid/Skill 1/Skill Icon").GetComponent<Image>().sprite = knightHigh.GetComponent<SpriteRenderer>().sprite;
        knightPanel.transform.Find("Content Grid/Skill 2/Skill Icon").GetComponent<Image>().sprite = knightCharge.GetComponent<SpriteRenderer>().sprite;
        knightPanel.transform.Find("Content Grid/Skill 3/Skill Icon").GetComponent<Image>().sprite = knightDeffBuff.GetComponent<SpriteRenderer>().sprite;

        //archer skills
        archerPanel.transform.Find("Content Grid/Skill 1/Skill Icon").GetComponent<Image>().sprite = archerAOE.GetComponent<SpriteRenderer>().sprite;
        archerPanel.transform.Find("Content Grid/Skill 2/Skill Icon").GetComponent<Image>().sprite = archerAttkBuff.GetComponent<SpriteRenderer>().sprite;
        archerPanel.transform.Find("Content Grid/Skill 3/Skill Icon").GetComponent<Image>().sprite = archerHigh.GetComponent<SpriteRenderer>().sprite;

        //priest skills
        priestPanel.transform.Find("Content Grid/Skill 1/Skill Icon").GetComponent<Image>().sprite = priestHeal.GetComponent<SpriteRenderer>().sprite;
        priestPanel.transform.Find("Content Grid/Skill 2/Skill Icon").GetComponent<Image>().sprite = priestDisable.GetComponent<SpriteRenderer>().sprite;
        priestPanel.transform.Find("Content Grid/Skill 3/Skill Icon").GetComponent<Image>().sprite = priestHealBuff.GetComponent<SpriteRenderer>().sprite;

    }
    void GetSkillCanvasComponents()
    {
        skillCanvas = GameObject.Find("LeaderSkillsCanvas").GetComponent<Canvas>();
        SkillBackButton = skillCanvas.transform.Find("BackButton").GetComponent<UnityEngine.UI.Button>();

        knightPanel = GameObject.Find("KnightSkillsPanel");

        archerPanel = GameObject.Find("ArcherSkillsPanel");

        priestPanel = GameObject.Find("PriestSkillsPanel");

        knightSel1 = knightPanel.transform.Find("Content Grid/Skill 1/Select Button").GetComponent<UnityEngine.UI.Button>();
        knightSel2 = knightPanel.transform.Find("Content Grid/Skill 2/Select Button").GetComponent<UnityEngine.UI.Button>();
        knightSel3 = knightPanel.transform.Find("Content Grid/Skill 3/Select Button").GetComponent<UnityEngine.UI.Button>();

        archerSel1 = archerPanel.transform.Find("Content Grid/Skill 1/Select Button").GetComponent<UnityEngine.UI.Button>();
        archerSel2 = archerPanel.transform.Find("Content Grid/Skill 2/Select Button").GetComponent<UnityEngine.UI.Button>();
        archerSel3 = archerPanel.transform.Find("Content Grid/Skill 3/Select Button").GetComponent<UnityEngine.UI.Button>();

        priestSel1 = priestPanel.transform.Find("Content Grid/Skill 1/Select Button").GetComponent<UnityEngine.UI.Button>();
        priestSel2 = priestPanel.transform.Find("Content Grid/Skill 2/Select Button").GetComponent<UnityEngine.UI.Button>();
        priestSel3 = priestPanel.transform.Find("Content Grid/Skill 3/Select Button").GetComponent<UnityEngine.UI.Button>();

        SetSkillIcon();

    }
    void DisableSkillCanvasComponents()
    {
        knightSel1.interactable = knightSel2.interactable = knightSel3.interactable = false;
        archerSel1.interactable = archerSel2.interactable = archerSel3.interactable = false;
        priestSel1.interactable = priestSel2.interactable = priestSel3.interactable = false;
        SkillBackButton.interactable = false;
    }

    void UpdateContent()
    {
        //check what is the selected leader type
        Enums.JobType leaderType = PlayerScript.playerdata.leaderType;
        switch (leaderType)
        {
            case Enums.JobType.Knight:
                archerSel1.interactable = archerSel2.interactable = archerSel3.interactable = false;
                priestSel1.interactable = priestSel2.interactable = priestSel3.interactable = false;
                knightSel1.interactable = knightSel2.interactable = knightSel3.interactable = true;;
                break;
            case Enums.JobType.Archer:
                knightSel1.interactable = knightSel2.interactable = knightSel3.interactable = false;
                priestSel1.interactable = priestSel2.interactable = priestSel3.interactable = false;
                archerSel1.interactable = archerSel2.interactable = archerSel3.interactable = true;;
                break;
            case Enums.JobType.Priest:
                archerSel1.interactable = archerSel2.interactable = archerSel3.interactable = false;
                knightSel1.interactable = knightSel2.interactable = knightSel3.interactable = false;
                priestSel1.interactable = priestSel2.interactable = priestSel3.interactable = true;
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
        SkillBackButton.interactable = true;
        DisableChooseCanvasComponents();
        UpdateContent();
    }

    public void Hide_BackPress()
    {
        DisableSkillCanvasComponents();
        EnabledChooseCanvasComponents();
        resourceManagement.SendMessage("UpdateLeaderButton", null);
        skillCanvas.enabled = false;

        if (PlayerScript.playerdata.firstResource)
            GameObject.Find("Tutorial Manager").SendMessage("DestroySkillTutorial");
    }

    public void KnightSel1_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.KnightHigh;
        resourceManagement.SendMessage("PrintMessage", "Bam-Bam! Selected");

        if (PlayerScript.playerdata.firstResource)
        {
            GameObject.Find("Tutorial Manager").SendMessage("ActivatePlayTutorial");
        }
    }

    public void KnightSel2_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.KnightDefbuff;
        resourceManagement.SendMessage("PrintMessage", "Stone Skin Selected");
    }

    public void KnightSel3_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.KnightCharge;
        resourceManagement.SendMessage("PrintMessage", "Whack-Em-All Selected");
    }

    public void ArcherSel1_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.ArcherAOE;
        resourceManagement.SendMessage("PrintMessage", "Arrow Rain! Selected");
    }

    public void ArcherSel2_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.ArcherAtkBuff;
        resourceManagement.SendMessage("PrintMessage", "Booster Selected");
    }

    public void ArcherSel3_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.ArcherHigh;
        resourceManagement.SendMessage("PrintMessage", "Snipe Selected");
    }

    public void PriestSel1_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.PriestHeal;
        resourceManagement.SendMessage("PrintMessage", "Virus Water Selected");
    }

    public void PriestSel2_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.PriestHex;
        resourceManagement.SendMessage("PrintMessage", "Levio-sa Selected");
    }

    public void PriestSel3_Press()
    {
        PlayerScript.playerdata.skillSelected = Enums.SkillName.PriestHealBuff;
        resourceManagement.SendMessage("PrintMessage", "Insurance Selected");
    }
    
}
