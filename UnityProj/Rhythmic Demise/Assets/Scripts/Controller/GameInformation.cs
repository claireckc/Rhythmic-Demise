using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class GameInformation : MonoBehaviour {
    const int RANGENUMBER = 3;
    static GameInformation gameInfo;    //static is not serialized, this only for loading data

    struct PlayerSave
    {
        /*
        skills
            -> skill name
            -> skill level
            -> damage or buff value

        troops(array of 3[troopSelected])
            -> selected troop type
            -> previously selected troops with count

        other
            -> total coins
            -> saved volume (sfx and background)
            -> button transparency alpha value
            -> current resource(sugar or carbon)
            -> troop leader
            -> current skill selected
            -> map that is completed
        */

        int totalCoins;
        int totalResource; //to summon
        int totalEnergy;    // to upgrade troops and skills
    }
    
    struct TroopSelected
    {
        Troops troop;
        int troopCount;
    }

    struct Substage
    {
        int id;
        int score;
        int coinsAttained;
        int topComboCount;
        int[] stageScoreRange;  //to determine the number of stars for that completed level
    }

    struct Mainstage
    {
        string stageName;
        Substage[] substages;
    }

    void Awake()
    {

    }

    void Start()
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
    
    
}
