using UnityEngine;
using System.Collections;

public class Skills : MonoBehaviour {

    public Enums.SkillName skillName;
    public int skillLevel; //0 if not unlocked, 1 if unlock and can be chosen
    public float skillValue;   //depending on the skill type and skill level, this value can be damage or buff value. Generic.
    

    public void levelUp(Enums.CharacterType type)
    {
        //called when the troop is leveled up. Change skill level and skillValue here, relative to the skill name
        if(type == Enums.CharacterType.Cancer)
        {
            switch (skillName)
            {
                case Enums.SkillName.KnightHigh:
                    break;
                case Enums.SkillName.KnightDefbuff:
                    break;
                case Enums.SkillName.KnightCharge:
                    break;
                case Enums.SkillName.ArcherAOE:
                    break;
                case Enums.SkillName.ArcherAtkBuff:
                    break;
                case Enums.SkillName.ArcherHigh:
                    break;
                case Enums.SkillName.PriestHealBuff:
                    break;
                case Enums.SkillName.PriestHex:
                    break;
                case Enums.SkillName.PriestCurse:
                    break;
            }
        }
        else if(type == Enums.CharacterType.Diabetic)
        {
            switch (skillName)
            {
                case Enums.SkillName.KnightHigh:
                    break;
                case Enums.SkillName.KnightDefbuff:
                    break;
                case Enums.SkillName.KnightCharge:
                    break;
                case Enums.SkillName.ArcherAOE:
                    break;
                case Enums.SkillName.ArcherAtkBuff:
                    break;
                case Enums.SkillName.ArcherHigh:
                    break;
                case Enums.SkillName.PriestHealBuff:
                    break;
                case Enums.SkillName.PriestHex:
                    break;
                case Enums.SkillName.PriestCurse:
                    break;
            }
        }
       //different character type will have different skill boost.
    }
}
