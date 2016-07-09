using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

[System.Serializable]
public class Skills {

    public Enums.SkillName skillName;
    public float skillValue;
    public int skillLevel;      //0 means locked, 1 and above means unlocked
}
