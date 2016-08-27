using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ComboManager : MonoBehaviour {
    public static ComboManager comboManager;

    Animator anim;
    Text comboText;

	// Use this for initialization
	void Start () {
        if (comboManager == null)
        {
            comboManager = this;
        }

        anim = gameObject.GetComponent<Animator>();
        comboText = gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void popCombo()
    {
        comboText.text = GameController.gameController.getCurrentStreak().ToString() + " COMBO!!";
        anim.SetTrigger("Combo");
    }
}
