using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager_TroopSel : MonoBehaviour
{

    public PlayerScript playerdata;
    AudioSource selectClick;

    void Awake()
    {
    }

    void Start()
    {
        playerdata = FindObjectOfType<PlayerScript>();
        selectClick = GameObject.Find("UI Music/Select").GetComponent<AudioSource>();
    }

    void PlaySelectAudio()
    {
        selectClick.Play();
    }

    public void OnBackPress()
    {
        PlaySelectAudio();
        Application.LoadLevel("StartScreen");
        Destroy(gameObject);
    }

    public void OnCancerPress()
    {
        PlaySelectAudio();
        PlayerScript.playerdata.pathogenType = Enums.CharacterType.Cancer;
        PlayerScript.playerdata.attackBonus = 0;
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
        PlaySelectAudio();
        PlayerScript.playerdata.pathogenType = Enums.CharacterType.Diabetic;
        PlayerScript.playerdata.attackBonus = 10;
        PlayerScript.playerdata.defenseBonus = 0;

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