using UnityEngine;
using System.Collections;

public class MouthEventHandler : MonoBehaviour
{

    public RuntimePlatform platform;
    public Ray ray;
    public RaycastHit2D rayHit;
    private GameObject obj;

    // Use this for initialization
    void Start()
    {

        platform = Application.platform;
    }

    // Update is called once per frame
    void Update()
    {

        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    DetermineTouchPosition(Input.GetTouch(0).position);
                }
            }
        }
		else if (platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButtonDown(0))
                DetermineTouchPosition(Input.mousePosition);
        }
    }

    public void EnterLevel(string levelTag)
    {
        char lastChar = levelTag[levelTag.Length - 1];
        int stageNumber = lastChar - '0';

        //ensure that the stage number received is within limit of the main map substage
        if(PlayerScript.playerdata.mapProgress[(int)PlayerScript.playerdata.clickedMap].stages.Count >= stageNumber)
        {
            if (stageNumber == 1 || CheckAccess(PlayerScript.playerdata.clickedMap, stageNumber))
            {
                PlayerScript.playerdata.clickedStageNumber = stageNumber;
                Application.LoadLevel("Resource Management");
            }
            //update the click stage number and enter rsrc mgnt
        }
    }

    public void DetermineTouchPosition(Vector2 touchPosition)
    {
        char one = '1';
        int _one = one - '0';
        print("Ascii: " + _one);
        ray = Camera.main.ScreenPointToRay(touchPosition);
        rayHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << 15);
        if (rayHit.collider != null)
        {
            EnterLevel(rayHit.collider.gameObject.tag);
           /* switch (rayHit.collider.gameObject.tag)
            {
                case "Mouth_1":
                    //go into troop selection screen
                    PlayerScript.playerdata.clickedStageNumber = 1;
                    Application.LoadLevel("Resource Management");
                    break;
                case "Mouth_2":
                    //if (PlayerScript.playerdata.mapProgress[0].stages[0].topComboCount >= 0 || !PlayerScript.playerdata.mapProgress[0].stages[0].IsComplete())
                    if(CheckAccess(Enums.MainMap.Mouth, 2))
                    {
                        PlayerScript.playerdata.clickedStageNumber = 2;
                        Application.LoadLevel("Resource Management");
                    }
                    break;

                case "Mouth_3":
                    //if (PlayerScript.playerdata.mapProgress[0].stages[1].topComboCount >= 0 || !PlayerScript.playerdata.mapProgress[0].stages[1].IsComplete())
                    if(CheckAccess(Enums.MainMap.Mouth, 3))
                    {
                        PlayerScript.playerdata.clickedStageNumber = 3;
                        Application.LoadLevel("Resource Management");
                    }
                    break;
            }*/

        }
    }

    public bool CheckAccess(Enums.MainMap mapname, int StageNumber)
    {
        for(int i = 0; i < PlayerScript.playerdata.mapProgress.Count; i++)
        {
            if(PlayerScript.playerdata.mapProgress[i].mapName == mapname)
            {   
                if(StageNumber > 1)
                {
                    //pervious map is complete, moving on to the next
                    if (PlayerScript.playerdata.mapProgress[i].stages[StageNumber - 2].IsComplete())
                        return true;
                    else if (PlayerScript.playerdata.mapProgress[i].stages[StageNumber - 1].IsComplete())   //check if player wants to replay
                        return true;
                    else
                        return false;
                }   
                //no check needed, in mouth, auto unlock stage 1
                return true;
            }
        }
        return false;
    }

    public void Map_ReturnClick()
    {
        Application.LoadLevel("MainMapOverview");
    }
}