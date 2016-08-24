using UnityEngine;
using System.Collections;

public class Uvula : Boss {
    public GameObject projectile;

    public void Start()
    {
        base.Start();
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
                    
                    Action();
                }
            }
        }
	}

    protected override void Action()
    {

        anim.SetTrigger("Attack");
    }

    protected override void specialAction()
    {
        
    }

    void hitEnemy()
    {
        Vector3 dir = closestPlayer.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject shoot = Instantiate(projectile, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        shoot.SendMessage("Initialize", closestPlayer.transform.position);
        shoot.SendMessage("initDamage", damage);
    }
}
