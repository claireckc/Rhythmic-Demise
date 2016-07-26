using UnityEngine;
using System.Collections;

public class BeatSpawner : MonoBehaviour {
    /*
    public static BeatSpawner beatSpawner;

    private int moveBeatCounter;
    private int inputBeatCounter;
    private int beatCounter;
    private int firstInputBeat;

    public GameController gc;

    public NoteControl note1;
    public NoteControl note2;
    public NoteControl note3;
    public NoteControl note4;

    public bool moveActionTurn;
    public bool inputActionTurn;

    private Vector3 comboTextPosition;

	// Use this for initialization
	void Start ()
    {
        moveBeatCounter = 0;
        inputBeatCounter = 0;
        beatCounter = 0;
        moveActionTurn = false;
        inputActionTurn = false;
        comboTextPosition = new Vector3(0, 4, 0);

        InvokeRepeating("spawnBeat", 0, 0.5f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        
	}

    void spawnBeat()
    {
        beatCounter++;

        Instantiate(note1, note1.transform.position, note1.transform.rotation);
        Instantiate(note2, note2.transform.position, note2.transform.rotation);
        Instantiate(note3, note3.transform.position, note3.transform.rotation);
        Instantiate(note4, note4.transform.position, note4.transform.rotation);

        if (inputActionTurn)
        {
            inputBeatCounter++;

            //determine which beat is the first input
            if (inputBeatCounter == 1)
            {
                firstInputBeat = beatCounter;
            }

            //if miss
            if (!gc.lastHit)
            {
                inputBeatCounter = 0;
                inputActionTurn = false;

                gc.clearSequence();

                //reset current streak
                gc.currentStreak = 0;
            }

            if (inputBeatCounter >= 4)
            {
                inputBeatCounter = 0;
                inputActionTurn = false;

                gc.clearSequence();
            }

            gc.lastHit = false;
        }
        else if (moveActionTurn)
        {
            moveBeatCounter++;

            if (moveBeatCounter == 1)
            {
                FloatingTextController.CreateFloatingText(gc.currentStreak.ToString() + " Combo!!", comboTextPosition);
            }

            if (moveBeatCounter >= 4)
            {
                gc.currentStreak++;

                moveActionTurn = false;
                moveBeatCounter = 0;
                ArmyController.armyController.setCurrentState(Enums.PlayerState.Idle);
                ArmyController.armyController.reset();
            }

        }
        //manage if miss the next move after completing a move
        else if (!gc.lastHit)
        {
            gc.clearSequence();

            //reset current streak
            gc.currentStreak = 0;
        }

        //check if current streak is the highest streak
        if (gc.currentStreak > gc.highestStreak)
        {
            gc.highestStreak = gc.currentStreak;
        }

        //reset beat counter
        if (beatCounter >= 8) { beatCounter = 0; }
    }*/
}
