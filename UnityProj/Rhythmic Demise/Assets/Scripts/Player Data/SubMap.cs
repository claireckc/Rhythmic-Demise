using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class SubMap {

    public Enums.MainMap parentMap;
    public int mapId;

    public int topComboCount, resourceAttained, stars;
    public List<int> comboRange;        //size 3
    public bool isComplete, isCurrent;
}
