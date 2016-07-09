using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Creep : Enemy {

    private float movementSpeed;
    private bool stopAndAttack;

	// Use this for initialization
	void Start () {
        currentHealth = maxHealth = 1;
        cooldown = nextActionTime = 2.0f;
        movementSpeed = 1.0f;
        damage = 0.2f;

        playerList = new List<GameObject>();
	}

    void Initialize(GameObject target){
        closestPlayer = target;
    }

    void Update(){
        if (!IsDead)
        {
            SetHealthVisual(currentHealth / maxHealth);

            //UpdateEnemyList();

            //FindClosestEnemy();

            if (stopAndAttack)
            {
                if (closestPlayer != null)
                {
                    //start attacking it
                    if (Time.time >= nextActionTime)
                    {
                        Action();
                    }
                }
            }
            else
            {
                Vector3 dir = closestPlayer.transform.position - this.transform.position;
                float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
                this.transform.rotation = Quaternion.Euler(0, 0, angle);
                transform.position = Vector2.MoveTowards(transform.position, closestPlayer.transform.position, movementSpeed * Time.deltaTime);
            }
        }
    }

    protected override void Action()
    {
        nextActionTime = Time.time + cooldown;

        //Attack animation start
        Character c = closestPlayer.GetComponent<Character>();
        c.TakeDamage(damage);
    }

    protected override void FindClosestEnemy()
    {
        if (playerList.Count > 0)
        {
            //start finding the closest enemy
            firstPlayer = playerList[0];
            closestDist = Vector2.Distance(this.transform.position, firstPlayer.transform.position);
            closestPlayer = firstPlayer;

            foreach (GameObject go in playerList)
            {
                float currentDist = Vector2.Distance(this.transform.position, go.transform.position);
                if (closestDist > currentDist)
                {
                    closestDist = currentDist;
                    closestPlayer = go;
                    break;
                }
            }
        }
        else
            closestPlayer = null;
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

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            stopAndAttack = true;
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        stopAndAttack = false;
    }

    public override void TakeDamage(float damage)
    {
        FloatingTextController.CreateFloatingText(damage.ToString(), transform);
        currentHealth -= damage;
    }

    public void SetHealthVisual(float healthNormalized)
    {
        healthBar.transform.localScale = new Vector3(healthNormalized,
                                                     healthBar.transform.localScale.y,
                                                     healthBar.transform.localScale.z);
    }
}
