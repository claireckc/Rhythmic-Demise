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
        goalPos = pos;

        foreach (Character c in army)
        {
            c.setCurrentState(Enums.PlayerState.Move);

            switch (c.getJobType())
            {
                case Enums.JobType.Knight:
                    float knightY = Random.Range(pos.transform.position.y - 1, pos.transform.position.y + 1);
                    Vector3 knightTempPos = new Vector3(pos.transform.position.x + 1, knightY);

                    c.moveTo(knightTempPos);
                    break;
                case Enums.JobType.Archer:
                    float archerY = Random.Range(pos.transform.position.y - 1, pos.transform.position.y + 1);
                    Vector3 archerTempPos = new Vector3(pos.transform.position.x - 1, archerY);

                    c.moveTo(archerTempPos);
                    break;
                case Enums.JobType.Priest:
                    float priestY = Random.Range(pos.transform.position.y - 1, pos.transform.position.y + 1);
                    Vector3 priestTempPos = new Vector3(pos.transform.position.x, priestY);

                    c.moveTo(priestTempPos);
                    break;
            }
        }
    }
}
