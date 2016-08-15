using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class Troop {

    public Enums.JobType job;
    public int level, energyNeeded;
    public float currentHealth, maxHealth, attack, defenseRating;
    public List<Skills> skills;        //3

    public float expToLevel;

    public void CalculateExpNeeded()
    {
        expToLevel = level * 1.4f;
    }

    public void LevelUp(float experienceGained)
    {
        //update experience needed
        CalculateExpNeeded();
        while(experienceGained >= expToLevel)
        {
            experienceGained -= expToLevel;
            level++;
            StatLevelUp();
            SkillLevelUp();
            CalculateExpNeeded();
        }
    }

    void StatLevelUp()
    {
        maxHealth *= 1.3f;
        attack *= 1.2f;
        defenseRating *= 1.3f;
    }
    
    void SkillLevelUp()
    {
        for(int i = 0; i < skills.Count; i++)
            skills[i].LevelUp();
    }
}
