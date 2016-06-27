using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Troops : MonoBehaviour {

    [System.Serializable]
    public struct Stats
    {
       public float currentHealth, maxHealth, attack;
       public float defenseRating;

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

    public void SetStats(float cHealth, float maxHealth, float atk, float def)
    {
        stats.currentHealth = cHealth;
        stats.maxHealth = maxHealth;
        stats.attack = atk;
        stats.defenseRating = def;
    }

    public void SetInfo(Enums.CharacterType cType, Enums.JobType jType, int level, int resource)
    {
        charType = cType;
        job = jType;
        this.level = level;
        resourceNeeded = resource; 
    }

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
