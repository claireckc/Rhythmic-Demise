using UnityEngine;
using System.Collections;

public class EventLiver : MonoBehaviour {

    public GameObject spellPrefab;
    public GameObject[] spellLocation;

    private float nextActionTime;
    private float cooldown;
    private MovingPoint targetPos1;
    private MovingPoint targetPos2;
    private float spellDamage;
    private float spellAmount;

    // Use this for initialization
	void Start () {
        nextActionTime = cooldown = 4f;
        spellDamage = 40;
        spellAmount = 1;
	}
	
	// Update is called once per frame
	void Update () {
        Action();
	}

    void Action()
    {
        if (Time.time >= nextActionTime)
        {
            nextActionTime = Time.time + cooldown;

            for (int i = 0; i < spellAmount; i++)
            {
                int index = Random.Range(0, spellLocation.Length);

                targetPos1 = spellLocation[index].GetComponent<MovingPoint>();

                GameObject sc = Instantiate(spellPrefab, targetPos1.transform.position, spellPrefab.transform.rotation) as GameObject;
                Destroy(sc, 2);

                if (ArmyController.armyController.currPos == targetPos1)
                {
                    ArmyController.armyController.takeDamage(spellDamage);
                }
            }

            
            if (Timer.timer.time <= 25)
            {
                spellAmount = 3;
            }
            else if (Timer.timer.time <= 45)
            {
                spellAmount = 2;
            }
        }
    }
}
