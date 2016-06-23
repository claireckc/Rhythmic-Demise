using UnityEngine;
using System.Collections;

public class Skills : MonoBehaviour {

    Enums.SkillName skillName;
    int skillLevel; //0 if not unlocked, 1 if unlock and can be chosen
    float skillValue;   //depending on the skill type and skill level, this value can be damage or buff value. Generic.
    int toUpgrade;  //energy needed to upgrade skill level, relative to skill level
}
