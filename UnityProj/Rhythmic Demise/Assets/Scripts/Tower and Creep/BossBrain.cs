using UnityEngine;
using System.Collections;

public class BossBrain : Boss {

    public GameObject projectile;

	void Start () {
        base.Start();
	}
	
	void Update () {
        if (!IsDead)
        {
            SetHealthVisual(currentHealth / maxHealth);
            UpdateEnemyList();
            FindClosestEnemy();

            if (currentHealth <= 300)
            {
                cooldown = 3;
            }

            if (closestPlayer != null)
            {
                //start attacking it
                if (Time.time >= nextActionTime)
                {
                    nextActionTime = Time.time + cooldown;

                    int tempNum = Random.Range(1, 101);
                    if (tempNum <= 100)
                        Action();
                    else
                        specialAction();
                }
            }
        }
	}

    protected override void specialAction()
    {
    }

    protected override void Action()
    {
        Vector3 dir = closestPlayer.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject shoot = Instantiate(projectile, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        shoot.SendMessage("Initialize", closestPlayer.transform.position);
        shoot.SendMessage("initDamage", damage);
    }
}
