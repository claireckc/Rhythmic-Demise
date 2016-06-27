using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.Serialization;

[System.Serializable]
public class PlayerData : MonoBehaviour {
    public static PlayerData playerdata;
    [System.Serializable] public struct Troop
    {
        public float currentHealth, maxHealth, attack, defenseRating;
    }

    [System.Serializable]public struct TroopSelected
    {
        public Troop troops;
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
    
    public void Start()
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
}
