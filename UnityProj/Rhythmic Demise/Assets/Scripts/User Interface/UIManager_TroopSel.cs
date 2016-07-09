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
        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
        MainScreen();
    }

    public void OnDiabeticPress()
    {
        PlayerScript.playerdata.pathogenType = Enums.CharacterType.Diabetic;
        SaveLoadManager.SaveAllInformation(PlayerScript.playerdata);
        MainScreen();

    }

    public void MainScreen()
    {
        Application.LoadLevel("MainMapOverview");
    }

}