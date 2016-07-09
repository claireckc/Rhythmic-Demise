using UnityEngine;
using System.Collections;

public class EndResultManager : MonoBehaviour {

    public GameController gc;

    AudioSource audio;

    Animator anim;

    bool isComplete;

	void Awake () {
        anim = GetComponent<Animator>();

        audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
	}
	
	void Update () {
        if (gc.army.Count <= 0)
        {
            audio.Stop();
            anim.SetTrigger("GameOver");
        }
        else if (gc.currPos.name == "EndPoint")
        {
            isComplete = true;
        }

        //Just for prototype, need to be arrange later
        if (PlayerScript.playerdata.mapProgress[0].stages[2].isCurrent)
        {
            if(!GameObject.Find("Uvula")){
                isComplete = true;
            }
        }

        if (isComplete)
        {
            audio.Stop();
            anim.SetTrigger("Finish");

            for (int i = 0; i < PlayerScript.playerdata.mapProgress.Count; i++)
            {
                switch (PlayerScript.playerdata.mapProgress[i].mapName)
                {
                    case Enums.MainMap.Mouth:

                        for (int j = 0; j < PlayerScript.playerdata.mapProgress[i].stages.Count; j++)
                        {
                            if (!PlayerScript.playerdata.mapProgress[i].stages[j].isComplete && PlayerScript.playerdata.mapProgress[i].stages[j].isCurrent)
                            {
                                PlayerScript.playerdata.mapProgress[i].stages[j].isComplete = true;
                                PlayerScript.playerdata.mapProgress[i].stages[j].isCurrent = false;
                            }
                        }
                        break;
                }
            }
        }
	}
}
