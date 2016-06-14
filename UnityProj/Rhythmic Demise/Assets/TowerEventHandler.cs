using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TowerEventHandler : MonoBehaviour {
	//all will be private!
	public float damage;
	public float cooldown, nextFireTime;
	public float closestDist;
	public GameObject firstCreep;
	public GameObject closestCreep;

	public GameObject arrow;

	GameObject toRemove;
	public List <GameObject> creepList;

	// Use this for initialization
	void Start () {
		creepList = new List<GameObject> ();
		closestDist = 1000.0f;
		cooldown = nextFireTime = 2.0f;
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
		//start finding the closest creep
		if (creepList.Count > 0) {
			firstCreep = creepList [0];
			closestDist = Vector2.Distance (this.transform.position, firstCreep.transform.position);
			closestCreep = firstCreep;

			foreach (GameObject go in creepList) {
				float currentDist = Vector2.Distance (this.transform.position, go.transform.position);
				if (closestDist > currentDist) {
					closestDist = currentDist;
					closestCreep = go;
					break;
				}
			}
		} else
			closestCreep = null;

		if (closestCreep != null) {
			//start attacking it
			if (Time.time >= nextFireTime) {
				AttackCreep ();
				//InvokeRepeating("AttackCreep", cooldown, cooldown);
			}
			//AttackCreep(closestCreep);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		Debug.Log ("Enter" + other.gameObject.tag);
		if (other.gameObject.tag.Contains("Enemy")) {
			creepList.Add (other.transform.parent.gameObject);
		}


		printList ();
	}

	void OnTriggerExit2D(Collider2D other){
		Debug.Log ("Exit");
		if(other.transform.parent.tag.Contains("Enemy") || other.transform.parent != null){
			foreach (GameObject go in creepList) {
				if (other.transform.parent.gameObject == go) {
					toRemove = go;
					break;
				}
			}
		}

		if (toRemove != null)
			creepList.Remove (toRemove);
		closestCreep = null;

		printList ();
	}

	void printList(){
		foreach (GameObject go in creepList) {
			Debug.Log (go);
		}
		Debug.Log (creepList.Count);
	}

	void AttackCreep(){
		Debug.Log ("Attack invoked");
		nextFireTime = Time.time + cooldown;

		Vector3 dir = closestCreep.transform.position - this.transform.position;
		float angle = Mathf.Atan2 (-dir.y, -dir.x) * Mathf.Rad2Deg;

		GameObject shoot = Instantiate (arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
		shoot.SendMessage ("Initialize", closestCreep);
		Debug.Log ("Attacking" + closestCreep);
	}
}
