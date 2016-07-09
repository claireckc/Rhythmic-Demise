using UnityEngine;
using System.Collections;

public class NoteControl : MonoBehaviour {

    private Vector3 speed;

	// Use this for initialization
	void Start () {
        speed = new Vector3(0, -4, 0);
	}   
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(speed * Time.deltaTime);

        Destroy(gameObject, 0.5f);
	}
}
