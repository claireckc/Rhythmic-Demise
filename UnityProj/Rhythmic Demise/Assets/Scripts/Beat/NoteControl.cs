using UnityEngine;
using System.Collections;

public class NoteControl : MonoBehaviour {

    private Vector3 speed;

	// Use this for initialization
	void Start () {
        speed = new Vector3(0, -5, 0);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        transform.Translate(speed * Time.deltaTime);

	}

    void OnTriggerEnter2D()
    {

    }

    void OnTriggerExit2D()
    {
        /*if (bs.inputActionTurn)
        {
            bs.inputActionTurn = false;
        }*/

        Destroy(gameObject);
    }

}
