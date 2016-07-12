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
}
