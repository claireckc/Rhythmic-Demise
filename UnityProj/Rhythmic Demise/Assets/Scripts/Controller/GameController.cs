using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public string moveSequence;

    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft;
    public KeyCode keyRight;

    public Button[] buttons;

    public Character player;
    public BeatSpawner bs;

	// Use this for initialization
	void Start () {
        moveSequence = "";
	}
	
	// Update is called once per frame
	void Update () {
        if (moveSequence.Length == 1)
        {
            bs.inputActionTurn = true;
        }

        if (moveSequence.Length >= 4)
        {
            switch (moveSequence)
            {
                case "AAAA":
                    player.setCurrentAction("moveLeft");
                    break;
                case "DDDD":
                    player.setCurrentAction("moveRight");
                    break;
                case "WWWW":
                    player.setCurrentAction("moveUp");
                    break;
                case "SSSS":
                    player.setCurrentAction("moveDown");
                    break;
            }

            clearSequence();
            bs.moveActionTurn = true;
        }
	}

    public void addHit(string hit)
    {
        moveSequence += hit;
    }

    public void clearSequence()
    {
        moveSequence = "";
    }
}
