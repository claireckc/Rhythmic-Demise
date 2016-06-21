using UnityEngine;
using System.Collections;

public class ArmyController : MonoBehaviour {

    private Character[] army;
    private Character leader;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void initArmy(Character[] a)
    {
        army = a;
    }

    public void setCurrentState(Enums.PlayerState action)
    {
        /*if (action == Enums.PlayerState.Skill)
        {
            leader.setCurrentState(action);
        }
        else
        {
            foreach (Character c in army)
            {
                c.setCurrentState(action);
            }
        }*/

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
}
