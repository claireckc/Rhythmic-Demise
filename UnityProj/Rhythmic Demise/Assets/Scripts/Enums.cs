﻿using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

[System.Serializable]
public class Enums : MonoBehaviour {
    public enum CharacterType { Cancer, Diabetic };
    public enum PlayerState { Idle, MoveUp, MoveDown, MoveLeft, MoveRight, Attack, Heal, Skill };

    public enum JobType { Knight, Archer, Priest}

    public enum SkillName
    {
        KnightHigh,
        KnightDefbuff,
        KnightCharge,

        ArcherAOE,
        ArcherAtkBuff,
        ArcherHigh,

        PriestHealBuff,
        PriestHex,
        PriestCurse
    }
}
