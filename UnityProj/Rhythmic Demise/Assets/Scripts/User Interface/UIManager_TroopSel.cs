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
        PlayerScript.playerdata.attackBonus = 5;
        PlayerScript.playerdata.defenseBonus = 10;

        for (int i = 0; i < 3; i++)
        {
            PlayerScript.playerdata.troopData[i].damage += PlayerScript.playerdata.attackBonus;
            PlayerScript.playerdata.troopData[i].armor += PlayerScript.playerdata.defenseBonus;
        }

        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
        MainScreen();
    }

    public void OnDiabeticPress()
    {
        PlayerScript.playerdata.pathogenType = Enums.CharacterType.Diabetic;
        PlayerScript.playerdata.attackBonus = 10;
        PlayerScript.playerdata.defenseBonus = 5;

        for (int i = 0; i < 3; i++)
        {
            PlayerScript.playerdata.troopData[i].damage += PlayerScript.playerdata.attackBonus;
            PlayerScript.playerdata.troopData[i].armor += PlayerScript.playerdata.defenseBonus;
        }

        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
        MainScreen();

    }

    public void MainScreen()
    {
        Application.LoadLevel("MainMapOverview");
    }

}