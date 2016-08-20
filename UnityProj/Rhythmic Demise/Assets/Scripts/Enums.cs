using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class Enums : MonoBehaviour {

    public const int TROOPSELECTED = 3;
    [System.Serializable]
    public enum CharacterType {None, Cancer, Diabetic };
    [System.Serializable]
    public enum PlayerState { Idle, MoveUp, MoveDown, MoveLeft, MoveRight, Attack, Skill };

    [System.Serializable]
    public enum JobType {None, Knight, Archer, Priest}

    [System.Serializable]
    public enum SkillName
    {
        None, 

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

    [System.Serializable]
    public enum MainMap
    {
        Mouth, Larnyx, Trachea, Lung, Heart, Liver, Spleen, Pancreas,
        Kidney, LIntes, SIntes, Brain
    }
    public const int MAINMAPCOUNT = 17;
    public const int MOUTHSTAGE = 3;

    public static string[] MapName =
    {
        "Mouth", "Larnyx", "Trachea", "Lungs", "Heart", "Liver",
        "Spleen", "Pancreas", "Kidney", "Lintes","Sintes", "Brain"
    };

    public static string[] StageName =
    {
        "MouthStage", "LarnyxStage", "TracheaStage", "LungStage", "HeartStage", 
        "LiverStage", "SpleenStage", "PancreasStage", "KidneyStage", 
        "LargeIntesStage", "SmallIntesStage", "BrainStage"
    };
}
