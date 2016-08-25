using UnityEngine;
using System.Collections;

public class SubStageHandler : MonoBehaviour
{

    public RuntimePlatform platform;
    public Ray ray;
    public RaycastHit2D rayHit;
    private GameObject obj;

    AudioSource selectClick, bgmUI;

    void SetupAudio()
    {
        selectClick = GameObject.Find("UI Music/Select").GetComponent<AudioSource>();
        bgmUI = GameObject.Find("UI Music/BGM").GetComponent<AudioSource>();

        selectClick.volume = PlayerScript.playerdata.effectsVolume;
        bgmUI.volume = PlayerScript.playerdata.globalVolume;

        if (!bgmUI.isPlaying)
            bgmUI.Play();
    }

    void PlaySelectAudio()
    {
        selectClick.Play();
    }

    // Use this for initialization
    void Start()
    {
        platform = Application.platform;
        SetupAudio();
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
            if (CheckAccess(PlayerScript.playerdata.clickedMap, stageNumber))
            {
                PlayerScript.playerdata.clickedStageNumber = stageNumber;
                Application.LoadLevel("Resource Management");
            }
        }
    }

    public void DetermineTouchPosition(Vector2 touchPosition)
    {
        ray = Camera.main.ScreenPointToRay(touchPosition);
        rayHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << 15);
        if (rayHit.collider != null)
        {
            PlaySelectAudio();
            EnterLevel(rayHit.collider.gameObject.tag);
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
                    {
                        return true;
                    }
                    else if (PlayerScript.playerdata.mapProgress[i].stages[StageNumber - 1].IsComplete())
                    {   //check if player wants to replay
                        return true;
                    }
                    else {
                        return false;
                    }
                }
                //no check needed, auto unlock stage 1
                return true;
            }
        }
        return false;
    }

    public void Map_ReturnClick()
    {
        PlaySelectAudio();
        PlayerScript.playerdata.clickedStageNumber = 0;
        Application.LoadLevel("MainMapOverview");
    }
}