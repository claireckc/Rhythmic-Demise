using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossHeart : Boss{

    public GameObject projectile;
    public GameObject troopPrefab; //rbc

	void Start () {
        currentHealth = maxHealth = 20;
        playerList = new List<GameObject>();
        cooldown = nextActionTime = 5.0f;
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
                if (Time.time >= nextActionTime)
                {
                    nextActionTime = Time.time + cooldown;

                    int tempNum = Random.Range(1, 101);
                    if (tempNum <= 85)
                        Action();
                    else
                        specialAction();
                }
            }
        }
	}

    protected override void specialAction()
    {
        for (int i = 0; i <= 2; i++)
        {
            Vector3 dir = closestPlayer.transform.position - this.transform.position;
            float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

            GameObject spawn = Instantiate(troopPrefab, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
            
            spawn.SendMessage("Initialize", closestPlayer.transform.position);
        }
    }

    protected override void Action()
    {
        Vector3 dir = closestPlayer.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject shoot = Instantiate(projectile, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        shoot.SendMessage("Initialize", closestPlayer.transform.position);
    }
}
