using UnityEngine;
using System.Collections;

public class BossPancreas : Boss {
    private int algoBalancer;

	void Start () {
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

                    int tempNum = Random.Range(1, 101);
                    if (tempNum <= 85)
                        Action();
                    else
                    {
                        if (algoBalancer > 2)
                        {
                            Action();
                        }
                        else
                        {
                            specialAction();
                        }
                    }
                }
            }
        }
	}

    protected override void specialAction()
    {
        algoBalancer++;

        foreach (GameObject go in playerList)
        {
            Character c = go.GetComponent<Character>();

            //reduce army's power
            c.setArmor(c.getArmor() - 2);

            c.setDamage(c.getDamage() - 3);
        }
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
