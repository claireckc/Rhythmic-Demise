using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public static GameController gameController;

    public string moveSequence;

    public KeyCode[] buttonsKeyCode;

    public BeatSpawner bs;

    private float startDelayTime = 1f;

    public bool lastHit;
    public int currentStreak;
    public int highestStreak;

    //UI
    public Text archerCountText;
    public Text priestCountText;
    public Text knightCountText;

    private int archerCount;
    private int priestCount;
    private int knightCount;

    // Use this for initialization
    void Start()
    {
        moveSequence = "";

        Invoke("updateUI", startDelayTime);

        FloatingTextController.Initialize();
	}
	
	// Update is called once per frame
	void Update () {
        if (moveSequence.Length == 1)
        {
            bs.inputActionTurn = true;
        }

        if (moveSequence.Length >= 4)
        {
            switch (moveSequence)
            {
                //move left
                case "1143":
                    if (ArmyController.armyController.currPos.left != null)
                    {
                        ArmyController.armyController.setCurrentState(Enums.PlayerState.MoveLeft);
                    }
                    bs.moveActionTurn = true;
                    break;
                //move right
                case "1134":
                    if (ArmyController.armyController.currPos.right != null)
                    {
                        ArmyController.armyController.setCurrentState(Enums.PlayerState.MoveRight);
                    }
                    bs.moveActionTurn = true;
                    break;
                //move up
                case "1144":
                    if (ArmyController.armyController.currPos.up != null)
                    {
                        ArmyController.armyController.setCurrentState(Enums.PlayerState.MoveUp);
                    }
                    bs.moveActionTurn = true;
                    break;
                //move down
                case "1133":
                    if (ArmyController.armyController.currPos.bottom != null)
                    {
                        ArmyController.armyController.setCurrentState(Enums.PlayerState.MoveDown);
                    }
                    bs.moveActionTurn = true;
                    break;
                //normal attack
                case "3332":
                    ArmyController.armyController.setCurrentState(Enums.PlayerState.Attack);
                    bs.moveActionTurn = true;
                    break;
                //use special skill
                case "1234":
                    ArmyController.armyController.setCurrentState(Enums.PlayerState.Skill);
                    bs.moveActionTurn = true;
                    break;
            }

            clearSequence();
        }
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
    }

    public void addScore(int point)
    {
        ScoreManager.score += point;
    }

    public void clearSequence()
    {
        moveSequence = "";
    }

    public void LoadLevel(string level)
    {
        Application.LoadLevel(level);
    }
}
