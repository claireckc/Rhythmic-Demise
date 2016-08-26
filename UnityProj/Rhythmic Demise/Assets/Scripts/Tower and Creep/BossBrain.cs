using UnityEngine;
using System.Collections;

public class BossBrain : Boss {
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
                cooldown = 4;
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
        anim.SetTrigger("Attack");
    }

    void hitEnemy()
    {
        Character c = closestPlayer.GetComponent<Character>();
        c.TakeDamage(damage);
    }
}
