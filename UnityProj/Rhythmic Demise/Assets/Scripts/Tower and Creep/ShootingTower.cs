﻿using UnityEngine;
using System.Collections;

public class ShootingTower : Tower {

    //tower inst
    public GameObject arrow;

	// Use this for initialization
	new void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	new void Update () {
        if (!IsDead)
        {
            base.Update();

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

        anim.SetTrigger("Attack");
    }

    void triggerAttack()
    {
        Vector3 dir = closestPlayer.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject shoot = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        shoot.SendMessage("Initialize", closestPlayer.transform.position);
        shoot.SendMessage("initDamage", damage);
    }
}
