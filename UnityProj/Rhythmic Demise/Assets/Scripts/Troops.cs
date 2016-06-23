using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

[System.Serializable]
public class Troops : MonoBehaviour {
    
    struct Stats
    {
        int attack, health;
        float defenseRating;
        /*NOTE: DefenseRating is in a form of %. Damage taken by the unit should be:
            multiplier = towerLevel / troopLevel;
            towerDamage = multiplier * TowerAttack;
            damageToTroop = 
        */
    }

    Enums.CharacterType charType;
    Enums.JobType job;
    int level;      //0 if locked.
    int resourceNeeded; //resource(suagr or carbon) needed to upgrade level, relative to level
    Skills[] skills;
    Stats stats;

    public void CalculateResource()
    {
        //use level to update the resource needed to upgrade the unit
    }

    public void CalculateStats()
    {
        //always call this when unit has leveled up to update the stats of unit
        
    }

    public bool AbleToLevel(int playerResource)
    {
        CalculateResource();
        if (playerResource > resourceNeeded)
            return true;
        else
            return false;
    }

    public bool LevelUp(int playerResource)
    {
        CalculateResource();
        if (playerResource > resourceNeeded)
        {
            //upgrade the stats
            return true;
        }
        else
            return false;       //print message to player that they cannot level up
    }
}
