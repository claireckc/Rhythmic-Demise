using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ArmyController : MonoBehaviour {

    public static ArmyController armyController;

    public List<Character> army;
    public MovingPoint goalPos;
    private Character leader;
    public MovingPoint currPos;
    public MovingPoint targetPos;

    public Enums.PlayerState currentAction;

    public List<GameObject> enemyList;
    public float closestDist;
    public GameObject firstEnemy;
    public GameObject closestEnemy;

    private int archerCount;
    private int priestCount;
    private int knightCount;

    private Knight knightPrefab;
    private Archer archerPrefab;
    private Priest priestPrefab;

    bool isRandomOnce;
    float knightY;
    float archerY;
    float priestY;
    Vector3 knightTempPos;
    Vector3 archerTempPos;
    Vector3 priestTempPos;

    MovingPoint movingPt1, movingPt2, movingPt3, movingPt4, movingPt5, movingPt6, endPoint;
    MovingPoint prevPoint;
    GameObject tutManager;
    GameObject textManager;

    bool moved, callMovingPt2;

    Tower tower1, tower2;

    int enumIndex;

	// Use this for initialization

	void Start () {
        if (PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth)
        {
            tutManager = GameObject.Find("Tutorial Manager");
            textManager = GameObject.Find("Text Manager");

            if (PlayerScript.playerdata.clickedStageNumber == 1)
            {
                tower1 = GameObject.Find("Towers/Shooting Tower 1").GetComponent<Tower>();
                tower2 = GameObject.Find("Towers/Shooting Tower").GetComponent<Tower>();
            }
        }

        moved = callMovingPt2 = false;

        if (armyController == null)
        {
            armyController = this;
        } 

        army = new List<Character>();
        enemyList = new List<GameObject>();

        init();
        GetMovingPoints();
	}
	
	// Update is called once per frame
	void Update () {
        //Debug.Log(currPos + " tp: " + targetPos);
        checkHealth();

        findClosestEnemy();

        enumIndex = (int)currentAction;

        if(PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth)
            TutorialCall();

        switch (currentAction)
        {
            case Enums.PlayerState.Idle:
                foreach (Character c in army)
                {
                    c.anim.SetInteger("State", enumIndex);
                }
                break;
            case Enums.PlayerState.MoveUp:
                targetPos = currPos.up;
                moveTo(targetPos);

                foreach (Character c in army)
                {
                    c.anim.SetInteger("State", enumIndex);
                }
                break;
            case Enums.PlayerState.MoveDown:
                targetPos = currPos.bottom;
                moveTo(targetPos);

                foreach (Character c in army)
                {
                    c.anim.SetInteger("State", enumIndex);
                }
                break;
            case Enums.PlayerState.MoveLeft:
                targetPos = currPos.left;
                moveTo(targetPos);

                foreach (Character c in army)
                {
                    c.anim.SetInteger("State", enumIndex);
                }
                break;
            case Enums.PlayerState.MoveRight:
                targetPos = currPos.right;
                moveTo(targetPos);
                foreach (Character c in army)
                {
                    c.anim.SetInteger("State", enumIndex);
                }
                break;
            case Enums.PlayerState.Attack:
                attack();
                break;
            case Enums.PlayerState.Skill:
                useSkill();
                break;
        }
	}

    void GetMovingPoints()
    {
        if(PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth)
        {
            switch (PlayerScript.playerdata.clickedStageNumber)
            {
                case 1: //first tutorial, 6 moving points
                    movingPt1 = GameObject.Find("MovingPoints/MovingPoint1").GetComponent<MovingPoint>();
                    movingPt2 = GameObject.Find("MovingPoints/MovingPoint2").GetComponent<MovingPoint>();
                    movingPt3 = GameObject.Find("MovingPoints/MovingPoint3").GetComponent<MovingPoint>();
                    movingPt4 = GameObject.Find("MovingPoints/MovingPoint4").GetComponent<MovingPoint>();
                    movingPt5 = GameObject.Find("MovingPoints/MovingPoint5").GetComponent<MovingPoint>();
                    movingPt6 = GameObject.Find("MovingPoints/MovingPoint6").GetComponent<MovingPoint>();
                    endPoint = GameObject.Find("MovingPoints/EndPoint").GetComponent<MovingPoint>();
                    break;
                case 2:
                    //second tutorial, only appear in movingpt2
                    movingPt1 = GameObject.Find("MovingPoints/MovingPoint1").GetComponent<MovingPoint>();
                    movingPt2 = GameObject.Find("MovingPoints/MovingPoint2").GetComponent<MovingPoint>();
                    endPoint = GameObject.Find("MovingPoints/EndPoint").GetComponent<MovingPoint>();
                    break;
                case 3: //boss stage
                    break;
       
            }
        }
    }

    void checkHealth()
    {
        for (int i = 0; i < army.Count; i++)
        {
            Character c = army[i];
            if (c.IsDead)
            {
                army.Remove(army[i]);
                Destroy(c.gameObject);
            }
        }
    }

    void TutorialCall()
    {
        if (PlayerScript.playerdata.clickedStageNumber == 1 && PlayerScript.playerdata.firstTut1)
        {
            if (currPos != prevPoint)
            {
                if (currPos == movingPt1)
                {
                    textManager.SendMessage("ShowPanel", false);
                    prevPoint = currPos;
                }

                if (currPos == movingPt2)
                {
                    if (!callMovingPt2)
                        textManager.SendMessage("ShowPanel", true);

                    tutManager.SendMessage("PlayAttack");
                    callMovingPt2 = true;

                    if (!tower1.IsDead)
                        tutManager.SendMessage("PlayAttack");
                    else
                    {
                        tutManager.SendMessage("PlayMoveRight");
                        prevPoint = currPos;
                    }
                }

                if (currPos == movingPt3 || currPos == movingPt4 || currPos == movingPt6)
                {
                    tutManager.SendMessage("PlayMoveRight");
                    prevPoint = currPos;
                }

                if (currPos == movingPt5)
                {
                    if (!tower2.IsDead)
                        tutManager.SendMessage("PlayAttack");
                    else {
                        tutManager.SendMessage("PlayMoveRight");
                        prevPoint = currPos;
                    }
                }

                if (currPos == endPoint)
                {
                    tutManager.SendMessage("HideIcon");
                    PlayerScript.playerdata.firstTut1 = false;  //prevent tutorial from playing again
                    SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
                    prevPoint = currPos;
                }
            }
        }
        else if (PlayerScript.playerdata.clickedStageNumber == 2 && PlayerScript.playerdata.firstTut2)
        {
            if(currPos != prevPoint)
            {
                if(currPos == movingPt2)
                {
                    textManager.SendMessage("ShowTutorial2Panel");
                    moved = true;
                    prevPoint = currPos;
                }
                else if (currPos == endPoint)
                {
                    print("End point");
                    PlayerScript.playerdata.firstTut2 = false;
                    SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
                }
                
                if(currPos != movingPt1 && currPos != movingPt2 && !TutorialManager.TutManager.tut2End)
                {
                    tutManager.SendMessage("HideAll");
                    TutorialManager.TutManager.tut2End = true;
                    textManager.SendMessage("DestroyAll");
                }
            }
        }
        
    }

    void init()
    {
        //init prefab
        if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
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

        knightCount = PlayerScript.playerdata.troopSelected[0].count;
        for (int i = 0; i < knightCount; i++)
        {
            float knightY = Random.Range(currPos.transform.position.y - 1, currPos.transform.position.y + 1);
            Vector3 knightTempPos = new Vector3(currPos.transform.position.x + 1, knightY);
            Knight k = Instantiate(knightPrefab, knightTempPos, knightPrefab.transform.rotation) as Knight;

            if (PlayerScript.playerdata.leaderType == Enums.JobType.Knight && i == 0)
            {
                leader = k;
            }

            army.Add(k);
        }

        archerCount = PlayerScript.playerdata.troopSelected[1].count;
        for (int i = 0; i < archerCount; i++)
        {
            float archerY = Random.Range(currPos.transform.position.y - 1, currPos.transform.position.y + 1);
            Vector3 archerTempPos = new Vector3(currPos.transform.position.x - 1, archerY);
            Archer a = Instantiate(archerPrefab, archerTempPos, archerPrefab.transform.rotation) as Archer;

            if (PlayerScript.playerdata.leaderType == Enums.JobType.Archer && i == 0)
            {
                leader = a;
            }

            army.Add(a);
        }

        priestCount = PlayerScript.playerdata.troopSelected[2].count;
        for (int i = 0; i < priestCount; i++)
        {
            float priestY = Random.Range(currPos.transform.position.y - 1, currPos.transform.position.y + 1);
            Vector3 priestTempPos = new Vector3(currPos.transform.position.x, priestY);
            Priest p = Instantiate(priestPrefab, priestTempPos, priestPrefab.transform.rotation) as Priest;

            if (PlayerScript.playerdata.leaderType == Enums.JobType.Priest && i == 0)
            {
                leader = p;
            }

            army.Add(p);
        }
        Invoke("initLeaderBonus", 1);
        Invoke("initAnimVar", 1);
        targetPos = currPos;
    }

    public void initArmy(List<Character> a)
    {
        army = a;
    }

    public void setCurrentState(Enums.PlayerState action)
    {
        currentAction = action;
    }

    public void reset()
    {
        //Update current position to the target position
        currPos = targetPos;

        isRandomOnce = false;

        foreach (Character c in army)
        {
            c.reset();
        }
    }

    public void attack()
    {
        foreach (Character c in army)
        {
            c.attack();
        }
    }

    public void useSkill()
    {
        leader.useSkill();
    }

    public void moveTo(MovingPoint pos)
    {
        goalPos = pos;

        foreach (Character c in army)
        {
            switch (c.getJobType())
            {
                case Enums.JobType.Knight:
                    if(!isRandomOnce)
                    {
                        knightY = Random.Range(pos.transform.position.y - 1, pos.transform.position.y + 1);
                        knightTempPos = new Vector3(pos.transform.position.x + 1, knightY);
                        c.setGoalPos(knightTempPos);
                    }

                    if (!c.getInPath())
                    {
                        knightY = Random.Range(pos.transform.position.y - 1, pos.transform.position.y + 1);
                        knightTempPos = new Vector3(pos.transform.position.x + 1, knightY);
                        c.setGoalPos(knightTempPos);
                    }
                    
                    c.moveTo(c.getGoalPos());
                    break;
                case Enums.JobType.Archer:
                    if (!isRandomOnce)
                    {
                        archerY = Random.Range(pos.transform.position.y - 1, pos.transform.position.y + 1);
                        archerTempPos = new Vector3(pos.transform.position.x - 1, archerY);
                        c.setGoalPos(archerTempPos);
                    }

                    if (!c.getInPath())
                    {
                        archerY = Random.Range(pos.transform.position.y - 1, pos.transform.position.y + 1);
                        archerTempPos = new Vector3(pos.transform.position.x - 1, archerY);
                        c.setGoalPos(archerTempPos);
                    }
                        
                    c.moveTo(c.getGoalPos());
                    break;
                case Enums.JobType.Priest:
                    if (!isRandomOnce)
                    {
                        priestY = Random.Range(pos.transform.position.y - 1, pos.transform.position.y + 1);
                        priestTempPos = new Vector3(pos.transform.position.x, priestY);
                        c.setGoalPos(priestTempPos);
                    }

                    if (!c.getInPath())
                    {
                        priestY = Random.Range(pos.transform.position.y - 1, pos.transform.position.y + 1);
                        priestTempPos = new Vector3(pos.transform.position.x, priestY);
                        c.setGoalPos(priestTempPos);
                    }
                        
                    c.moveTo(c.getGoalPos());
                    break;
            }
        }

        isRandomOnce = true;
    }

    public void addEnemyList(GameObject enemy)
    {
        if (!enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
        }
    }

    public void removeEnemyList(GameObject enemy)
    {
        enemyList.Remove(enemy);
    }

    protected void UpdateEnemyList()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            Enemy e = enemyList[i].GetComponent<Enemy>();
            
            if (e.IsDead || e == null)
            {
                enemyList.Remove(enemyList[i]);

                Destroy(e.gameObject);
                
                //add score
                ScoreManager.addScore();
            }
        }
    }

    public void findClosestEnemy()
    {
        UpdateEnemyList();

        if (enemyList.Count > 0)
        {
            firstEnemy = enemyList[0];
            closestDist = Vector2.Distance(this.transform.position, firstEnemy.transform.position);
            closestEnemy = firstEnemy;

            foreach (GameObject go in enemyList)
            {
                float currentDist = Vector2.Distance(this.transform.position, go.transform.position);
                if (closestDist > currentDist)
                {
                    closestDist = currentDist;
                    closestEnemy = go;
                    break;
                }
            }
        }
        else
            closestEnemy = null;
    }

    void initLeaderBonus()
    {
        //init leader bonus buff
        switch (PlayerScript.playerdata.leaderType)
        {
            case Enums.JobType.Knight:
                foreach (Character c in army)
                {
                    //increase defense by 5
                    c.setArmor(c.getArmor() + 5);
                }
                break;
            case Enums.JobType.Archer:
                foreach (Character c in army)
                {
                    //increase damage by 5
                    c.setDamage(c.getDamage() + 5);
                }
                break;
            case Enums.JobType.Priest:
                foreach (Character c in army)
                {
                    //increase hp by 20%
                    c.addMaxHealth(50);
                }
                break;
        }
    }

    void initAnimVar()
    {

        foreach (Character c in army)
        {
            c.anim.SetInteger("Type", (int)PlayerScript.playerdata.pathogenType);
        }
    }

    public void healArmy(float hp)
    {
        foreach (Character c in army)
        {
            c.addHealth(hp);
        }
    }

    public void takeDamage(float d)
    {
        foreach (Character c in army)
        {
            c.TakeDamage(d);
        }
    }
}
