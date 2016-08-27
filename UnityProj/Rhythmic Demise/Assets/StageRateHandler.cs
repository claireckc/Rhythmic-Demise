using UnityEngine;
using System.Collections;

public class StageRateHandler : MonoBehaviour {

    const int RATE = 3;
    private GameObject fullStar, halfStar, emptyStar, nothing, locked;
    public GameObject firstStarPos, secondStarPos, thirdStarPos;
    public float MAXSTARS = 3.0f;
    private GameObject[] RatingObjectArray;

    public void Start()
    {
       
        fullStar = Resources.Load<GameObject>("Prefabs/FullStar");
        halfStar = Resources.Load<GameObject>("Prefabs/HalfStar");
        emptyStar = Resources.Load<GameObject>("Prefabs/EmptyStar");
        nothing = Resources.Load<GameObject>("Prefabs/EmptySprite");
        locked = Resources.Load<GameObject>("Prefabs/Lock");

        RatingObjectArray = new GameObject[RATE];
    }

    public int InstantiateFullStar(float stageStars)
    {
        //position is the last position that has a full star
        int position = 0;
        for (int i = 0; i < stageStars; i++)
        {
            if (i == 0)
            {
                RatingObjectArray[0] = Instantiate(fullStar, firstStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                position++;
            }
            else if (i == 1)
            {
                RatingObjectArray[1] = Instantiate(fullStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                position++;

            }
            else if (i == 2)
            {
                RatingObjectArray[2] = Instantiate(fullStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                position++;
            }
        }
        return position;    //return 3 is all full stars
    }

    public void ClearIcons()
    {
        for(int i = 0; i < RATE; i++)
        {
            Destroy(RatingObjectArray[i]);
        }

    }
    
    public void Init(Enums.MainMap mapName)
    {
        ClearIcons();
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
                        case 1:
                            RatingObjectArray[1] = Instantiate(emptyStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            RatingObjectArray[2] = Instantiate(emptyStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            break;
                        case 2:
                            RatingObjectArray[2] = Instantiate(emptyStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            break;
                        default:
                            RatingObjectArray[0] = Instantiate(emptyStar, firstStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            RatingObjectArray[1] = Instantiate(emptyStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            RatingObjectArray[2] = Instantiate(emptyStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
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
                        case 0:
                            RatingObjectArray[0] = Instantiate(halfStar, firstStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            RatingObjectArray[1] = Instantiate(emptyStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            RatingObjectArray[2] = Instantiate(emptyStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            break;
                        case 1:
                            RatingObjectArray[0] = Instantiate(halfStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            RatingObjectArray[1] = Instantiate(emptyStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            break;
                        case 2:
                            RatingObjectArray[0] = Instantiate(halfStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            break;
                        default:
                            RatingObjectArray[0] = Instantiate(emptyStar, firstStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            RatingObjectArray[1] = Instantiate(emptyStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            RatingObjectArray[2] = Instantiate(emptyStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                            break;
                    }

                }
                
            }
        }
        else
        {
            for(int i =0; i < PlayerScript.playerdata.mapProgress.Count; i++)
            {
                if(mapName == PlayerScript.playerdata.mapProgress[i].mapName)
                {
                    if (PlayerScript.playerdata.mapProgress[i].isLocked)
                    {
                        RatingObjectArray[0] = Instantiate(nothing, firstStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                        RatingObjectArray[1] = Instantiate(locked, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                        RatingObjectArray[2] = Instantiate(nothing, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                    }
                    else
                    {
                        RatingObjectArray[0] = Instantiate(emptyStar, firstStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                        RatingObjectArray[1] = Instantiate(emptyStar, secondStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                        RatingObjectArray[2] = Instantiate(emptyStar, thirdStarPos.transform.position, Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                    }
                }
            }
        }
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
