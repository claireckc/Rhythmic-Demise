﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossHeart : Boss{

    public GameObject projectile;
    public GameObject troopPrefab; //rbc

	void Start () {
        playerList = new List<GameObject>();
        cooldown = nextActionTime = 5.0f;
        currentHealth = maxHealth = 20;
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
                    int tempNum = Random.Range(1, 100);
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
        Instantiate(troopPrefab, this.transform.position, this.transform.rotation);
        Instantiate(troopPrefab, this.transform.position, this.transform.rotation);
        Instantiate(troopPrefab, this.transform.position, this.transform.rotation);
    }

    protected override void Action()
    {
        nextActionTime = Time.time + cooldown;

        Vector3 dir = closestPlayer.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject shoot = Instantiate(projectile, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        shoot.SendMessage("Initialize", closestPlayer);
    }
}
