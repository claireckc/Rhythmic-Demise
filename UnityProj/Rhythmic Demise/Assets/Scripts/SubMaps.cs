using UnityEngine;
using System.Collections;
using UnityEngine.Serialization;

[System.Serializable]
public class SubMaps : MonoBehaviour {

    public Enums.MainMap parentMap;
    public int mapId;          //numbered. parentMap + mapId = unique identifier for submap

    public int topComboCount;  //to determine the stars and awarding resource
    public int resourceAttained;
    public int[] comboRange;   //determine the number of stars for that stage
    public int stars;
    
    
    public void DetermineStars()
    {
        stars = 0;
        //update the number of stars using range
        if (topComboCount > comboRange[0])
            stars++;

        if (topComboCount > comboRange[1])
            stars++;

        if (topComboCount > comboRange[2])
            stars++;
    }
}
