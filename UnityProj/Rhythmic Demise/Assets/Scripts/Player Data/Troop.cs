using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class Troop
{

    public Enums.JobType job;
    public int level, energyNeeded;
    public int currentHealth, maxHealth, damage;
    public int armor;
    public List<Skills> skills;        //3

    public float expToLevel, currentExp;

    public void IncreaseExpNeeded()
    {
        expToLevel *= 1.4f;
    }

    public void LevelUp()
    {
        currentExp += PlayerScript.playerdata.totalResource - expToLevel;
        PlayerScript.playerdata.totalResource -= (int)expToLevel;
        IncreaseExpNeeded();
        StatLevelUp();
        SkillLevelUp();
    }


    void StatLevelUp()
    {
        maxHealth += 15;
        damage += 5;
        armor += 2;
        energyNeeded *= 2;
    }

    void SkillLevelUp()
    {
        for (int i = 0; i < skills.Count; i++)
            skills[i].LevelUp();
    }
}
