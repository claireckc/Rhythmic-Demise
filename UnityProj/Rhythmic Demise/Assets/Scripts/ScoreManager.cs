using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score;

    Text text;

	void Start () {
        score = 0;
        text = GetComponent<Text>();
	}
	
	void Update () {
        text.text = "X" + score;
	}
}
