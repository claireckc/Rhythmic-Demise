using UnityEngine;
using System.Collections;

public class EndResultManager : MonoBehaviour {
    AudioSource audio;
    const int STARS = 3;
    Animator anim;

    bool isComplete, done;

	void Awake () {
        anim = GetComponent<Animator>();

        audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
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
        if (PlayerScript.playerdata.mapProgress[0].stages[2].isCurrent)
        {
            if(!GameObject.Find("Uvula")){
                isComplete = true;
            }
        }

        if (isComplete && !done)
        {
            audio.Stop();
            anim.SetTrigger("Finish");
            
            //update resources
            PlayerScript.playerdata.totalResource += ScoreManager.score;

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
                                
                                //check if the current saved map score is higher than newly achieved map score
                                if(PlayerScript.playerdata.mapProgress[i].stages[i].topComboCount < ScoreManager.score)
                                {
                                    int gotStars = 0;
                                    PlayerScript.playerdata.mapProgress[i].stages[i].topComboCount = ScoreManager.score;
                                    for (int k = 0; k < STARS; k++)
                                    {
                                        if (PlayerScript.playerdata.mapProgress[i].stages[i].comboRange[k] < ScoreManager.score)
                                            gotStars++;
                                    }
                                    //update stars
                                    if (PlayerScript.playerdata.mapProgress[i].stages[i].stars < gotStars)
                                        PlayerScript.playerdata.mapProgress[i].stages[i].stars = gotStars;
                                }
                                break;
                            }
                        }
                        break;
                }
            }

            done = true;
        }
        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
	}
}
