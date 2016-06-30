using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public static int score = 0;

    Text text;

	void Start () {
        text = GetComponent<Text>();
	}
	
	void Update () {
        text.text = "X" + score;
	}
}
