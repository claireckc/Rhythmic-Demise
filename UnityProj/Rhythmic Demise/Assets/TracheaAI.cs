using UnityEngine;
using System.Collections;


public class TracheaAI : MonoBehaviour {
	
	private float skillTime;
	private float attackTime;

	// Use this for initialization
	void Start () {
		skillTime = attackTime = Random.Range (5.0f, 8.0f);
	}

	void FixedUpdate(){
		
		if (Time.time >= attackTime) {
			print ("attack");
			attackTime = Time.time + skillTime;
			skillTime = RandomTime ();
			//attack ();
		} else
			print ("dont attack");
	}

	public float RandomTime(){
		return Random.Range (3.0f, 5.0f);
	}

	public void stunAttack(){
		//stun the players that are not defending
		print("stun them");
	}

	public void damageAttack(){
		print("damage them");

	}

	public void defenseDropAttack(){
		print("defense drop them");

	}

	public void attack(){
		attackTime = Time.time + skillTime;
		switch (Random.Range(1, 4)) {
		case 1:
			stunAttack ();
			break;
		case 2:
			damageAttack ();
			break;
		case 3:
			defenseDropAttack ();
			break;
		case 4:
			break;
		}
		skillTime = RandomTime ();

	}
}
