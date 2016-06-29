using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ArmyController : MonoBehaviour {

    private List<Character> army;
    private Character leader;

    public Enums.PlayerState currentAction;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void initArmy(List<Character> a)
    {
        army = a;
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
        foreach (Character c in army)
        {
            c.setCurrentState(Enums.PlayerState.Move);
            c.moveTo(pos);
        }
    }
}
