using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenuCanvas;

    AudioSource audio;

    void Start()
    {
        audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
    }

	void Update () {
	}

    public void Resume()
    {
        pauseMenuCanvas.SetActive(false);
        Time.timeScale = 1f;

        if (!audio.isPlaying)
        {
            audio.Play();
        }
    }

    public void Pause()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        audio.Pause();
    }
}
