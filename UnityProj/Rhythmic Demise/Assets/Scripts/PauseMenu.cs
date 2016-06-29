using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {

    public bool isPaused;

    public GameObject pauseMenuCanvas;

    AudioSource audio;

    void Start()
    {
        audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }

	void Update () {
        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            audio.Pause();
        }
        else
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;

            if(!audio.isPlaying)
                audio.Play();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isPaused = !isPaused;
        }
	}

    public void Resume()
    {
        isPaused = false;
    }
}
