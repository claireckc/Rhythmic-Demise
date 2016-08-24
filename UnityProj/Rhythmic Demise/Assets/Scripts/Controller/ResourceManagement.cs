using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System.Collections;

public class ResourceManagement : MonoBehaviour
{
    bool firstTutorial;
    public Sprite cancerKnightSprite, cancerArcherSprite, cancerPriestSprite;
    public Sprite diabeticKnightSprite, diabeticArcherSprite, diabeticPriestSprite;
    public Image slot1, slot2, slot3;
    private Text noneText1, noneText2, noneText3;
    public Canvas chooseCanvas, mainCanvas;
    public Text resourceText, energyText;
    public Text resourceLabel;
    public Text countText1, countText2, countText3;
    public UnityEngine.UI.Button playButton;
    private int slotClicked;
    public Text messageText;
    public Animator textAnim;
    GameObject skillManager;

    private Sprite originalSprite1, originalSprite2, originalSprite3;

    /*Choose canvas component*/
    public Text knightLevel, knightAttack, knightDefense, archerLevel, archerAttack, archerDefense, priestLevel, priestAttack, priestDefense;
    public Text knightResource, archerResource, priestResource;
    public UnityEngine.UI.Button knightLeader, archerLeader, priestLeader;

    public Image chooseKnightSprite, chooseArcherSprite, choosePriestSprite;

    GameObject resourceTutorial;

    void Start()
    {
        firstTutorial = true;
        slotClicked = 0;
        StartMain();
        StartChoose();

        InitMain();
        InitChoose();
        chooseCanvas.enabled = false;

        skillManager = GameObject.Find("SkillsManager");

        if (PlayerScript.playerdata.firstResource)
            resourceTutorial = GameObject.Find("Tutorial Manager");
    }

    public void StartMain()
    {

        mainCanvas = mainCanvas.GetComponent<Canvas>();
        slot1 = slot1.GetComponent<Image>();
        slot2 = slot2.GetComponent<Image>();
        slot3 = slot3.GetComponent<Image>();

        noneText1 = slot1.GetComponentInChildren<Text>();
        noneText2 = slot2.GetComponentInChildren<Text>();
        noneText3 = slot3.GetComponentInChildren<Text>();

        resourceLabel = resourceLabel.GetComponent<Text>();

        resourceText = resourceText.GetComponent<Text>();
        energyText = energyText.GetComponent<Text>();

        chooseCanvas = chooseCanvas.GetComponent<Canvas>();

        countText1 = countText1.GetComponent<Text>();
        countText2 = countText2.GetComponent<Text>();
        countText3 = countText3.GetComponent<Text>();

        playButton = playButton.GetComponent<UnityEngine.UI.Button>();

        originalSprite1 = slot1.sprite;
        originalSprite2 = slot2.sprite;
        originalSprite3 = slot3.sprite;

        messageText = messageText.GetComponent<Text>();
        textAnim = textAnim.GetComponent<Animator>();
    }

    public void InitMain()
    {
        //check with playerdata and initialize accordingly
        if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
            resourceLabel.text = "Carbon:";
        else
            resourceLabel.text = "Sugar:";

        resourceText.text = PlayerScript.playerdata.totalResource.ToString();
        //DEBUG
        PlayerScript.playerdata.totalEnergy = 10;
        //
        energyText.text = PlayerScript.playerdata.totalEnergy.ToString();

        for (int i = 0; i < PlayerScript.playerdata.troopSelected.Count; i++)
        {
            if (PlayerScript.playerdata.troopSelected[i].troop.job != Enums.JobType.None)
            {
                if (i == 0)
                    noneText1.enabled = false;
                else if (i == 1)
                    noneText2.enabled = false;
                else
                    noneText3.enabled = false;

                switch (PlayerScript.playerdata.troopSelected[i].troop.job)
                {
                    case Enums.JobType.Knight:
                        if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
                        {
                            if (i == 0)
                                slot1.sprite = cancerKnightSprite;
                            else if (i == 1)
                                slot2.sprite = cancerKnightSprite;
                            else
                                slot3.sprite = cancerKnightSprite;
                        }
                        else
                        {
                            if (i == 0)
                                slot1.sprite = diabeticKnightSprite;
                            else if (i == 1)
                                slot2.sprite = diabeticKnightSprite;
                            else
                                slot3.sprite = diabeticKnightSprite;
                        }
                        break;
                    case Enums.JobType.Archer:
                        if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
                        {
                            if (i == 0)
                                slot1.sprite = cancerArcherSprite;
                            else if (i == 1)
                                slot2.sprite = cancerArcherSprite;
                            else
                                slot3.sprite = cancerArcherSprite;
                        }
                        else
                        {
                            if (i == 0)
                                slot1.sprite = diabeticArcherSprite;
                            else if (i == 1)
                                slot2.sprite = diabeticArcherSprite;
                            else
                                slot3.sprite = diabeticArcherSprite;

                        }
                        break;
                    case Enums.JobType.Priest:
                        if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
                        {
                            if (i == 0)
                                slot1.sprite = cancerPriestSprite;
                            else if (i == 1)
                                slot2.sprite = cancerPriestSprite;
                            else
                                slot3.sprite = cancerPriestSprite;

                        }
                        else
                        {
                            if (i == 0)
                                slot1.sprite = diabeticPriestSprite;
                            else if (i == 1)
                                slot2.sprite = diabeticPriestSprite;
                            else
                                slot3.sprite = diabeticPriestSprite;
                        }
                        break;
                }
                if (i == 0)
                    countText1.text = PlayerScript.playerdata.troopSelected[i].count.ToString();
                else if (i == 1)
                    countText2.text = PlayerScript.playerdata.troopSelected[i].count.ToString();
                else if (i == 2)
                    countText3.text = PlayerScript.playerdata.troopSelected[i].count.ToString();
            }
        }

        int charCount = 0;
        for (int i = 0; i < PlayerScript.playerdata.troopSelected.Count; i++)
        {
            if (PlayerScript.playerdata.troopSelected[i].troop.job != Enums.JobType.None)
                charCount++;
        }

        if (charCount == 0)
            playButton.interactable = false;
        else
            playButton.interactable = true;
    }

    public bool HasLeader()
    {
        if (PlayerScript.playerdata.leaderType != Enums.JobType.None)
            return true;
        return false;
    }

    public bool HasTeam()
    {
        for (int i = 0; i < PlayerScript.playerdata.troopSelected.Count; i++)
        {
            if (PlayerScript.playerdata.troopSelected[i].troop.job != Enums.JobType.None)
                return true;
        }

        return false;
    }

    public void Main_PlayPress()
    {
        if (HasTeam() && HasLeader())
        {
            switch (PlayerScript.playerdata.clickedMap)
            {
                case Enums.MainMap.Mouth:
                    switch (PlayerScript.playerdata.clickedStageNumber)
                    {
                        case 1:
                            if (PlayerScript.playerdata.firstResource)
                            {
                                PlayerScript.playerdata.firstResource = false;
                                SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
                            }

                            Application.LoadLevel("TutorialScene");
                            break;
                        case 2:
                            Application.LoadLevel("Tutorial2Scene");
                            break;
                        case 3:
                            Application.LoadLevel("Tutorial3Scene");
                            break;
                    }
                    break;
                case Enums.MainMap.Larnyx:
                    switch (PlayerScript.playerdata.clickedStageNumber)
                    {
                        case 1:
                            Application.LoadLevel("LarnyxMap1");
                            break;
                        case 2:
                            Application.LoadLevel("LarnyxBossMap");
                            break;
                    }
                    break;
                case Enums.MainMap.Trachea:
                    switch (PlayerScript.playerdata.clickedStageNumber)
                    {
                        case 1:
                            Application.LoadLevel("TracheaMap1");
                            break;
                        case 2:
                            Application.LoadLevel("TracheaMap2");
                            break;
                        case 3:
                            Application.LoadLevel("TracheaEventMap");
                            break;
                    }
                    break;
                case Enums.MainMap.Lung:
                    switch (PlayerScript.playerdata.clickedStageNumber)
                    {
                        case 1:
                            Application.LoadLevel("LungMap1");
                            break;
                        case 2:
                            Application.LoadLevel("LungMap2");
                            break;
                        case 3:
                            Application.LoadLevel("LungBossMap");
                            break;
                    }
                    break;
                case Enums.MainMap.Heart:
                    switch (PlayerScript.playerdata.clickedStageNumber)
                    {
                        case 1:
                            Application.LoadLevel("HeartMap1");
                            break;
                        case 2:
                            Application.LoadLevel("HeartMap2");
                            break;
                        case 3:
                            Application.LoadLevel("HeartBossMap");
                            break;
                    }
                    break;
                case Enums.MainMap.Liver:
                    switch (PlayerScript.playerdata.clickedStageNumber)
                    {
                        case 1:
                            Application.LoadLevel("LiverMap1");
                            break;
                        case 2:
                            Application.LoadLevel("LiverEventMap");
                            break;
                    }
                    break;
                case Enums.MainMap.Spleen:
                    switch (PlayerScript.playerdata.clickedStageNumber)
                    {
                        case 1:
                            Application.LoadLevel("SpleenMap1");
                            break;
                        case 2:
                            Application.LoadLevel("SpleenMap2");
                            break;
                        case 3:
                            Application.LoadLevel("SpleenBossMap");
                            break;
                    }
                    break;
                case Enums.MainMap.Pancreas:
                    switch (PlayerScript.playerdata.clickedStageNumber)
                    {
                        case 1:
                            Application.LoadLevel("PancreasMap1");
                            break;
                        case 2:
                            Application.LoadLevel("PancreasMap2");
                            break;
                        case 3:
                            Application.LoadLevel("PancreasBossMap");
                            break;
                    }
                    break;
                case Enums.MainMap.Kidney:
                    switch (PlayerScript.playerdata.clickedStageNumber)
                    {
                        case 1:
                            Application.LoadLevel("KidneyMap1");
                            break;
                        case 2:
                            Application.LoadLevel("KidneyMap2");
                            break;
                        case 3:
                            Application.LoadLevel("KidneyBossMap");
                            break;
                    }
                    break;
                case Enums.MainMap.SIntes:
                    switch (PlayerScript.playerdata.clickedStageNumber)
                    {
                        case 1:
                            Application.LoadLevel("SmallIntestineMap1");
                            break;
                        case 2:
                            Application.LoadLevel("SmallIntestineEventMap");
                            break;
                    }
                    break;
                case Enums.MainMap.LIntes:
                    switch (PlayerScript.playerdata.clickedStageNumber)
                    {
                        case 1:
                            Application.LoadLevel("LargeIntestineMap1");
                            break;
                        case 2:
                            Application.LoadLevel("LargeIntestineMap2");
                            break;
                        case 3:
                            Application.LoadLevel("LargeIntestineBossMap");
                            break;
                    }
                    break;
                case Enums.MainMap.Brain:
                    switch (PlayerScript.playerdata.clickedStageNumber)
                    {
                        case 1:
                            Application.LoadLevel("BrainMap1");
                            break;
                        case 2:
                            Application.LoadLevel("BrainMap2");
                            break;
                        case 3:
                            Application.LoadLevel("BrainBossMap");
                            break;
                    }
                    break;
                default:
                    Debug.Log("Stage Enter Error");
                    break;
            }
        }
        else if (!HasTeam())
        {
            //print out a messsage to tell them to select a unit first
            PrintMessage("Select units first.");
        }
        else if (!HasLeader())
        {
            PrintMessage("Select your leader first.");
        }
    }

    public void PrintMessage(string msg)
    {
        messageText.text = msg;
        textAnim.Play("ResourceTextAnimation", -1, 0f);

    }

    public void Slot1_Click()
    {
        chooseCanvas.enabled = true;
        playButton.interactable = false;
        slotClicked = 1;

        if (PlayerScript.playerdata.firstResource)
        {
            if (firstTutorial)
                resourceTutorial.SendMessage("ActivateChooseTutorial");
            else
                resourceTutorial.SendMessage("ActivateSkillTutorial");
        }
    }

    public void Slot2_Click()
    {
        chooseCanvas.enabled = true;
        playButton.interactable = false;
        slotClicked = 2;
    }

    public void Slot3_Click()
    {
        chooseCanvas.enabled = true;
        playButton.interactable = false;
        slotClicked = 3;
    }

    public void Slot1_Plus()
    {
        if (PlayerScript.playerdata.troopSelected[0].troop.job != Enums.JobType.None)
        {
            if (PlayerScript.playerdata.totalEnergy >= PlayerScript.playerdata.troopSelected[0].troop.energyNeeded && PlayerScript.playerdata.totalEnergy != 0)
            {
                //allow summoning
                PlayerScript.playerdata.totalEnergy -= PlayerScript.playerdata.troopSelected[0].troop.energyNeeded;
                PlayerScript.playerdata.troopSelected[0].count++;
            }

            UpdateSelectedSlot(1);
        }
    }

    public void Slot2_Plus()
    {
        if (PlayerScript.playerdata.troopSelected[1].troop.job != Enums.JobType.None)
        {
            if (PlayerScript.playerdata.totalEnergy >= PlayerScript.playerdata.troopSelected[1].troop.energyNeeded && PlayerScript.playerdata.totalEnergy != 0)
            {
                //allow summoning
                PlayerScript.playerdata.totalEnergy -= PlayerScript.playerdata.troopSelected[1].troop.energyNeeded;
                PlayerScript.playerdata.troopSelected[1].count++;
            }

            UpdateSelectedSlot(2);
        }
    }

    public void Slot3_Plus()
    {
        if (PlayerScript.playerdata.troopSelected[2].troop.job != Enums.JobType.None)
        {
            if (PlayerScript.playerdata.totalEnergy >= PlayerScript.playerdata.troopSelected[2].troop.energyNeeded && PlayerScript.playerdata.totalEnergy != 0)
            {
                //allow summoning
                PlayerScript.playerdata.totalEnergy -= PlayerScript.playerdata.troopSelected[2].troop.energyNeeded;
                PlayerScript.playerdata.troopSelected[2].count++;
            }

            UpdateSelectedSlot(3);
        }
    }

    public void Slot1_Minus()
    {
        if (PlayerScript.playerdata.troopSelected[0].troop.job != Enums.JobType.None && PlayerScript.playerdata.troopSelected[0].count > 0)
        {
            PlayerScript.playerdata.totalEnergy += PlayerScript.playerdata.troopSelected[0].troop.energyNeeded;
            PlayerScript.playerdata.troopSelected[0].count--;

            if (PlayerScript.playerdata.troopSelected[0].count == 0)
            {
                PlayerScript.playerdata.troopSelected[0].troop = new Troop();
                slot1.sprite = originalSprite1;
                noneText1.enabled = true;
            }

            UpdateSelectedSlot(1);
        }
    }

    public void Slot2_Minus()
    {
        if (PlayerScript.playerdata.troopSelected[1].troop.job != Enums.JobType.None && PlayerScript.playerdata.troopSelected[1].count > 0)
        {
            PlayerScript.playerdata.totalEnergy += PlayerScript.playerdata.troopSelected[1].troop.energyNeeded;
            PlayerScript.playerdata.troopSelected[1].count--;

            if (PlayerScript.playerdata.troopSelected[1].count == 0)
            {
                PlayerScript.playerdata.troopSelected[1].troop = new Troop();
                slot2.sprite = originalSprite2;
                noneText2.enabled = true;
            }
            UpdateSelectedSlot(2);
        }
    }

    public void Slot3_Minus()
    {
        if (PlayerScript.playerdata.troopSelected[2].troop.job != Enums.JobType.None && PlayerScript.playerdata.troopSelected[2].count > 0)
        {
            PlayerScript.playerdata.totalEnergy += PlayerScript.playerdata.troopSelected[2].troop.energyNeeded;
            PlayerScript.playerdata.troopSelected[2].count--;

            if (PlayerScript.playerdata.troopSelected[2].count == 0)
            {
                PlayerScript.playerdata.troopSelected[2].troop = new Troop();
                slot3.sprite = originalSprite3;
                noneText3.enabled = true;
            }
            UpdateSelectedSlot(3);
        }
    }

    public void Management_BackPress()
    {
        Application.LoadLevel("MouthStage");
    }

    public void UpdateSelectedSlot(int SlotNumber)
    {
        energyText.text = PlayerScript.playerdata.totalEnergy.ToString();

        if (SlotNumber == 1)
        {
            if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
            {
                switch (PlayerScript.playerdata.troopSelected[0].troop.job)
                {
                    case Enums.JobType.Knight:
                        slot1.sprite = cancerKnightSprite;
                        break;
                    case Enums.JobType.Archer:
                        slot1.sprite = cancerArcherSprite;
                        break;
                    case Enums.JobType.Priest:
                        slot1.sprite = cancerPriestSprite;
                        break;
                }
            }
            else
            {
                switch (PlayerScript.playerdata.troopSelected[0].troop.job)
                {
                    case Enums.JobType.Knight:
                        slot1.sprite = diabeticKnightSprite;
                        break;
                    case Enums.JobType.Archer:
                        slot1.sprite = diabeticArcherSprite;
                        break;
                    case Enums.JobType.Priest:
                        slot1.sprite = diabeticPriestSprite;
                        break;
                }
            }
            countText1.text = PlayerScript.playerdata.troopSelected[0].count.ToString();
        }
        else if (SlotNumber == 2)
        {
            if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
            {
                switch (PlayerScript.playerdata.troopSelected[1].troop.job)
                {
                    case Enums.JobType.Knight:
                        slot2.sprite = cancerKnightSprite;
                        break;
                    case Enums.JobType.Archer:
                        slot2.sprite = cancerArcherSprite;
                        break;
                    case Enums.JobType.Priest:
                        slot2.sprite = cancerPriestSprite;
                        break;
                }
            }
            else
            {
                switch (PlayerScript.playerdata.troopSelected[1].troop.job)
                {
                    case Enums.JobType.Knight:
                        slot2.sprite = diabeticKnightSprite;
                        break;
                    case Enums.JobType.Archer:
                        slot2.sprite = diabeticArcherSprite;
                        break;
                    case Enums.JobType.Priest:
                        slot2.sprite = diabeticPriestSprite;
                        break;
                }
            }
            countText2.text = PlayerScript.playerdata.troopSelected[1].count.ToString();
        }
        else
        {
            if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
            {
                switch (PlayerScript.playerdata.troopSelected[2].troop.job)
                {
                    case Enums.JobType.Knight:
                        slot3.sprite = cancerKnightSprite;
                        break;
                    case Enums.JobType.Archer:
                        slot3.sprite = cancerArcherSprite;
                        break;
                    case Enums.JobType.Priest:
                        slot3.sprite = cancerPriestSprite;
                        break;
                }
            }
            else
            {
                switch (PlayerScript.playerdata.troopSelected[2].troop.job)
                {
                    case Enums.JobType.Knight:
                        slot3.sprite = diabeticKnightSprite;
                        break;
                    case Enums.JobType.Archer:
                        slot3.sprite = diabeticArcherSprite;
                        break;
                    case Enums.JobType.Priest:
                        slot3.sprite = diabeticPriestSprite;
                        break;
                }
            }
            countText3.text = PlayerScript.playerdata.troopSelected[2].count.ToString();
        }
    }

    /*********************************Choose Canvas**************************************/

    public void StartChoose()
    {
        knightLevel = knightLevel.GetComponent<Text>();
        archerLevel = archerLevel.GetComponent<Text>();
        priestLevel = priestLevel.GetComponent<Text>();

        knightAttack = knightAttack.GetComponent<Text>();
        archerAttack = archerAttack.GetComponent<Text>();
        priestAttack = priestAttack.GetComponent<Text>();

        knightDefense = knightDefense.GetComponent<Text>();
        archerDefense = archerDefense.GetComponent<Text>();
        priestDefense = priestDefense.GetComponent<Text>();

        knightResource = knightResource.GetComponent<Text>();
        archerResource = archerResource.GetComponent<Text>();
        priestResource = priestResource.GetComponent<Text>();

        chooseKnightSprite = chooseKnightSprite.GetComponent<Image>();
        chooseArcherSprite = chooseArcherSprite.GetComponent<Image>();
        choosePriestSprite = choosePriestSprite.GetComponent<Image>();

        knightLeader = knightLeader.GetComponent<UnityEngine.UI.Button>();
        archerLeader = archerLeader.GetComponent<UnityEngine.UI.Button>();
        priestLeader = priestLeader.GetComponent<UnityEngine.UI.Button>();

    }

    public void InitChoose()
    {
        UpdateLeaderButton();

        //reflect all troop data
        if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
        {
            //set first slot
            chooseKnightSprite.sprite = cancerKnightSprite;
            if (PlayerScript.playerdata.troopData[0].level > 0)
                chooseKnightSprite.GetComponentInChildren<Text>().enabled = false;
            else
                chooseKnightSprite.GetComponentInChildren<Text>().enabled = true;

            knightLevel.text = PlayerScript.playerdata.troopData[0].level.ToString();
            knightAttack.text = PlayerScript.playerdata.troopData[0].damage.ToString();
            knightDefense.text = PlayerScript.playerdata.troopData[0].armor.ToString();
            knightResource.text = PlayerScript.playerdata.troopData[0].energyNeeded.ToString();

            //set second slot
            chooseArcherSprite.sprite = cancerArcherSprite;
            if (PlayerScript.playerdata.troopData[1].level > 0)
                chooseArcherSprite.GetComponentInChildren<Text>().enabled = false;
            else
                chooseArcherSprite.GetComponentInChildren<Text>().enabled = true;

            archerLevel.text = PlayerScript.playerdata.troopData[1].level.ToString();
            archerAttack.text = PlayerScript.playerdata.troopData[1].damage.ToString();
            archerDefense.text = PlayerScript.playerdata.troopData[1].armor.ToString();
            archerResource.text = PlayerScript.playerdata.troopData[1].energyNeeded.ToString();

            //set third slot
            choosePriestSprite.sprite = cancerPriestSprite;
            if (PlayerScript.playerdata.troopData[2].level > 0)
                choosePriestSprite.GetComponentInChildren<Text>().enabled = false;
            else
                choosePriestSprite.GetComponentInChildren<Text>().enabled = true;

            priestLevel.text = PlayerScript.playerdata.troopData[2].level.ToString();
            priestAttack.text = PlayerScript.playerdata.troopData[2].damage.ToString();
            priestDefense.text = PlayerScript.playerdata.troopData[2].armor.ToString();
            priestResource.text = PlayerScript.playerdata.troopData[2].energyNeeded.ToString();
        }
        else
        {
            //set first slot
            chooseKnightSprite.sprite = diabeticKnightSprite;
            if (PlayerScript.playerdata.troopData[0].level > 0)
                chooseKnightSprite.GetComponentInChildren<Text>().enabled = false;
            else
                chooseKnightSprite.GetComponentInChildren<Text>().enabled = true;

            knightLevel.text = PlayerScript.playerdata.troopData[0].level.ToString();
            knightAttack.text = PlayerScript.playerdata.troopData[0].damage.ToString();
            knightDefense.text = PlayerScript.playerdata.troopData[0].armor.ToString();
            knightResource.text = PlayerScript.playerdata.troopData[0].energyNeeded.ToString();

            //set second slot
            chooseArcherSprite.sprite = diabeticArcherSprite;
            if (PlayerScript.playerdata.troopData[1].level > 0)
                chooseArcherSprite.GetComponentInChildren<Text>().enabled = false;
            else
                chooseArcherSprite.GetComponentInChildren<Text>().enabled = true;

            archerLevel.text = PlayerScript.playerdata.troopData[1].level.ToString();
            archerAttack.text = PlayerScript.playerdata.troopData[1].damage.ToString();
            archerDefense.text = PlayerScript.playerdata.troopData[1].armor.ToString();
            archerResource.text = PlayerScript.playerdata.troopData[1].energyNeeded.ToString();

            //set third slot
            choosePriestSprite.sprite = diabeticPriestSprite;
            if (PlayerScript.playerdata.troopData[2].level > 0)
                choosePriestSprite.GetComponentInChildren<Text>().enabled = false;
            else
                choosePriestSprite.GetComponentInChildren<Text>().enabled = true;

            priestLevel.text = PlayerScript.playerdata.troopData[2].level.ToString();
            priestAttack.text = PlayerScript.playerdata.troopData[2].damage.ToString();
            priestDefense.text = PlayerScript.playerdata.troopData[2].armor.ToString();
            priestResource.text = PlayerScript.playerdata.troopData[2].energyNeeded.ToString();
        }
    }

    public void ChooseCanvas_BackPress()
    {
        if (PlayerScript.playerdata.firstResource)
        {
            resourceTutorial.SendMessage("ActivateFinalTutorial");
        }

        slotClicked = 0;
        InitMain();
        playButton.interactable = true;
        chooseCanvas.enabled = false;
    }

    public void Choose_KnightLeader()
    {
        PlayerScript.playerdata.leaderType = Enums.JobType.Knight;
        PrintMessage("Knight is now the leader.");
        UpdateLeaderButton();

        if (PlayerScript.playerdata.firstResource)
            resourceTutorial.SendMessage("ActivateSkillPanel");
    }
    public void Choose_ArcherLeader()
    {
        PlayerScript.playerdata.leaderType = Enums.JobType.Archer;
        PrintMessage("Archer is now the leader.");
        UpdateLeaderButton();
    }
    public void Choose_PriestLeader()
    {
        PlayerScript.playerdata.leaderType = Enums.JobType.Priest;
        PrintMessage("Priest is now the leader.");
        UpdateLeaderButton();
    }

    public void UpdateLeaderButton()
    {
        switch (PlayerScript.playerdata.leaderType)
        {
            case Enums.JobType.Knight:
                knightLeader.interactable = false;
                archerLeader.interactable = true;
                priestLeader.interactable = true;
                break;

            case Enums.JobType.Archer:
                knightLeader.interactable = true;
                archerLeader.interactable = false;
                priestLeader.interactable = true;
                break;

            case Enums.JobType.Priest:
                knightLeader.interactable = true;
                archerLeader.interactable = true;
                priestLeader.interactable = false;
                break;
            default:
                knightLeader.interactable = true;
                archerLeader.interactable = true;
                priestLeader.interactable = true;
                break;
        }
    }
    public void Choose_KnightPress()
    {
        bool allowed = true;
        for (int i = 0; i < PlayerScript.playerdata.troopSelected.Count; i++)
        {
            if (PlayerScript.playerdata.troopSelected[i].troop.job == Enums.JobType.Knight)
            {
                allowed = false;
                break;
            }
        }
        if (PlayerScript.playerdata.troopSelected[slotClicked - 1].troop.job != Enums.JobType.Knight && PlayerScript.playerdata.troopData[0].level > 0 && allowed)
        {
            PlayerScript.playerdata.totalResource += PlayerScript.playerdata.troopSelected[slotClicked - 1].troop.energyNeeded * PlayerScript.playerdata.troopSelected.Count;
            PlayerScript.playerdata.troopSelected[slotClicked - 1].count = 1;
            PlayerScript.playerdata.troopSelected[slotClicked - 1].troop = PlayerScript.playerdata.troopData[0];


            slotClicked = 0;
            InitMain();
            chooseCanvas.enabled = false;

            if (PlayerScript.playerdata.firstResource)
            {
                print("Changed first tutorial");
                resourceTutorial.SendMessage("ActivateLeaderTutorial");
                firstTutorial = false;
            }
        }
    }

    public void Choose_ArcherPress()
    {
        bool allowed = true;

        for (int i = 0; i < PlayerScript.playerdata.troopSelected.Count; i++)
        {
            if (PlayerScript.playerdata.troopSelected[i].troop.job == Enums.JobType.Archer)
            {
                allowed = false;
                break;
            }
        }

        if (PlayerScript.playerdata.troopSelected[slotClicked - 1].troop.job != Enums.JobType.Archer && PlayerScript.playerdata.troopData[1].level > 0 && allowed)
        {
            PlayerScript.playerdata.totalResource += PlayerScript.playerdata.troopSelected[slotClicked - 1].troop.energyNeeded * PlayerScript.playerdata.troopSelected.Count;
            PlayerScript.playerdata.troopSelected[slotClicked - 1].count = 1;
            PlayerScript.playerdata.troopSelected[slotClicked - 1].troop = PlayerScript.playerdata.troopData[1];

            slotClicked = 0;
            InitMain();
            chooseCanvas.enabled = false;
        }
    }

    public void Choose_PriestPress()
    {
        bool allowed = true;

        for (int i = 0; i < PlayerScript.playerdata.troopSelected.Count; i++)
        {
            if (PlayerScript.playerdata.troopSelected[i].troop.job == Enums.JobType.Priest)
            {
                allowed = false;
                break;
            }
        }

        if (PlayerScript.playerdata.troopSelected[slotClicked - 1].troop.job != Enums.JobType.Priest && PlayerScript.playerdata.troopData[2].level > 0 && allowed)
        {
            PlayerScript.playerdata.totalResource += PlayerScript.playerdata.troopSelected[slotClicked - 1].troop.energyNeeded * PlayerScript.playerdata.troopSelected.Count;
            PlayerScript.playerdata.troopSelected[slotClicked - 1].count = 1;
            PlayerScript.playerdata.troopSelected[slotClicked - 1].troop = PlayerScript.playerdata.troopData[2];


            slotClicked = 0;
            InitMain();
            chooseCanvas.enabled = false;
        }
    }

    public void SkillPress()
    {
        skillManager.SendMessage("Show", null);

        if (PlayerScript.playerdata.firstResource)
        {
            resourceTutorial.SendMessage("ActivateFirstSkillTutorial");
        }
    }
}