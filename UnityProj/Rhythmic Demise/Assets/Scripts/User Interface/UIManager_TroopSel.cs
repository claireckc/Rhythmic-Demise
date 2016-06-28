using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager_TroopSel : MonoBehaviour {

    public PlayerData playerdata;

	void Awake(){
	}

	void Start ()
    {
        playerdata = FindObjectOfType<PlayerData>();
    }

	public void OnBackPress(){
        Application.LoadLevel("StartScreen");
		//SceneManager.LoadScene ("StartScreen");
		Destroy (gameObject);
	}

	public void OnCancerPress(){
        playerdata.pathogenType = Enums.CharacterType.Cancer;
        MainScreen();
	}

	public void OnDiabeticPress()
    {
        playerdata.pathogenType = Enums.CharacterType.Diabetic;
        MainScreen();

    }

    public void MainScreen()
    {
        Application.LoadLevel("MainMapOverview");
    }

}
