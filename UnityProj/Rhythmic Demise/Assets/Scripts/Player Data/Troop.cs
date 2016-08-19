using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class Troop {

    public Enums.JobType job;
    public int level, energyNeeded;
    public int currentHealth, maxHealth, damage; 
    public int armor;
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
            CalculateExpNeeded();
        }
    }
}
