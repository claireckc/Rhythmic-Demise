using UnityEngine;
using System.Collections;

public class ArmyController : MonoBehaviour {

    private Character[] army;

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
        foreach (Character c in army)
        {
            c.setCurrentState(action);
        }
    }
}
