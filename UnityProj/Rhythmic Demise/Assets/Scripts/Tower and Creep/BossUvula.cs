using UnityEngine;
using System.Collections;

public class BossUvula : Boss {

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
        Character c = closestPlayer.GetComponent<Character>();
        c.TakeDamage(damage);
    }
}
