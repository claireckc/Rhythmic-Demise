using UnityEngine;
using System.Collections;

public class CreepAI : MonoBehaviour {

    public float health, damage, movementSpeed;
    public float attackTime, nextAttackTime;
    public GameObject closestEnemy;
    public bool stopAndAttack;

	// Use this for initialization
	void Start () {
        attackTime = nextAttackTime = 2.0f;
        movementSpeed = 1.0f;
	}

    void Initialize(GameObject target){
        closestEnemy = target;
    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void FixedUpdate(){
        //follow current enemy, update the facing direction
        if (stopAndAttack) {
            attack();
        }
        else {
            Vector3 dir = closestEnemy.transform.position - this.transform.position;
            float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
            this.transform.rotation = Quaternion.Euler(0, 0, angle);
            transform.position = Vector2.MoveTowards(transform.position, closestEnemy.transform.position, movementSpeed * Time.deltaTime);
        }

    }

    void attack() {
        //get closest enemy health and make damage
        //run attacking animation
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enter something");
        if (other.gameObject.tag.Contains("Enemy"))
        {
            if (other.transform.parent.gameObject == closestEnemy)
                stopAndAttack = true;
            else
                stopAndAttack = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        stopAndAttack = false;
    }
}
