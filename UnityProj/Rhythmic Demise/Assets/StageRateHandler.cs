using UnityEngine;
using System.Collections;

public class StageRateHandler : MonoBehaviour {

    private GameObject fullStar, halfStar, emptyStar;
    public GameObject firstStarPos, secondStarPos, thirdStarPos;
    public GameObject locked;
    public float MAXSTARS = 3.0f;

    public void Start()
    {
       
        fullStar = Resources.Load<GameObject>("Prefabs/FullStar");
        halfStar = Resources.Load<GameObject>("Prefabs/HalfStar");
        emptyStar = Resources.Load<GameObject>("Prefabs/EmptyStar");
    }

    public int InstantiateFullStar(float stageStars)
    {
        int position = 0;
        for (int i = 0; i < stageStars; i++)
        {
            if (i == 0)
            {
                Instantiate(fullStar, firstStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                position++;
            }
            else if (i == 1)
            {
                Instantiate(fullStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                position++;

            }
            else if (i == 2)
            {
                Instantiate(fullStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                position++;
            }
        }
        return position;    //return 3 is all full stars
    }
    
    public void Init(Enums.MainMap mapName)
    {
        float stageStars = GetMainStars(mapName);
        if (stageStars > 0f)
        {
            if(stageStars % 1 == 0)
            {
                //whole number stars
               int position = InstantiateFullStar(stageStars);
                if(position < 3)
                {
                    switch (position)
                    {
                        case 1: Instantiate(emptyStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            Instantiate(emptyStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            break;
                        case 2: Instantiate(emptyStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            break;
                        default: Instantiate(emptyStar, firstStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            Instantiate(emptyStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            Instantiate(emptyStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            break; 
                    }
                }
            }
            else
            {
                //with a 0.5f
                int position = InstantiateFullStar(stageStars);
                if(position < 3.0f)
                {
                    switch (position)
                    {
                        case 0: Instantiate(halfStar, firstStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                                Instantiate(emptyStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                                Instantiate(emptyStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            break;
                        case 1:
                            Instantiate(halfStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            Instantiate(emptyStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            break;
                        case 2:
                            Instantiate(halfStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            break;
                        default:
                            Instantiate(emptyStar, firstStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            Instantiate(emptyStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            Instantiate(emptyStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f));
                            break;
                    }

                }
                
            }
        }
        else
        {
            //check if locked or poor score
        }
        print("Total map " + GetMainStars(mapName));
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
        Debug.Log("Submap: " + PlayerScript.playerdata.mapProgress[(int)mapName].stages[index].stars);
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
