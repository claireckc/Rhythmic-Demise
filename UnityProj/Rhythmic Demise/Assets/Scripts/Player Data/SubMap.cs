using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Serialization;

[System.Serializable]
public class SubMap {

    public Enums.MainMap parentMap;
    public int mapId;

    public float stars;

    public int topComboCount, resourceAttained;
    public List<int> comboRange;        //size 3

    public bool IsComplete()
    {
        return (resourceAttained >= 0);
    }

    public bool isCurrent(int clicked)
    {
        return (clicked == (mapId + 1));
    }
}
