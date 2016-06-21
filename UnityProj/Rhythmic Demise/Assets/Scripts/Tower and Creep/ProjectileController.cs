using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	public float damage;
	public GameObject enemy;
	public float movementSpeed = 2.0f;

	// Use this for initialization
	void Start () {

	}

	void Initialize (GameObject target){
		enemy = target;
	}


	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){

		Vector3 dir = enemy.transform.position - this.transform.position;
		float angle = Mathf.Atan2 (-dir.y, -dir.x) * Mathf.Rad2Deg;
		this.transform.rotation = Quaternion.Euler (0, 0, angle);
		transform.position = Vector2.Lerp (transform.position, enemy.transform.position, movementSpeed * Time.deltaTime);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag.Contains("Enemy")) {
			Destroy (gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag.Contains("Enemy"))
			Destroy (gameObject);
	}
}
