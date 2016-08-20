using UnityEngine;
using System.Collections;

public class EventSmallIntestine : MonoBehaviour {

    float damage;
    private float nextActionTime;
    private float cooldown;

	void Start () {
        damage = 10;
        nextActionTime = cooldown = 5f;
	}
	
	void Update () {
        if (GameController.gameController.getCurrentStreak() >= 5)
        {
            damage = 8;
        }
        else if (GameController.gameController.getCurrentStreak() >= 10)
        {
            damage = 6;
        }
        else if (GameController.gameController.getCurrentStreak() >= 15) 
        {
            damage = 5;
        }

        if (Time.time >= nextActionTime)
        {
            nextActionTime = Time.time + cooldown;

            ArmyController.armyController.takeDamage(damage);        
        }
	}
}
