﻿using UnityEngine;
using System.Collections;

public class EndResultManager : MonoBehaviour {
    public static EndResultManager erm;

    AudioSource audio;
    Animator anim;
    GameObject endStageClone;

    bool isComplete, done;
    bool isBossStage;

	void Start () {
        if (erm == null) { 
            erm = this; 
        }

        anim = GetComponent<Animator>();
        audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        endStageClone = Instantiate(Resources.Load<GameObject>("Prefabs/End Stage"));
	}
	
	void Update () {
        if (ArmyController.armyController.army.Count <= 0)
        {
            audio.Stop();
            GameObject.Find("Game Music").SendMessage("EndGameMusic");
            GameObject.Find("Game Music").SendMessage("PlayGameOver");
            anim.SetTrigger("GameOver");
        }
        else if (ArmyController.armyController.currPos.name == "EndPoint")
        {
            isComplete = true;
        }
        /*
        //Just for prototype, need to be arrange later
        if (PlayerScript.playerdata.mapProgress[0].stages[2].isCurrent(PlayerScript.playerdata.clickedStageNumber))
        {
            if(!GameObject.Find("Boss")){
                isComplete = true;
            }
        }*/

        if (GameObject.Find("Boss") && !isBossStage)
        {
            isBossStage = true;
        }

        if (isBossStage)
        {
            Boss b = GameObject.Find("Boss").GetComponent<Boss>();
            if (b.IsDead)
            {
                isComplete = true;
            }
        }

        if (isComplete && !done)
        {
            GameObject.Find("Game Music").SendMessage("PlayVictory");
            StopGame();
        }
	}

    public void StopGame()
    {
        audio.Stop();
        anim.SetTrigger("Finish");

        //update resources
        endStageClone.SendMessage("UpdateData", (int)PlayerScript.playerdata.clickedMap);
        done = true;
    }

    public bool isDone()
    {
        return done;
    }
}
