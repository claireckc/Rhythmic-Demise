using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

[System.Serializable]
public class MainMap : MonoBehaviour{
    public Enums.MainMap mapName;
    public SubMaps[] stages;
    public int stars;
    public bool isComplete;

    public void DetermineStars()
    {
        int totalStars = 0;
        stars = 0;
        
        foreach (SubMaps sm in stages)
            totalStars += sm.stars;

        stars = totalStars / stages.Length;
    }
    
}