using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

    public string moveSequence;

    public KeyCode keyUp;
    public KeyCode keyDown;
    public KeyCode keyLeft;
    public KeyCode keyRight;

    public PlayerController player;
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
                    player.move(-1, 0);
                    break;
                case "DDDD":
                    player.move(1, 0);
                    break;
                case "WWWW":
                    player.move(0, 1);
                    break;
                case "SSSS":
                    player.move(0, -1);
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
