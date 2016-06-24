using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class GameInformation : MonoBehaviour {
    public const int RANGENUMBER = 3;
    public static GameInformation gameInfo;    //static is not serialized, this only for loading data

    public struct TroopSelected
    {
        Troops troop;
        int troopCount;
    }

    public struct PlayerSave
    {
        /*****************************Reource****************************/  
        int totalResource; //to level up(suagr or carbon)
        int totalEnergy;    //to summon, map progress

        /*****************************Troop information****************************/
        Enums.JobType leaderType;
        Enums.SkillName skillSelected;      //skill is checked with leaderType and skillSelected type
        Enums.CharacterType chosenUnit; //cancer or diabetic unit, initialize all troops characterType
        TroopSelected[] troopSelected;  //array of 3

        /*****************************Map****************************/
        MainMap[] mapProgress;  //contains all the map information


        /*****************************Settings****************************/
        float globalVol, sfxVol;
        float buttonAlpha;
        
    }

    public void Awake()
    {
        //begin loading from loadinformation script
    }

    public void Start()
    {
        //game information is a singleton
        if (gameInfo != null)
            Destroy(gameObject);
        else
        {
            DontDestroyOnLoad(gameObject);
            gameInfo = this;
        }
    }

    public void Update()
    {

    }
    
    
}
