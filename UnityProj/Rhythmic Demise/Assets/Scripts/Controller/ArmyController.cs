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
    private MovingPoint targetPos;

    public Enums.PlayerState currentAction;

    public static List<GameObject> enemyList;
    public static float closestDist;
    public static GameObject firstEnemy;
    public static GameObject closestEnemy;

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

	// Use this for initialization
	void Start () {
        if (armyController == null)
        {
            armyController = this;
        }

        army = new List<Character>();
        enemyList = new List<GameObject>();

        init();
	}
	
	// Update is called once per frame
	void Update () {
        findClosestEnemy();

        switch (currentAction)
        {
            case Enums.PlayerState.Idle:
                break;
            case Enums.PlayerState.MoveUp:
                targetPos = currPos.up;
                moveTo(targetPos);
                break;
            case Enums.PlayerState.MoveDown:
                targetPos = currPos.bottom;
                moveTo(targetPos);
                break;
            case Enums.PlayerState.MoveLeft:
                targetPos = currPos.left;
                moveTo(targetPos);
                break;
            case Enums.PlayerState.MoveRight:
                targetPos = currPos.right;
                moveTo(targetPos);
                break;
            case Enums.PlayerState.Attack:
                attack();
                break;
            case Enums.PlayerState.Skill:
                useSkill();
                break;
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
            army.Add(k);
        }

        archerCount = PlayerScript.playerdata.troopSelected[1].count;
        for (int i = 0; i < archerCount; i++)
        {
            float archerY = Random.Range(currPos.transform.position.y - 1, currPos.transform.position.y + 1);
            Vector3 archerTempPos = new Vector3(currPos.transform.position.x - 1, archerY);
            Archer a = Instantiate(archerPrefab, archerTempPos, archerPrefab.transform.rotation) as Archer;
            army.Add(a);
        }

        priestCount = PlayerScript.playerdata.troopSelected[2].count;
        for (int i = 0; i < priestCount; i++)
        {
            float priestY = Random.Range(currPos.transform.position.y - 1, currPos.transform.position.y + 1);
            Vector3 priestTempPos = new Vector3(currPos.transform.position.x, priestY);
            Priest p = Instantiate(priestPrefab, priestTempPos, priestPrefab.transform.rotation) as Priest;
            army.Add(p);
        }
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
        foreach (Character c in army)
        {
            c.useSkill();
        }
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

                    if(c.getInPath())
                        c.moveTo(c.getGoalPos());
                    break;
                case Enums.JobType.Archer:
                    if (!isRandomOnce)
                    {
                        archerY = Random.Range(pos.transform.position.y - 1, pos.transform.position.y + 1);
                        archerTempPos = new Vector3(pos.transform.position.x - 1, archerY);
                        c.setGoalPos(archerTempPos);
                    }

                    if (c.getInPath())
                        c.moveTo(c.getGoalPos());
                    break;
                case Enums.JobType.Priest:
                    if (!isRandomOnce)
                    {
                        priestY = Random.Range(pos.transform.position.y - 1, pos.transform.position.y + 1);
                        priestTempPos = new Vector3(pos.transform.position.x, priestY);
                        c.setGoalPos(priestTempPos);
                    }

                    if (c.getInPath())
                        c.moveTo(c.getGoalPos());
                    break;
            }
        }

        isRandomOnce = true;
    }

    public static void addEnemyList(GameObject enemy)
    {
        if (!enemyList.Contains(enemy))
        {
            enemyList.Add(enemy);
        }
    }

    public static void removeEnemyList(GameObject enemy)
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
                ScoreManager.score += 10;
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
}
