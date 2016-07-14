using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ArmyController : MonoBehaviour {

    public static List<Character> army;
    public static MovingPoint goalPos;
    private Character leader;

    public Enums.PlayerState currentAction;

    float dist;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	}

    public void initArmy(List<Character> a, MovingPoint currPos)
    {
        army = a;
        goalPos = currPos;

        foreach (Character c in army)
        {
            c.transform.position = currPos.transform.position;
        }
    }

    public void setCurrentState(Enums.PlayerState action)
    {
        foreach (Character c in army)
        {
            c.setCurrentState(action);
        }
    }

    public void reset()
    {
        foreach (Character c in army)
        {
            c.reset();
        }
    }

    public void moveTo(MovingPoint pos)
    {
        goalPos = pos;

        foreach (Character c in army)
        {
            c.setCurrentState(Enums.PlayerState.Move);
            c.moveTo(pos);
        }
    }
}
