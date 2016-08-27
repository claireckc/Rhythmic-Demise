using UnityEngine;
using System.Collections;

public class GameMusic : MonoBehaviour {

    AudioSource stageMusic, bossMusic, bgmUI, victoryMusic, gameOverMusic;
    Enums.MainMap currentMap;
    int currentStage;
    bool stagePause, bossPause;

    void Start() {
        currentMap = PlayerScript.playerdata.clickedMap;
        currentStage = PlayerScript.playerdata.clickedStageNumber;

        stageMusic = GameObject.Find("UI Music/Stage Music").GetComponent<AudioSource>();
        bossMusic = GameObject.Find("UI Music/Boss Music").GetComponent<AudioSource>();
        bgmUI = GameObject.Find("UI Music/BGM").GetComponent<AudioSource>();
        victoryMusic = GameObject.Find("UI Music/Victory").GetComponent<AudioSource>();
        gameOverMusic = GameObject.Find("UI Music/GameOver").GetComponent<AudioSource>();

        stagePause = bossPause = false;

        if (bgmUI.isPlaying)
            bgmUI.Stop();

        if (ShouldPlay())
        {
            PlayGameMusic();
        }
    }

    bool ShouldPlay()
    {
        bool play = true;
        Enums.MainMap currentMap = PlayerScript.playerdata.clickedMap;
        int currentStage = PlayerScript.playerdata.clickedStageNumber;

        if (currentMap == Enums.MainMap.Mouth)
        {
            if (currentStage == 1 && PlayerScript.playerdata.firstTut1)
                play = false;

            if (currentStage == 2 && PlayerScript.playerdata.firstTut2)
                play = true;

            if (currentStage == 3 && PlayerScript.playerdata.firstTut3)
                play = true;
        }
        return play;
    }

    void EndGameMusic()
    {
        if (stageMusic.isPlaying)
            stageMusic.Stop();
        if (bossMusic.isPlaying)
            bossMusic.Stop();

    }

    void PlayGameOver()
    {
        EndGameMusic();
        gameOverMusic.Play();
    }

    void PlayVictory()
    {
        EndGameMusic();
        victoryMusic.Play();
    }

    public void PauseGameMusic()
    {
        if (stageMusic.isPlaying)
        {
            stageMusic.Pause();
            stagePause = true;
        }
        if (bossMusic.isPlaying)
        {
            bossMusic.Pause();
            bossPause = true;
        }
    }

    public void UnPauseGameMusic()
    {
        if (bossPause)
            bossMusic.UnPause();

        if (stagePause)
            stageMusic.UnPause();
    }

    void PlayGameMusic()
    {
        if (currentMap == Enums.MainMap.Larnyx || currentMap == Enums.MainMap.Liver || currentMap == Enums.MainMap.SIntes)
        {
            if (currentStage == 2)
                bossMusic.Play();
            else
                stageMusic.Play();
        }
        else
        {
            if (currentStage == 3)
                bossMusic.Play();
            else
                stageMusic.Play();
        }
    }
    
}
