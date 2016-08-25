﻿using UnityEngine;
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
        UnlockNextMap((int)PlayerScript.playerdata.clickedMap);
        UpdatePlayerExp(PlayerScript.playerdata.clickedStageNumber);
        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
    }

    void UpdatePlayerExp(int stage)
    {
        /*
        1. Get player unit's level
        2. Get stage (level) and form multiplier (player level / stage number)
        3. call function to level up
        */
        for (int i = 0; i < PlayerScript.playerdata.troopSelected.Count; i++)
        {
            PlayerScript.playerdata.expMultiplier = (float)PlayerScript.playerdata.troopSelected[i].troop.level / stage;
            float baseExp = 1f;//get base experience from level completion, NOT COMPLETE
            PlayerScript.playerdata.troopSelected[i].troop.LevelUp(baseExp * PlayerScript.playerdata.expMultiplier);
        }
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
