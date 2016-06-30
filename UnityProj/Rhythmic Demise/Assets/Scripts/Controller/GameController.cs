using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {
    public static GameController gameController;

    public string moveSequence;

    public KeyCode[] buttonsKeyCode;

    public ArmyController armyController;
    public List<Character> army;
    public BeatSpawner bs;

    private float startDelayTime = 1f;

    public MovingPoint currPos;

    //UI
    public Text archerCountText;
    public Text priestCountText;
    public Text knightCountText;

    private int archerCount;
    private int priestCount;
    private int knightCount;

    void init()
    {
        moveSequence = "";
        armyController = gameObject.AddComponent<ArmyController>();
        armyController.initArmy(army, currPos);
        Invoke("updateUI", startDelayTime);
    }

	// Use this for initialization
	void Start () {
        init();
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
                case "1111":
                    if (currPos.left != null)
                    {
                        armyController.setCurrentState(Enums.PlayerState.Move);
                        armyController.moveTo(currPos.left);
                        currPos = currPos.left;
                    }
                    break;
                //move right
                case "1143":
                    if (currPos.right != null)
                    {
                        armyController.setCurrentState(Enums.PlayerState.Move);
                        armyController.moveTo(currPos.right);
                        currPos = currPos.right;
                    }
                    break;
                //move up
                case "1132":
                    if (currPos.up != null)
                    {
                        armyController.setCurrentState(Enums.PlayerState.Move);
                        armyController.moveTo(currPos.up);
                        currPos = currPos.up;
                    }
                    break;
                //move down
                case "1134":
                    if (currPos.bottom != null)
                    {
                        armyController.setCurrentState(Enums.PlayerState.Move);
                        armyController.moveTo(currPos.bottom);
                        currPos = currPos.bottom;
                    }
                    break;
                //normal attack
                case "3332":
                    armyController.setCurrentState(Enums.PlayerState.Attack);
                    break;
                //use special skill
                case "1234":
                    armyController.setCurrentState(Enums.PlayerState.Skill);
                    break;
            }

            clearSequence();
            bs.moveActionTurn = true;
        }
	}

    public void updateUI()
    {
        archerCount = 0;
        priestCount = 0;
        knightCount = 0;

        foreach(Character character in army)
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
