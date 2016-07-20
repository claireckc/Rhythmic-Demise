using UnityEngine;
using System.Collections;

public class StageRateHandler : MonoBehaviour {

    private GameObject fullStar, halfStar, emptyStar;
    public GameObject locked;

    public void Start()
    {
        fullStar = Resources.Load<GameObject>("Prefabs/FullStar");
        halfStar = Resources.Load<GameObject>("Prefabs/HalfStar");
        emptyStar = Resources.Load<GameObject>("Prefabs/EmptyStar");
    }
    
    public void Init(Enums.MainMap mapName)
    {
        print(GetMainStars(mapName));
    }

    public float GetMainStars(Enums.MainMap mapName)
    {
        float totalStars = 0f;
        for(int i = 0; i < PlayerScript.playerdata.mapProgress[(int)mapName].stages.Count; i++)
        {
            totalStars += GetSubStars(mapName, i);
        }

        float avgStars = totalStars / PlayerScript.playerdata.mapProgress[(int)mapName].stages.Count;
        avgStars = RoundOff(avgStars);
        return avgStars;
    }

    public float GetSubStars(Enums.MainMap mapName, int index)
    {
        return PlayerScript.playerdata.mapProgress[(int)mapName].stages[index].stars;
    }

    public float RoundOff(float dec)
    {
        if (dec % 1 == 0)
            return dec;  //whole number
        else if (dec % 1 >= 0.5f)
            return dec - (dec % 1) + 0.5f;  //to 0.5
        else
            return dec - (dec % 1);     //round down
    }
}
