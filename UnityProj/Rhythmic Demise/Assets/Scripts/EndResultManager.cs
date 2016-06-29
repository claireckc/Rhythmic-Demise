using UnityEngine;
using System.Collections;

public class EndResultManager : MonoBehaviour {

    public GameController gc;

    AudioSource audio;

    Animator anim;

	void Awake () {
        anim = GetComponent<Animator>();

        audio = GameObject.Find("Audio Source").GetComponent<AudioSource>();
	}
	
	void Update () {
        if (gc.army.Count <= 0)
        {
            audio.Stop();
            anim.SetTrigger("GameOver");
        }
        else if (gc.currPos.name == "MovingPoint6")
        {
            audio.Stop();
            anim.SetTrigger("Finish");
        }
	}
}
