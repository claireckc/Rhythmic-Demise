using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    public static Timer timer;

    public float time;
    private Text timerText;

	// Use this for initialization
	void Start () {
        if (timer == null)
        {
            timer = this;
        }

        timerText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (!EndResultManager.erm.isDone())
        {
            time -= Time.deltaTime;

            timerText.text = time.ToString("f0");
        }

        if (time <= 0)
        {
            EndResultManager.erm.StopGame();
        }
	}
}
