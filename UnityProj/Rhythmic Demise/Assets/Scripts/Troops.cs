using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

[System.Serializable]
public class Troops : MonoBehaviour {

    public struct Stats
    {
        float currentHealth, maxHealth, attack;
        float defenseRating;

        /*NOTE: DefenseRating is in a form of %. Damage taken by the unit should be:
            multiplier = towerLevel / troopLevel;
            towerDamage = multiplier * TowerAttack;
            damageToTroop = 
        */
    }

    public Enums.CharacterType charType;
    public Enums.JobType job;
    public int level;      //0 if locked.
    public int resourceNeeded; //resource(suagr or carbon) needed to upgrade level, relative to level
    public Skills[] skills;
    public Stats stats;

    public void CalculateResource()
    {
        //use level to update the resource needed(int resourceNeeded) to upgrade the unit
    }

    public void IncreaseStats()
    {
        if (charType == Enums.CharacterType.Cancer)
        {

        }
        else if (charType == Enums.CharacterType.Diabetic)
        {

        }
    }
    public bool LevelUp(int playerResource)
    {
        CalculateResource();
        if (playerResource > resourceNeeded)
        {
            //upgrade the stats
            foreach(Skills sk in skills)
            {
                sk.levelUp(charType);
            }
            IncreaseStats();
            //save gameinformation before returning
            return true;
        }
        else
            return false;       //print message to player that they cannot level up
    }

    
}
