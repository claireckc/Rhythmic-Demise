using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Uvula : Boss {

    public GameObject projectile;

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
                    Action();
                }
            }
        }
	}

    protected override void Action()
    {
        nextActionTime = Time.time + cooldown;

        Vector3 dir = closestPlayer.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject shoot = Instantiate(projectile, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        shoot.SendMessage("Initialize", closestPlayer);
    }

    protected override void specialAction()
    {
        
    }
}
