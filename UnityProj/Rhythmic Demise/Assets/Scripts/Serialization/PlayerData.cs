using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Serialization;

[System.Serializable]
public class PlayerData
{

    //Resources
    public int totalResource, totalEnergy;

    //Troops information
    public Enums.JobType leaderType;
    public Enums.SkillName skillSelected;
    public Enums.CharacterType pathogenType;
    public List<Troop> troopData;       //first is knight then archer and then priest, to store the stats and the level
    public List<TroopSelected> troopSelected;

    //multiplier
    public float attackMultiplier;
    public float defenseMultiplier;

    //Map progress
    public List<MainMap> mapProgress;

    //settings
    public float globalVolume, effectsVolume, buttonAlpha;

    private Troop tp;
    private Skills sk;
    private MainMap newMap;
    private TroopSelected ts;
    private SubMap stage;

    public PlayerData()
    {
        Init();
    }

    public void Init()
    {
        Debug.Log("call init in playerdata script");
        leaderType = Enums.JobType.None;
        skillSelected = Enums.SkillName.None;
        pathogenType = Enums.CharacterType.None;
        globalVolume = effectsVolume = buttonAlpha = 1.0f;
        totalResource = 5;
        totalEnergy = 5;
        troopData = new List<Troop>();
        troopSelected = new List<TroopSelected>();

        attackMultiplier = 1;
        defenseMultiplier = 1;

        //for troop data
        for (int i = 0; i < 3; i++)
        {
            tp = new Troop();
            tp.skills = new List<Skills>();
            tp.job = (Enums.JobType)i + 1;
            if (i == 0)
                tp.level = i + 1;     //first unlock would be the knight
            else
                tp.level = i + 1;//should be 0, set temp for debug;

            //MICHAEL SET MAX HEALTH, ATTACK, DEFENSERATING HERE, THIS IS INITIALIZATION

            switch (i)
            {
                case 0: //knight
                    tp.currentHealth = tp.maxHealth = 15;
                    tp.attack = 3 * attackMultiplier;
                    tp.defenseRating = 2 * defenseMultiplier;
                    tp.energyNeeded = 2;
                    break;
                case 1: //archer
                    tp.currentHealth = tp.maxHealth = 8;
                    tp.attack = 2 * attackMultiplier;
                    tp.defenseRating = 1 * defenseMultiplier;
                    tp.energyNeeded = 1;
                    break;
                case 2: //priest
                    tp.currentHealth = tp.maxHealth = 10;
                    tp.attack = 1 * attackMultiplier;
                    tp.defenseRating = 1.2f * defenseMultiplier;
                    tp.energyNeeded = 1;
                    break;
            }
            
            for (int j = 0; j < 3; j++)
            {
                sk = new Skills();
                switch (i)
                {
                    //if knight
                    case 0:
                        sk.skillName = (Enums.SkillName)j + 1;
                        break;
                    //if archer
                    case 1:
                        sk.skillName = (Enums.SkillName)j + 4;
                        break;
                    //if priest
                    case 2:
                        sk.skillName = (Enums.SkillName)j + 7;
                        break;
                }
                if (i == 0)
                {
                    sk.skillLevel = 1;
                    /*sk.skillValue = sk.skilllevel * SOMETHING    //MICHAEL, SET SKILL VALUE HERE, THIS IS INITIALIZATION! KNIGHT SKILL VALUE*/
                }
                else
                {
                    sk.skillLevel = 0;
                    sk.skillValue = 0;
                }

                tp.skills.Add(sk);
            }

            troopData.Add(tp);
        }

        //for troop selected
        for (int i = 0; i < 3; i++)
        {
            ts = new TroopSelected();
            ts.troop = new Troop();
            ts.count = 0;
            troopSelected.Add(ts);
        }

        mapProgress = new List<MainMap>();
        //for map
        for (int i = 0; i < Enums.MAINMAPCOUNT; i++)
        {
            newMap = new MainMap();
            newMap.mapName = (Enums.MainMap)i;

            //initialize the number of substages, when everything is confirmed
            newMap.stages = new List<SubMap>();
            if (i == 0) //tutorial stage
            {
                newMap.isLocked = false;
                for (int k = 0; k < 3; k++)
                {
                    stage = new SubMap();
                    stage.parentMap = newMap.mapName;
                    stage.mapId = k;
                    stage.topComboCount = stage.resourceAttained = 0;
                    stage.stars = 0f;
                    stage.comboRange = new List<int>();
                    for (int j = 0; j < 3; j++)
                        stage.comboRange.Add(0);
                    stage.isComplete = false;
                    stage.isCurrent = false;
                    newMap.stages.Add(stage);
                }
            }
            else
                newMap.isLocked = true;

            newMap.avgStars = 0;
            newMap.isComplete = false;
            mapProgress.Add(newMap);
        }
    }

    /*public void Awake()
    {
        if (playerdata != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            playerdata = this;
        }
    }
    public void Start()
    {
        Init();
    }*/
}