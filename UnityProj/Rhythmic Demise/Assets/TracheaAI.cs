using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TracheaAI : Enemy {
	
	// Use this for initialization
	void Start () {
		cooldown = nextActionTime = Random.Range (3.0f, 6.0f);
		playerList = new List<GameObject> ();
	}

	void FixedUpdate(){
		
		if (Time.time >= nextActionTime) {
			Action ();
		}
	}

	public float RandomTime(){
		return Random.Range (3.0f, 6.0f);
	}

	public void stunAttack(){
		//stun the players that are not defending
		print("stun them");
		for (int i = 0; i < playerList.Count; i++) {
			Character c = playerList [i].GetComponent<Character> ();

		}
	}

	public void damageAttack(){
		for (int i = 0; i < playerList.Count; i++) {
			Character c = playerList [i].GetComponent<Character> ();
			c.TakeDamage (damage);
		}
	}

	public void defenseDropAttack(){
		print("defense drop them");

	}

	protected override void Action(){
		nextActionTime = Time.time + cooldown;
		switch (Random.Range(1, 4)) {
		case 1:
			stunAttack ();
			break;
		case 2:
			//damage range
			damage = Random.Range (4, 8);
			damageAttack ();
			break;
		case 3:
			defenseDropAttack ();
			break;
		case 4:
			break;
		}
		cooldown = RandomTime ();

	}

	protected override void FindClosestEnemy(){

	}

	protected override void UpdateEnemyList()
	{
		for (int i = 0; i < playerList.Count; i++)
		{
			Character c = playerList[i].GetComponent<Character>();

			if (c.IsDead)
			{
				//Need to be re-arrange soon
				playerList.Remove(playerList[i]);
				GameController gc = GameObject.Find("GameController").GetComponent<GameController>();
				gc.army.Remove(c);
				gc.updateUI();

				Destroy(c.gameObject);
			}
		}
	}

	protected override void OnTriggerEnter2D(Collider2D other){

	}

	protected override void OnTriggerExit2D(Collider2D other){

	}

	public override void TakeDamage(float damage){

	}
}
