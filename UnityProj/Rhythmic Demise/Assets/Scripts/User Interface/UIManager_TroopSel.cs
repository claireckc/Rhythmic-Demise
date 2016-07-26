using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager_TroopSel : MonoBehaviour
{

    public PlayerScript playerdata;

    void Awake()
    {
    }

    void Start()
    {
        playerdata = FindObjectOfType<PlayerScript>();
    }

    public void OnBackPress()
    {
        Application.LoadLevel("StartScreen");
        //SceneManager.LoadScene ("StartScreen");
        Destroy(gameObject);
    }

    public void OnCancerPress()
    {
        PlayerScript.playerdata.pathogenType = Enums.CharacterType.Cancer;
        PlayerScript.playerdata.attackMultiplier = 1;
        PlayerScript.playerdata.defenseMultiplier = 1.5f;

        for (int i = 0; i < 3; i++)
        {
            PlayerScript.playerdata.troopData[i].attack *= PlayerScript.playerdata.attackMultiplier;
            PlayerScript.playerdata.troopData[i].defenseRating *= PlayerScript.playerdata.defenseMultiplier;
        }

        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
        MainScreen();
    }

    public void OnDiabeticPress()
    {
        PlayerScript.playerdata.pathogenType = Enums.CharacterType.Diabetic;
        PlayerScript.playerdata.attackMultiplier = 1.5f;
        PlayerScript.playerdata.defenseMultiplier = 1;

        for (int i = 0; i < 3; i++)
        {
            PlayerScript.playerdata.troopData[i].attack *= PlayerScript.playerdata.attackMultiplier;
            PlayerScript.playerdata.troopData[i].defenseRating *= PlayerScript.playerdata.defenseMultiplier;
        }

        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
        MainScreen();

    }

    public void MainScreen()
    {
        Application.LoadLevel("MainMapOverview");
    }

}