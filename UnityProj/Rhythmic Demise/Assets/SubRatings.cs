using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SubRatings : MonoBehaviour {
    MainMap currentMap;

    Vector3 firstPosDiff, secondPosDiff, thirdPosDiff;
    GameObject firstIcon, secondIcon, thirdIcon;
    bool threeMaps;

    Vector3 smallScale;
    GameObject emptyStar, fullStar, theLock, cloneStar;
    List<GameObject> cloneStars;

    void Start()
    { 
       switch (PlayerScript.playerdata.clickedMap)
         {
             case Enums.MainMap.Larnyx:
             case Enums.MainMap.Liver:
                 threeMaps = false;
                 break;
             default:
                 threeMaps = true;
                 break;
         }
        Init();
        InitStars();
        Scale();
    }

    void InitPos()
    {
        //for debugging purpose
        firstPosDiff = new Vector3(0.84f, 0.47f);
        secondPosDiff = new Vector3(0.03f, 0.78f);
        thirdPosDiff = new Vector3(-0.82f, 0.47f);
    }

    void Init()
    {
        print("get current map");
        InitPos();
        currentMap = PlayerScript.playerdata.mapProgress[(int)PlayerScript.playerdata.clickedMap];
        cloneStars = new List<GameObject>();
        firstIcon = GameObject.Find("FirstStage");

        if (threeMaps)
        {
            secondIcon = GameObject.Find("SecondStage");
            thirdIcon = GameObject.Find("LastStage");
        }
        else
        {
            secondIcon = GameObject.Find("LastStage");
        }

        smallScale = new Vector3(0.3f, 0.3f);
        emptyStar = Resources.Load("Prefabs/EmptyStar") as GameObject;
        fullStar = Resources.Load("Prefabs/FullStar") as GameObject;
        theLock = Resources.Load("Prefabs/Lock") as GameObject;
        print(theLock);
    }

    int GetStars(int stage)
    {
        int starCount = 0;
        
        if (currentMap.stages[stage - 1].topComboCount < 0)
            return -1;

        for(int i = 0; i < currentMap.stages[stage - 1].comboRange.Count; i++)
        {
            if (currentMap.stages[stage - 1].topComboCount > currentMap.stages[stage - 1].comboRange[i])
                starCount++;
        }

        return starCount;
    }

    void SetStars(int stage)
    {
        GameObject icon;
        if (stage == 1)
            icon = firstIcon;
        else if (stage == 2)
            icon = secondIcon;
        else 
            icon = thirdIcon;
        switch (GetStars(stage))
        {
            case 1:
                cloneStar = Instantiate(fullStar, GetPos(icon.transform.position, firstPosDiff), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                cloneStars.Add(cloneStar);
                cloneStar = Instantiate(emptyStar, GetPos(icon.transform.position, secondPosDiff), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                cloneStars.Add(cloneStar);
                cloneStar = Instantiate(emptyStar, GetPos(icon.transform.position, thirdPosDiff), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                cloneStars.Add(cloneStar);
                break;
            case 2:
                cloneStar = Instantiate(fullStar, GetPos(icon.transform.position, firstPosDiff), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                cloneStars.Add(cloneStar);
                cloneStar = Instantiate(fullStar, GetPos(icon.transform.position, secondPosDiff), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                cloneStars.Add(cloneStar);
                cloneStar = Instantiate(emptyStar, GetPos(icon.transform.position, thirdPosDiff), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                cloneStars.Add(cloneStar);
                break;
            case 3:
                cloneStar = Instantiate(fullStar, GetPos(icon.transform.position, firstPosDiff), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                cloneStars.Add(cloneStar);
                cloneStar = Instantiate(fullStar, GetPos(icon.transform.position, secondPosDiff), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                cloneStars.Add(cloneStar);
                cloneStar = Instantiate(fullStar, GetPos(icon.transform.position, thirdPosDiff), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                cloneStars.Add(cloneStar);
                break;
            default:
                if (currentMap.stages[stage - 1].topComboCount < 0)
                {
                    //still locked
                    cloneStar = Instantiate(theLock, GetPos(icon.transform.position, secondPosDiff), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                    cloneStars.Add(cloneStar);
                }
                else
                {
                    //unlocked but not played yet
                    cloneStar = Instantiate(emptyStar, GetPos(icon.transform.position, firstPosDiff), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                    cloneStars.Add(cloneStar);
                    cloneStar = Instantiate(emptyStar, GetPos(icon.transform.position, secondPosDiff), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                    cloneStars.Add(cloneStar);
                    cloneStar = Instantiate(emptyStar, GetPos(icon.transform.position, thirdPosDiff), Quaternion.Euler(0f, 0f, 0f)) as GameObject;
                    cloneStars.Add(cloneStar);
                }
                break;
        }
    }

    void InitStars()
    {
        if (PlayerScript.playerdata.clickedMap == Enums.MainMap.Larnyx || PlayerScript.playerdata.clickedMap == Enums.MainMap.Liver ||
            PlayerScript.playerdata.clickedMap == Enums.MainMap.SIntes)
        {
            for (int i = 0; i < 2; i++)
            {
                SetStars(i + 1);
            }
        }
        else
        {

            for (int i = 0; i < 3; i++)
            {
                SetStars(i + 1);
            }
        }
    }

    Vector3 GetPos(Vector3 iconPos, Vector3 diffPos)
    {
        float x = iconPos.x - diffPos.x;
        float y = iconPos.y - diffPos.y;
        
        return new Vector3(x, y, 0f);
    }

    void Scale()
    {
        foreach(GameObject star in cloneStars)
        {
            star.transform.localScale = smallScale;
        }
    }
}
