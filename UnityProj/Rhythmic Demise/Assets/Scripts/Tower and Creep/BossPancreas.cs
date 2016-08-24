using UnityEngine;
using System.Collections;

public class BossPancreas : Boss {

    public GameObject projectile;

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
            c.setArmor(c.getArmor() - 5);

            c.setDamage(c.getDamage() - 5);
        }
    }

    protected override void Action()
    {
        anim.SetTrigger("Attack");
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
