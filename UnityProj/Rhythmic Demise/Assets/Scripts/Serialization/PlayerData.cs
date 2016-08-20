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
    public float attackMultiplier;
    public float defenseMultiplier;
    public float expMultiplier;     //base on stage number and player unit level

    //Map progress
    public List<MainMap> mapProgress;

    //settings
    public float globalVolume, effectsVolume, buttonAlpha;

    //for tutorial
    public bool firstTut1, firstTut2, firstTut3, firstResource;

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
        leaderType = Enums.JobType.None;
        skillSelected = Enums.SkillName.None;
        pathogenType = Enums.CharacterType.None;
        globalVolume = effectsVolume = buttonAlpha = 1.0f;
        totalResource = 5;
        totalEnergy = 5;
        troopData = new List<Troop>();
        troopSelected = new List<TroopSelected>();

        attackMultiplier = 1f;
        defenseMultiplier = 1f;
        expMultiplier = 1f;

        firstTut1 = firstTut2 = firstTut3 = firstResource = true;

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
}