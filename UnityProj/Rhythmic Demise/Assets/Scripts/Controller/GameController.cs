using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public static GameController gameController;

    private string moveSequence;

    private float startDelayTime = 1f;

    private bool lastHit;
    private int currentStreak;
    private int highestStreak;

    //UI
    public Text archerCountText;
    public Text priestCountText;
    public Text knightCountText;

    private int archerCount;
    private int priestCount;
    private int knightCount;

    //beat spawner
    private int moveBeatCounter;
    private int inputBeatCounter;
    private int beatCounter;
    private NoteControl note1;
    private NoteControl note2;
    private NoteControl note3;
    private NoteControl note4;
    private bool moveActionTurn;
    private bool inputActionTurn;
    private Vector3 comboTextPosition;

    GameObject tutorialManager, tower1, tower2;

    // Use this for initialization
    void Start()
    {
        if (PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth)
        {
            if (PlayerScript.playerdata.firstTut1 || PlayerScript.playerdata.firstTut2 || PlayerScript.playerdata.firstTut3)
                tutorialManager = GameObject.Find("Tutorial Manager");
        }


        if (gameController == null) gameController = this;
        
        init();

        FloatingTextController.Initialize();

        InvokeRepeating("spawnBeat", 0, 0.5f);

        Invoke("updateUI", startDelayTime);
	}

    void init()
    {
        moveBeatCounter = 0;
        inputBeatCounter = 0;
        beatCounter = 0;
        moveActionTurn = false;
        inputActionTurn = false;
        comboTextPosition = new Vector3(0, 4, 0);

        note1 = Resources.Load<NoteControl>("Prefabs/UI/Note1");
        note2 = Resources.Load<NoteControl>("Prefabs/UI/Note2");
        note3 = Resources.Load<NoteControl>("Prefabs/UI/Note3");
        note4 = Resources.Load<NoteControl>("Prefabs/UI/Note4");

        moveSequence = "";
    }
	
	// Update is called once per frame
	void Update () {
        if (moveSequence.Length == 1)
        {
            inputActionTurn = true;
        }

        if (moveSequence.Length >= 4)
        {
            switch (moveSequence)
            {
                //move left
                case "1143":
                    if (ArmyController.armyController.currPos.left != null && ArmyController.armyController.enemyList.Count == 0)
                    {
                        ArmyController.armyController.setCurrentState(Enums.PlayerState.MoveLeft);
                    }
                    moveActionTurn = true;
                    break;
                //move right
                case "1134":
                    if (ArmyController.armyController.currPos.right != null && ArmyController.armyController.enemyList.Count == 0)
                    {
                        ArmyController.armyController.setCurrentState(Enums.PlayerState.MoveRight);
                    }
                    moveActionTurn = true;
                    break;
                //move up
                case "1144":
                    if (ArmyController.armyController.currPos.up != null && ArmyController.armyController.enemyList.Count == 0)
                    {
                        ArmyController.armyController.setCurrentState(Enums.PlayerState.MoveUp);
                    }
                    moveActionTurn = true;
                    break;
                //move down
                case "1133":
                    if (ArmyController.armyController.currPos.bottom != null && ArmyController.armyController.enemyList.Count == 0)
                    {
                        ArmyController.armyController.setCurrentState(Enums.PlayerState.MoveDown);
                    }
                    moveActionTurn = true;
                    break;
                //normal attack
                case "3332":
                    ArmyController.armyController.setCurrentState(Enums.PlayerState.Attack);
                    moveActionTurn = true;
                    break;
                //use special skill
                case "1234":
                    ArmyController.armyController.setCurrentState(Enums.PlayerState.Skill);
                    moveActionTurn = true;
                    break;
            }

            FinalTutorial(moveSequence);
            clearSequence();
        }

	}

    void spawnBeat()
    {
        beatCounter++;

        Instantiate(note1, note1.transform.position, note1.transform.rotation);
        Instantiate(note2, note2.transform.position, note2.transform.rotation);
        Instantiate(note3, note3.transform.position, note3.transform.rotation);
        Instantiate(note4, note4.transform.position, note4.transform.rotation);

        if (inputActionTurn)
        {
            inputBeatCounter++;

            //if miss
            if (!lastHit)
            {
                inputBeatCounter = 0;
                inputActionTurn = false;

                clearSequence();
                
                if(AccessTutorial() && !TutorialManager.TutManager.tut3End
                    || AccessTutorial() && !TutorialManager.TutManager.tut2End)
                     tutorialManager.SendMessage("ShowAll");

                //reset current streak
                currentStreak = 0;
                ScoreManager.comboMultiplier = 1;
            }

            if (inputBeatCounter >= 4)
            {
                inputBeatCounter = 0;
                inputActionTurn = false;

                clearSequence();
            }

            lastHit = false;
        }
        else if (moveActionTurn)
        {
            moveBeatCounter++;

            if (moveBeatCounter == 1)
            {
                FloatingTextController.CreateFloatingText(currentStreak.ToString() + " Combo!!", comboTextPosition);

                currentStreak++;

                //calculate combo multiplier
                ScoreManager.comboMultiplier += 1;
            }

            if (moveBeatCounter >= 4)
            {

                moveActionTurn = false;
                moveBeatCounter = 0;
                ArmyController.armyController.setCurrentState(Enums.PlayerState.Idle);
                ArmyController.armyController.reset();
            }
        }
        //manage if miss the next move after completing a move
        else if (!lastHit)
        {
            clearSequence();

            //reset current streak
            currentStreak = 0;
            ScoreManager.comboMultiplier = 1;
        }

        //check if current streak is the highest streak
        if (currentStreak > highestStreak)
        {
            highestStreak = currentStreak;
        }

        //reset beat counter
        if (beatCounter >= 8) { beatCounter = 0; }
    }

    public void updateUI()
    {
        archerCount = 0;
        priestCount = 0;
        knightCount = 0;

        foreach(Character character in ArmyController.armyController.army)
        {
            switch (character.getJobType())
            {
                case Enums.JobType.Archer:
                    archerCount++;
                    break;
                case Enums.JobType.Knight:
                    knightCount++;
                    break;
                case Enums.JobType.Priest:
                    priestCount++;
                    break;
            }
        }

        archerCountText.text = "x" + archerCount;
        priestCountText.text = "x" + priestCount;
        knightCountText.text = "x" + knightCount;
    }

    public void addHit(string hit)
    {
        moveSequence += hit;
        lastHit = true;

        if (AccessTutorial())
            TutorialCall(hit);
    }

    void FinalTutorial(string move)
    {
        if(AccessTutorial())
        {
            if (TutorialManager.TutManager.final && move == "1234")
            {
                tutorialManager.SendMessage("HideIcon");
                tutorialManager.SendMessage("HideAll");
                TutorialManager.TutManager.tut3End = true;
            }
        }
    }

    bool AccessTutorial()
    {
        if (PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth)
        {
            return ((PlayerScript.playerdata.clickedStageNumber == 1 && PlayerScript.playerdata.firstTut1) ||
                (PlayerScript.playerdata.clickedStageNumber == 2 && PlayerScript.playerdata.firstTut2) ||
                (PlayerScript.playerdata.clickedStageNumber == 3 && PlayerScript.playerdata.firstTut3));
        }
        else if (Application.loadedLevelName == "Resource Management" && PlayerScript.playerdata.firstResource)
            return true;

        return false;
    }

    void TutorialCall(string hit)
    {
        if(PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth && lastHit)
        {
            int index = moveSequence.Length - 1;
            bool hide = false;

            switch (TutorialManager.TutManager.currentPlaying)
            {
                case Enums.TutMove.Right:
                    switch (index)
                    {
                        case 0:
                            //red
                            if (hit == "1")
                                hide = true;
                            break;
                        case 1:
                            //red
                            if (hit == "1")
                                hide = true;
                            break;
                        case 2:
                            //green
                            if (hit == "3")
                                hide = true;
                            break;
                        case 3:
                            //yellow
                            if (hit == "4")
                                hide = true;
                            break;
                    }
                    break;
                case Enums.TutMove.Left:
                    switch (index)
                    {
                        case 0:
                            //red
                            if (hit == "1")
                                hide = true;
                            break;
                        case 1:
                            //red
                            if (hit == "1")
                                hide = true;
                            break;
                        case 2:
                            //green
                            if (hit == "4")
                                hide = true;
                            break;
                        case 3:
                            //yellow
                            if (hit == "3")
                                hide = true;
                            break;
                    }
                    break;
                case Enums.TutMove.Up:
                    switch (index)
                    {
                        case 0:
                            //red
                            if (hit == "1")
                                hide = true;
                            break;
                        case 1:
                            //red
                            if (hit == "1")
                                hide = true;
                            break;
                        case 2:
                            //green
                            if (hit == "4")
                                hide = true;
                            break;
                        case 3:
                            //yellow
                            if (hit == "4")
                                hide = true;
                            break;
                    }
                    break;
                case Enums.TutMove.Down:
                    switch (index)
                    {
                        case 0:
                            //red
                            if (hit == "1")
                                hide = true;
                            break;
                        case 1:
                            //red
                            if (hit == "1")
                                hide = true;
                            break;
                        case 2:
                            //green
                            if (hit == "3")
                                hide = true;
                            break;
                        case 3:
                            //green
                            if (hit == "3")
                                hide = true;
                            break;
                    }
                    break;
                case Enums.TutMove.Attack:
                    switch (index)
                    {
                        case 0:
                            //green
                            if (hit == "3")
                                hide = true;
                            break;
                        case 1:
                            //green
                            if (hit == "3")
                                hide = true;
                            break;
                        case 2:
                            //green
                            if (hit == "3")
                                hide = true;
                            break;
                        case 3:
                            //blue
                            if (hit == "2")
                                hide = true;
                            break;
                    }
                    break;
                case Enums.TutMove.Defend:
                    switch (index)
                    {
                        case 0:
                            //yellow
                            if (hit == "4")
                                hide = true;
                            break;
                        case 1:
                            //yellow
                            if (hit == "4")
                                hide = true;
                            break;
                        case 2:
                            //yellow
                            if (hit == "4")
                                hide = true;
                            break;
                        case 3:
                            //red
                            if (hit == "1")
                                hide = true;
                            break;
                    }
                    break;
                case Enums.TutMove.Skill:
                    switch (index)
                    {
                        case 0:
                            //red
                            if (hit == "1")
                                hide = true;
                            break;
                        case 1:
                            //blue
                            if (hit == "2")
                                hide = true;
                            break;
                        case 2:
                            //green
                            if (hit == "3")
                                hide = true;
                            break;
                        case 3:
                            //yellow
                            if (hit == "4")
                                hide = true;
                            break;
                    }
                    break;
            }
            
            if (hide)
            {
                tutorialManager.SendMessage("Hide", index);
            }
            else
            {
                if(AccessTutorial() && !TutorialManager.TutManager.tut3End)
                    tutorialManager.SendMessage("ShowAll");

            }
        }
    }

    public void addScore(int point)
    {
        ScoreManager.score += point;
    }

    public void clearSequence()
    {
        moveSequence = "";
    }

    public void levelUp(int tIndex)
    {
        PlayerScript.playerdata.troopData[tIndex].damage += 5;
        PlayerScript.playerdata.troopData[tIndex].maxHealth += 15;
        PlayerScript.playerdata.troopData[tIndex].armor += 1;
    }

    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }

    public int getCurrentStreak()
    {
        return currentStreak;
    }
}
