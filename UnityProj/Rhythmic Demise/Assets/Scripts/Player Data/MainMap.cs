using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;
using System.Collections.Generic;

[System.Serializable]
public class MainMap {

    public Enums.MainMap mapName;
    public List<SubMap> stages;
    public int avgStars;
    public bool isComplete;
    public bool isLocked;
}
