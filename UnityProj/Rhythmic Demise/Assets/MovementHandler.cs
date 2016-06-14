using UnityEngine;
using System.Collections;

public class MovementHandler : MonoBehaviour {

	public float movementSpeed = 5.0f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		movement ();
	}

	void movement(){
		if(Input.GetKey(KeyCode.A)){	
			moveLeft ();
		}

		if (Input.GetKey (KeyCode.D)) {
			moveRight ();
		}

		if (Input.GetKey (KeyCode.S)) {
			moveDown ();
		}

		if (Input.GetKey (KeyCode.W)) {
			moveUp ();
		}
	}

	void moveLeft(){
		transform.position += new Vector3 (-movementSpeed * Time.deltaTime, 0.0f, 0.0f);
	}

	void moveRight(){
		transform.position += new Vector3 (movementSpeed * Time.deltaTime, 0.0f, 0.0f);

	}

	void moveUp(){
		transform.position += new Vector3 (0.0f, movementSpeed * Time.deltaTime, 0.0f);
	}

	void moveDown(){
		transform.position += new Vector3 (0.0f, -movementSpeed * Time.deltaTime, 0.0f);
	}

	void OnTriggerEnter2D(Collider2D other){
		if(other.tag.Contains("Projectile"))
			Destroy(other.gameObject);
	}
}
