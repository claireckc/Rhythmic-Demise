using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager_TroopSel : MonoBehaviour {

	public Canvas troopCanvas;
	public Button backButton, cancerButton, diabetesButton;

	void Awake(){
		troopCanvas = troopCanvas.GetComponent<Canvas> ();
		backButton = backButton.GetComponent<Button> ();
		cancerButton = cancerButton.GetComponent<Button> ();
		diabetesButton = diabetesButton.GetComponent<Button> ();
	}

	void Start () {
		troopCanvas.enabled = true;
	}

	public void OnBackPress(){
		//SceneManager.LoadScene ("StartScreen");
		//Destroy (gameObject);
	}

	public void OnCancerPress(){

	}

	public void OnDiabeticPress(){

	}

}
