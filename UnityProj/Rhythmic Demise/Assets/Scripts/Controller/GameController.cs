using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

    public string moveSequence;

    public KeyCode[] buttonsKeyCode;

    public ArmyController armyController;
    public List<Character> army;
    public BeatSpawner bs;
    
    //UI
    public Text archerCountText;

    private int archerCount;

    void init()
    {
        moveSequence = "";
        armyController = gameObject.AddComponent<ArmyController>();
        armyController.initArmy(army);

        updateUI();
    }

	// Use this for initialization
	void Start () {
        init();
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
                //move left
                case "1111":
                    armyController.setCurrentState(Enums.PlayerState.MoveLeft);
                    break;
                //move right
                case "2222":
                    armyController.setCurrentState(Enums.PlayerState.MoveRight);
                    break;
                //move up
                case "3333":
                    armyController.setCurrentState(Enums.PlayerState.MoveUp);
                    break;
                //move down
                case "4444":
                    armyController.setCurrentState(Enums.PlayerState.MoveDown);
                    break;
                //normal attack
                case "1131":
                    armyController.setCurrentState(Enums.PlayerState.Attack);
                    break;
                //use special skill
                case "3343":
                    armyController.setCurrentState(Enums.PlayerState.Skill);
                    break;
            }

            clearSequence();
            bs.moveActionTurn = true;
        }
	}

    public void updateUI()
    {
        archerCount = 0;

        foreach (Archer archer in army)
        {
            archerCount++;
        }

        archerCountText.text = "x" + archerCount;
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
