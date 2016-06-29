using UnityEngine;
using System.Collections;

public class EndResultManager : MonoBehaviour {

    public GameController gc;

    Animator anim;

	void Awake () {
        anim = GetComponent<Animator>();
	}
	
	void Update () {
        if (gc.army.Count <= 0)
        {
            
            anim.SetTrigger("GameOver");
        }
        else if (gc.currPos.name == "MovingPoint6")
        {
            anim.SetTrigger("Finish");
        }
	}
}
