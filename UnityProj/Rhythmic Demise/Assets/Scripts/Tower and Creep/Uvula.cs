using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Uvula : Enemy {

    public GameObject slime;

    public GameObject healthBar;

	void Start () {
        currentHealth = maxHealth = 4;
        playerList = new List<GameObject>();
        cooldown = nextFireTime = 5.0f;
	}
	
	void Update () {
        if (!IsDead)
        {
            SetHealthVisual(currentHealth / maxHealth);

            UpdateEnemyList();

            FindClosestEnemy();

            if (closestPlayer != null)
            {
                //start attacking it
                if (Time.time >= nextFireTime)
                {
                    Attack();
                }
            }
        }
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
            playerList.Add(other.gameObject);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject go in playerList)
            {
                if (other.gameObject == go)
                {
                    toRemove = go;
                    break;
                }
            }

            if (toRemove != null)
            {
                playerList.Remove(toRemove);
            }

            //closestEnemy = null;
        }
    }

    protected override void Attack()
    {
        nextFireTime = Time.time + cooldown;

        Vector3 dir = closestPlayer.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject shoot = Instantiate(slime, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        shoot.SendMessage("Initialize", closestPlayer);
    }

    public override void TakeDamage(float damage)
    {
        FloatingTextController.CreateFloatingText(damage.ToString(), transform);
        currentHealth -= damage;
    }

    // Health between [0.0f,1.0f] == (currentHealth / totalHealth)
    public void SetHealthVisual(float healthNormalized)
    {
        healthBar.transform.localScale = new Vector3(healthNormalized,
                                                     healthBar.transform.localScale.y,
                                                     healthBar.transform.localScale.z);
    }
}
