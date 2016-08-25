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
    public Enums.MainMap clickedMap;
    public int clickedStageNumber;

    //multiplier
    public int attackBonus;
    public int defenseBonus;
    public float expMultiplier;     //base on stage number and player unit level

    //Map progress
    public List<MainMap> mapProgress;

    //settings
    public float globalVolume, effectsVolume, buttonAlpha;

    //for tutorial
    public bool firstTut1, firstTut2, firstTut3, firstResource, firstMap;

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

        attackBonus = 0;
        defenseBonus = 0;
        expMultiplier = 1f;

        firstTut1 = firstTut2 = firstTut3 = firstResource = firstMap = true;

        //for troop data
        for (int i = 0; i < 3; i++)
        {
            tp = new Troop();
            tp.skills = new List<Skills>();
            tp.job = (Enums.JobType)i + 1;
            if (i == 0)
                tp.level = i + 1;     //first unlock would be the knight
            else
                tp.level = i + 1;//should be 0, set temp for debug mode;

            //MICHAEL SET MAX HEALTH, ATTACK, DEFENSERATING HERE, THIS IS INITIALIZATION

            switch (i)
            {
                case 0: //knight
                    tp.currentHealth = tp.maxHealth = 200;
                    tp.damage = 20 + attackBonus;
                    tp.armor = 5 + defenseBonus;
                    tp.energyNeeded = 1;
                    break;
                case 1: //archer
                    tp.currentHealth = tp.maxHealth = 130;
                    tp.damage = 20 + attackBonus;
                    tp.armor = 2 + defenseBonus;
                    tp.energyNeeded = 1;
                    break;
                case 2: //priest
                    tp.currentHealth = tp.maxHealth = 100;
                    tp.damage = 12 + attackBonus;
                    tp.armor = 2 + defenseBonus;
                    tp.energyNeeded = 1;
                    break;
            }

            for (int j = 0; j < 2; j++)
            {
                sk = new Skills();
                switch (i)
                {
                    //if knight
                    case 0:
                        switch (j)
                        {
                            case 0:
                                sk.skillName = Enums.SkillName.KnightCharge;
                                sk.skillValue = 25;
                                sk.skillLevel = 1;
                                sk.skillCooldown = 20;
                                break;
                            case 1:
                                sk.skillName = Enums.SkillName.KnightHigh;
                                sk.skillValue = 50;
                                sk.skillLevel = 1;
                                sk.skillCooldown = 20;
                                break;
                        }
                        break;
                    //if archer
                    case 1:
                        switch (j)
                        {
                            case 0:
                                sk.skillName = Enums.SkillName.ArcherAOE;
                                sk.skillValue = 25;
                                sk.skillLevel = 0;
                                sk.skillCooldown = 20;
                                break;
                            case 1:
                                sk.skillName = Enums.SkillName.ArcherHigh;
                                sk.skillValue = 50;
                                sk.skillLevel = 0;
                                sk.skillCooldown = 20;
                                break;
                        }
                        break;
                    //if priest
                    case 2:
                        switch (j)
                        {
                            case 0:
                                sk.skillName = Enums.SkillName.PriestHeal;
                                sk.skillValue = 80;
                                sk.skillLevel = 0;
                                sk.skillCooldown = 20;
                                break;
                            case 1:
                                sk.skillName = Enums.SkillName.PriestHex;
                                sk.skillValue = 5;
                                sk.skillLevel = 0;
                                sk.skillCooldown = 20;
                                break;
                        }
                        break;
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
            ts.troop.level = 1;
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
            if (i == 1 || i == 5)
            {
                //larnyx and liver only has two stages
                newMap.isLocked = true;
                for (int k = 0; k < 2; k++)
                {
                    stage = new SubMap();
                    stage.parentMap = newMap.mapName;
                    stage.mapId = k;
                    stage.topComboCount = stage.resourceAttained = -1;
                    stage.stars = 0f;
                    stage.comboRange = new List<int>();
                    for (int j = 0; j < 3; j++)
                        stage.comboRange.Add(0);
                    newMap.stages.Add(stage);
                }
            }
            else
            {
                if (i == 0)
                    newMap.isLocked = false;
                else
                    newMap.isLocked = true;

                for (int k = 0; k < 3; k++)
                {
                    stage = new SubMap();
                    stage.parentMap = newMap.mapName;
                    stage.mapId = k;
                    if (i == 0 && k == 0)
                    {
                        stage.topComboCount = 0;
                        stage.resourceAttained = -1;
                    }
                    else
                        stage.topComboCount = stage.resourceAttained = -1;
                    stage.stars = 0f;
                    stage.comboRange = new List<int>();
                    for (int j = 0; j < 3; j++)
                        stage.comboRange.Add(0);
                    newMap.stages.Add(stage);
                }
            }

            newMap.avgStars = 0;
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