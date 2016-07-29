using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;
    public static int comboMultiplier;

    Text text;

	void Start () {
        score = 0;
        comboMultiplier = 1;
        text = GetComponent<Text>();
	}
	
	void Update () {
        text.text = "X" + score;
	}

    public static void addScore()
    {
        score = score + (10 * comboMultiplier);
    }
}
