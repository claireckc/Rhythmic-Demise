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

    private Knight knightPrefab;
    private Archer archerPrefab;
    private Priest priestPrefab;

    void init()
    {
        //init prefab
        if(PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
        {
            knightPrefab = Resources.Load<Knight>("Prefabs/CancerKnight");
            archerPrefab = Resources.Load<Archer>("Prefabs/CancerArcher");
            priestPrefab = Resources.Load<Priest>("Prefabs/CancerPriest");
        }
        else if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Diabetic)
        {
            knightPrefab = Resources.Load<Knight>("Prefabs/DiabeticKnight");
            archerPrefab = Resources.Load<Archer>("Prefabs/DiabeticArcher");
            priestPrefab = Resources.Load<Priest>("Prefabs/DiabeticPriest");
        }

        armyController = gameObject.AddComponent<ArmyController>();

        knightCount = PlayerScript.playerdata.troopSelected[0].count;
        for (int i = 0; i < knightCount; i++)
        {
            Knight k = Instantiate(knightPrefab, currPos.transform.position, knightPrefab.transform.rotation) as Knight;
            army.Add(k);
        }

        archerCount = PlayerScript.playerdata.troopSelected[1].count;
        for (int i = 0; i < archerCount; i++)
        {
            Archer a = Instantiate(archerPrefab, currPos.transform.position, archerPrefab.transform.rotation) as Archer;
            army.Add(a);
        }

        priestCount = PlayerScript.playerdata.troopSelected[2].count;
        for (int i = 0; i < priestCount; i++)
        {
            Priest p = Instantiate(priestPrefab, currPos.transform.position, priestPrefab.transform.rotation) as Priest;
            army.Add(p);
        }

        armyController.initArmy(army,currPos);
    }

    // Use this for initialization
    void Start()
    {
        init();

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
                    if (currPos.left != null)
                    {
                        armyController.setCurrentState(Enums.PlayerState.Move);
                        armyController.moveTo(currPos.left);
                        currPos = currPos.left;
                    }
                    bs.moveActionTurn = true;
                    break;
                //move right
                case "1134":
                    if (currPos.right != null)
                    {
                        armyController.setCurrentState(Enums.PlayerState.Move);
                        armyController.moveTo(currPos.right);
                        currPos = currPos.right;
                    }
                    bs.moveActionTurn = true;
                    break;
                //move up
                case "1144":
                    if (currPos.up != null)
                    {
                        armyController.setCurrentState(Enums.PlayerState.Move);
                        armyController.moveTo(currPos.up);
                        currPos = currPos.up;
                    }
                    bs.moveActionTurn = true;
                    break;
                //move down
                case "1133":
                    if (currPos.bottom != null)
                    {
                        armyController.setCurrentState(Enums.PlayerState.Move);
                        armyController.moveTo(currPos.bottom);
                        currPos = currPos.bottom;
                    }
                    bs.moveActionTurn = true;
                    break;
                //normal attack
                case "3332":
                    armyController.setCurrentState(Enums.PlayerState.Attack);
                    bs.moveActionTurn = true;
                    break;
                //use special skill
                case "1234":
                    armyController.setCurrentState(Enums.PlayerState.Skill);
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
