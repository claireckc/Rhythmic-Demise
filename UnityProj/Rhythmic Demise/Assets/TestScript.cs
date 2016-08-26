using UnityEngine;
using System.Collections;

public class TestScript : MonoBehaviour {

	// Use this for initialization
	void Start () {

        AudioSource gameover = GameObject.Find("UI Music/GameOver").GetComponent<AudioSource>();
        gameover.Play();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
