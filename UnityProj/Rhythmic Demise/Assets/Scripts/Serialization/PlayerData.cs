using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Serialization;

[System.Serializable]
public class PlayerData : MonoBehaviour {

    public static PlayerData playerdata;

    [System.Serializable] public struct Troop
    {
        public Enums.JobType job;
        public int level;
        public float currentHealth, maxHealth, attack, defenseRating;
        public List<Skills> skills;        //3
    }

    [System.Serializable] public struct Skills
    {
        public Enums.SkillName skillName;
        public float skillValue;
        public int skillLevel;      //0 means locked, 1 and above means unlocked
    }

    [System.Serializable]public struct TroopSelected
    {
        public Troop troop;
        public int count;
    }

    [System.Serializable] public struct SubMap
    {
        public Enums.MainMap parentMap;
        public int mapId;

        public int topComboCount, resourceAttained, stars;
        public List<int> comboRange;        //size 3
    }

    [System.Serializable] public struct MainMap
    {
        public Enums.MainMap mapName;
        public List<SubMap> stages;
        public int avgStars;
        public bool isComplete;
    }
    //Resources
    public int totalResource, totalEnergy;

    //Troops information
    public Enums.JobType leaderType;
    public Enums.SkillName skillSelected;
    public Enums.CharacterType pathogenType;
    public List<Troop> troopData;       //first is knight then archer and then priest, to store the stats and the level
    public List<TroopSelected> troopSelected;

    //Map progress
    public List<MainMap> mapProgress;

    //settings
    public float globalVolume, effectsVolume, buttonAlpha;

    public void Init()
    {
        leaderType = Enums.JobType.None;
        skillSelected = Enums.SkillName.None;
        pathogenType = Enums.CharacterType.None;
        globalVolume = effectsVolume = buttonAlpha = 1.0f;
        totalResource = 5;
        totalEnergy = 0;

        //for troop data
        for(int i = 0; i < 3; i++)
        {
            Troop tp = new Troop();
            tp.skills = new List<Skills>();
            tp.job = (Enums.JobType)i + 1;
            if (i == 0)
                tp.level = i+1;     //first unlock would be the knight
            else
                tp.level = 0;

            //MICHAEL SET MAX HEALTH, ATTACK, DEFENSERATING HERE, THIS IS INITIALIZATION
            for(int j = 0; j < 3; j++)
            {
                Skills sk = new Skills();
                switch (i)
                {
                    //if knight
                    case 0:
                        sk.skillName = (Enums.SkillName)j + 1;
                        break;
                    //if archer
                    case 1: sk.skillName = (Enums.SkillName)j + 4;
                        break;
                    //if priest
                    case 2: sk.skillName = (Enums.SkillName)j + 7;
                        break;
                }
                if(i == 0)
                {
                    sk.skillLevel = 1;
                    /*sk.skillValue = sk.skilllevel * SOMETHING */    //MICHAEL, SET SKILL VALUE HERE, THIS IS INITIALIZATION! KNIGHT SKILL VALUE
                }
                else
                {
                    sk.skillLevel = 0;
                    sk.skillValue = 0;
                }

                tp.skills.Add(sk);
                troopData.Add(tp);
            }
        }

        //for troop selected
        for(int i = 0; i < 3; i++)
        {
            TroopSelected tp = new TroopSelected();
            tp.troop = new Troop();
            tp.count = 0;
            troopSelected.Add(tp);
        }

        //for map
        for(int i = 0; i < Enums.MAINMAPCOUNT; i++)
        {
            MainMap newMap = new MainMap();
            newMap.mapName = (Enums.MainMap)i;

            //initialize the number of substages, when everything is confirmed
            newMap.stages = new List<SubMap>();
            if(i == 0) //tutorial stage
            {
                for (int k = 0; k < 3; k++) {

                    SubMap stage = new SubMap();
                    stage.parentMap = newMap.mapName;
                    stage.mapId = k;
                    stage.topComboCount = stage.resourceAttained = stage.stars = 0;
                    stage.comboRange = new List<int>();
                    for (int j = 0; j < 3; j++)
                        stage.comboRange.Add(0);
                }
            }
            newMap.avgStars = 0;
            newMap.isComplete = false;
            mapProgress.Add(newMap);

        }
    }

    /*public void SaveData()
    {
        SaveLoadManager.SaveAllInformation(playerdata);
    }

    public void LoadData()
    {
        PlayerData pdNew = SaveLoadManager.LoadInformation();
        playerdata = pdNew;
    }*/
    public void Awake()
    {
        Init();
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
    }
}
