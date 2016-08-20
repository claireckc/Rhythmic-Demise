using UnityEngine;
using System.Collections;

public class EventSmallIntestine : MonoBehaviour {

    float damage;
    private float nextActionTime;
    private float cooldown;

	void Start () {
        damage = 5;
        nextActionTime = cooldown = 5f;
	}
	
	void Update () {
        if (GameController.gameController.getCurrentStreak() >= 5)
        {
            damage = 3;
        }   

        if (Time.time >= nextActionTime)
        {
            nextActionTime = Time.time + cooldown;

            ArmyController.armyController.takeDamage(damage);        
        }
	}
}
