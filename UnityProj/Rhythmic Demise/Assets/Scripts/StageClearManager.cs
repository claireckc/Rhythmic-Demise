using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageClearManager : MonoBehaviour {

    Text scoreText;

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.Find("CoinAmountText");
        scoreText = go.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "X" + ScoreManager.score;
	}
}
