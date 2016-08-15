using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {

    public float timer;
    private Text timerText;

	// Use this for initialization
	void Start () {
        timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!EndResultManager.erm.isDone())
        {
            timer -= Time.deltaTime;

            timerText.text = timer.ToString("f0");
        }

        if (timer <= 0)
        {
            EndResultManager.erm.StopGame();
        }
	}
}
