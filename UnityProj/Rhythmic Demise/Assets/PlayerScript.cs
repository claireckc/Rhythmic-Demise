using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerScript : MonoBehaviour {

    public static PlayerData playerdata;

    private Troop tp;
    private Skills sk;
    private MainMap newMap;
    private TroopSelected ts;
    private SubMap stage;

    public void Init()  
    {
        playerdata.leaderType = Enums.JobType.None;
        playerdata.skillSelected = Enums.SkillName.None;
        playerdata.pathogenType = Enums.CharacterType.None;
        playerdata.globalVolume = playerdata.effectsVolume = playerdata.buttonAlpha = 1.0f;
        playerdata.totalResource = 5;
        playerdata.totalEnergy = 0;
        playerdata.troopData = new List<Troop>();
        playerdata.troopSelected = new List<TroopSelected>();

        //for troop data
        for(int i = 0; i < 3; i++)
        {
            tp = new Troop();
            tp.skills = new List<Skills>();
            tp.job = (Enums.JobType)i + 1;
            if (i == 0)
                tp.level = i+1;     //first unlock would be the knight
            else
                tp.level = 0;

            //MICHAEL SET MAX HEALTH, ATTACK, DEFENSERATING HERE, THIS IS INITIALIZATION
            for(int j = 0; j < 3; j++)
            {
                sk = new Skills();
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
                    /*sk.skillValue = sk.skilllevel * SOMETHING    //MICHAEL, SET SKILL VALUE HERE, THIS IS INITIALIZATION! KNIGHT SKILL VALUE*/
                }
                else
                {
                    sk.skillLevel = 0;
                    sk.skillValue = 0;
                }

                tp.skills.Add(sk);
            }
           
            playerdata.troopData.Add(tp);
        }

        //for troop selected
        for(int i = 0; i < 3; i++)
        {
            ts = new TroopSelected();
            ts.troop = new Troop();
            ts.count = 0;
            playerdata.troopSelected.Add(ts);
        }

        playerdata.mapProgress = new List<MainMap>();
        //for map
        for(int i = 0; i < Enums.MAINMAPCOUNT; i++)
        {
            newMap = new MainMap();
            newMap.mapName = (Enums.MainMap)i;

            //initialize the number of substages, when everything is confirmed
            newMap.stages = new List<SubMap>();
            if(i == 0) //tutorial stage
            {
                for (int k = 0; k < 3; k++) {

                    stage = new SubMap();
                    stage.parentMap = newMap.mapName;
                    stage.mapId = k;
                    stage.topComboCount = stage.resourceAttained = stage.stars = 0;
                    stage.comboRange = new List<int>();
                    for (int j = 0; j < 3; j++)
                        stage.comboRange.Add(0);
                    stage.isComplete = false;
                    stage.isCurrent = false;
                    newMap.stages.Add(stage);
                }
            }
            newMap.avgStars = 0;
            newMap.isComplete = false;
            playerdata.mapProgress.Add(newMap);

        }
    }

    public void Awake()
    {
        if (playerdata != null)
            Destroy(gameObject);
        else
        {
            print("Null data");
            DontDestroyOnLoad(gameObject);
            SaveLoadManager.LoadInformation();
            print(PlayerScript.playerdata.pathogenType);
            if (playerdata == null)
                print("NUll still");  playerdata = new PlayerData();
        }

    }
    public void Start()
    {
        Init();
    }
}
