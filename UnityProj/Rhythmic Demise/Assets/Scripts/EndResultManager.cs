using UnityEngine;
using System.Collections;

public class EndResultManager : MonoBehaviour {
    AudioSource audio;
    Animator anim;
    GameObject endStageClone;

    bool isComplete, done;

	void Awake () {
        anim = GetComponent<Animator>();
        audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        endStageClone = Instantiate(Resources.Load<GameObject>("Prefabs/End Stage"));
	}
	
	void Update () {
        if (ArmyController.armyController.army.Count <= 0)
        {
            audio.Stop();
            anim.SetTrigger("GameOver");
        }
        else if (ArmyController.armyController.currPos.name == "EndPoint")
        {
            isComplete = true;
        }

        //Just for prototype, need to be arrange later
        if (PlayerScript.playerdata.mapProgress[0].stages[2].isCurrent(PlayerScript.playerdata.clickedStageNumber))
        {
            if(!GameObject.Find("Uvula")){
                isComplete = true;
            }
        }

        if (isComplete && !done)
        {
            StopGame();
        }
	}

    void StopGame()
    {
        audio.Stop();
        anim.SetTrigger("Finish");

        //update resources
        endStageClone.SendMessage("UpdateData", (int)PlayerScript.playerdata.clickedMap);
        done = true;
    }
}
