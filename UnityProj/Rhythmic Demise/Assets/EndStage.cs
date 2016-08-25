using UnityEngine;
using System.Collections;

public class EndStage : MonoBehaviour
{
    int stageCount;
    MainMap currentMap;
    SubMap currentStage;

    public void UpdateData(int highestStreak)
    {
        currentMap = PlayerScript.playerdata.mapProgress[(int)PlayerScript.playerdata.clickedMap];
        currentStage = currentMap.stages[PlayerScript.playerdata.clickedStageNumber - 1];
        PlayerScript.playerdata.totalResource += ScoreManager.score;
        UpdateMap(highestStreak);
        UpdateStars();
        UpdateTroop();
        UnlockNextMap((int)PlayerScript.playerdata.clickedMap);
        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
    }
    
    void UnlockNextMap(int currentMapIndex)
    {
        //check if it is the last substage
        if (PlayerScript.playerdata.clickedStageNumber == PlayerScript.playerdata.mapProgress[currentMapIndex].stages.Count)
        {
            //last substagae, unlock the next map
            PlayerScript.playerdata.mapProgress[currentMapIndex + 1].isLocked = false;
            PlayerScript.playerdata.mapProgress[currentMapIndex + 1].stages[0].topComboCount = 0;
        }
        else
        {
            PlayerScript.playerdata.mapProgress[currentMapIndex].stages[PlayerScript.playerdata.clickedStageNumber].topComboCount = 0;
        }
    }

    void UpdateTroop()
    {
        if(PlayerScript.playerdata.clickedMap == Enums.MainMap.Mouth)
        {
            if (PlayerScript.playerdata.clickedStageNumber == 1)
            {
                if (PlayerScript.playerdata.troopData[1].level == 0)
                    PlayerScript.playerdata.troopData[1].level = 1;
            }
            else if (PlayerScript.playerdata.clickedStageNumber == 2)
            {
                if (PlayerScript.playerdata.troopData[2].level == 0)
                    PlayerScript.playerdata.troopData[2].level = 1;
            }
        }
    }

    public void UpdateMap(int highestStreak)
    {
        if (currentStage.topComboCount < highestStreak)
        {
            currentStage.topComboCount = highestStreak;
            currentStage.resourceAttained = ScoreManager.score;
        }
    }

    public void UpdateStars()
    {
        int starsAttained = 0;
        for (int i = 0; i < currentStage.comboRange.Count; i++)
        {
            if (currentStage.topComboCount > currentStage.comboRange[i])
                starsAttained++;
        }

        if (starsAttained > currentStage.stars)
            currentStage.stars = starsAttained;
    }
}
