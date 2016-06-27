using UnityEngine;
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

    public void NewMap(int intMap)
    {
        mapName = (Enums.MainMap)intMap;
        stars = 0;
        isComplete = false;

        //set the number of substage in each map
        switch (mapName)
        {
            case Enums.MainMap.Mouth:
                stages = new SubMaps[Enums.MOUTHSTAGE];
                break;

            case Enums.MainMap.Larnyx:
                break;

            case Enums.MainMap.Trachea:
                break;

            case Enums.MainMap.Esophagus:
                break;

            case Enums.MainMap.Lung:
                break;

            case Enums.MainMap.Heart:
                break;

            case Enums.MainMap.Liver:
                break;

            case Enums.MainMap.Spleen:
                break;

            case Enums.MainMap.Pancrease:
                break;

            case Enums.MainMap.Stomach:
                break;

            case Enums.MainMap.Gallbladder:
                break;

            case Enums.MainMap.LKidney:
                break;

            case Enums.MainMap.RKidney:
                break;

            case Enums.MainMap.LIntes:
                break;

            case Enums.MainMap.SIntes:
                break;

            case Enums.MainMap.SpinalCord:
                break;

            case Enums.MainMap.Brain:
                break;

        }
    }
    
}