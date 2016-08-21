using UnityEngine;
using System.Collections;

public class ProjectileController : MonoBehaviour {

	public float damage;
	public GameObject enemy;
	public float speed = 2.0f;

    private Vector3 direction;
    private Vector3 goal;
    private const float minDistance = 0.2f;

	// Use this for initialization
	void Start () {

	}

	void Initialize (Vector3 target){
        direction = target - this.transform.position;
		goal = target;
	}

    void initDamage(float damage)
    {
        this.damage = damage;
    }

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate(){
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = Vector2.Lerp(transform.position, goal, speed * Time.deltaTime);

        if ((transform.position - goal).sqrMagnitude <= minDistance * minDistance)
        {
            Destroy(gameObject);
        }
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
            Character player = other.GetComponent<Character>();
            player.TakeDamage(damage);
			Destroy (gameObject);
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if(other.tag == "Player")
			Destroy (this);
	}
}
