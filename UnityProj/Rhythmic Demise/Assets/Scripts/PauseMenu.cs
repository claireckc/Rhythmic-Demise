using UnityEngine;
using System.Collections;

public class PauseMenu : MonoBehaviour {
    public GameObject pauseMenuCanvas;

    AudioSource audio;
    GameMusic gm;

    void Start()
    {
        audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        gm = GameObject.Find("Game Music").GetComponent<GameMusic>();
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

        gm.UnPauseGameMusic();
    }

    public void Pause()
    {
        pauseMenuCanvas.SetActive(true);
        Time.timeScale = 0f;

        audio.Pause();

        gm.PauseGameMusic();
    }
}
