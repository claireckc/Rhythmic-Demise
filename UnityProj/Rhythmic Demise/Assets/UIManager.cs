﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour {

	//Start Canvas
	public Canvas startCanvas;
	public Text startText;
	public Button startButton, optionButton, exitButton;

	//Option Canvas
	public Canvas optionCanvas;
	public Button eraseButton, backButton, volumeButton, aboutButton;

	void Awake(){
		//Start Canvas
		startCanvas = startCanvas.GetComponent<Canvas> ();
		startText = startText.GetComponent<Text> ();
		startButton = startButton.GetComponent<Button> ();
		optionButton = optionButton.GetComponent<Button> ();
		exitButton = exitButton.GetComponent<Button> ();

		//Option Canvas
		optionCanvas = optionCanvas.GetComponent<Canvas>();
		eraseButton = eraseButton.GetComponent<Button> ();
		backButton = backButton.GetComponent<Button> ();
		volumeButton = volumeButton.GetComponent<Button> ();
		aboutButton = aboutButton.GetComponent<Button> ();
	}

	void Start () {
		optionCanvas.enabled = false;
		//startText.text = "Start New Game";
	}

	//for start canvas
	public void ExitPress_Start(){
		Application.Quit ();
	}

	public void StartPress_Start(){

		SceneManager.LoadScene ("TutorialScene");
	}

	public void OptionPress_Start(){
		//disable components in canvas 
		startButton.enabled = false;
		optionButton.enabled = false;
		exitButton.enabled = false;
		optionCanvas.enabled = true;
	}

	//for option canvas
	public void BackPress_Opt(){
		
		startButton.enabled = true;
		optionButton.enabled = true;
		exitButton.enabled = true;
		optionCanvas.enabled = false;
	}

	public void ErasePress_Opt(){

	}

	public void VolPress_Opt(){

	}

	public void AboutPress_Opt(){

	}
}
