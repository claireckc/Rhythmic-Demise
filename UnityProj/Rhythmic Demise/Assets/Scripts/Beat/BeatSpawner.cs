using UnityEngine;
using System.Collections;

public class BeatSpawner : MonoBehaviour {

    private int moveBeatCounter;
    private int inputBeatCounter;

    public GameController gc;

    public NoteControl note1;
    public NoteControl note2;
    public NoteControl note3;
    public NoteControl note4;

    public bool moveActionTurn;
    public bool inputActionTurn;

	// Use this for initialization
	void Start ()
    {
        moveBeatCounter = 0;
        inputBeatCounter = 0;
        moveActionTurn = false;
        inputActionTurn = false;
        InvokeRepeating("spawnBeat", 0, 0.5f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    void spawnBeat()
    {
        Instantiate(note1, note1.transform.position, note1.transform.rotation);
        Instantiate(note2, note2.transform.position, note2.transform.rotation);
        Instantiate(note3, note3.transform.position, note3.transform.rotation);
        Instantiate(note4, note4.transform.position, note4.transform.rotation);

        if (inputActionTurn)
        {
            inputBeatCounter++;

            if (inputBeatCounter >= 4)
            {
                inputBeatCounter = 0;
                inputActionTurn = false;

                gc.clearSequence();
            }
        }
        else if (moveActionTurn)
        {
            moveBeatCounter++;  

            if (moveBeatCounter >= 4)
            {
                moveActionTurn = false;
                moveBeatCounter = 0;
                gc.armyController.setCurrentState(Enums.PlayerState.Idle);
            }

        }
    }
}
