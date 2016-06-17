using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DebugText : MonoBehaviour {

    Text text;

    public GameController gc;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        if (gc.bs.inputActionTurn)
        {
            text.text = "Input Turn";
        }
        else if (gc.bs.moveActionTurn)
        {
            text.text = "Move Turn";
        }
        else
        {
            text.text = "Waiting for Input";
        }
	}
}
