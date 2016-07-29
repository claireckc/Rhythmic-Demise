using UnityEngine;
using System.Collections;

public class EndStage : MonoBehaviour {
    int stageCount;
    MainMap currentMap;
    SubMap currentStage;
    
    public void UpdateData()
    {
        currentMap = PlayerScript.playerdata.mapProgress[(int)PlayerScript.playerdata.clickedMap];
        currentStage = currentMap.stages[PlayerScript.playerdata.clickedStageNumber - 1];
        PlayerScript.playerdata.totalResource += ScoreManager.score;
        UpdateMap();
        UpdateStars();
        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
        
        for(int i = 0; i < currentMap.stages.Count; i++)
        {
            print("Combo: " + currentMap.stages[i].topComboCount);
            print("Stars: " + currentMap.stages[i].stars);
        }
    }

    public void UpdateMap()
    {
       if(currentStage.topComboCount < ScoreManager.score)
        {
            currentStage.topComboCount = ScoreManager.score;
        }
    }

    public void UpdateStars()
    {
        int starsAttained = 0;
        for(int i = 0; i < currentStage.comboRange.Count; i++)
        {
            if (currentStage.topComboCount > currentStage.comboRange[i])
                starsAttained++;
        }

        if (starsAttained > currentStage.stars)
            currentStage.stars = starsAttained;
    }
}
