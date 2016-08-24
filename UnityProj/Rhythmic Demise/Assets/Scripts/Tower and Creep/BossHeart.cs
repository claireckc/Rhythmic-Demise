using UnityEngine;
using System.Collections;

public class BossHeart : Boss{

    public GameObject projectile;
    public GameObject troopPrefab; //rbc
    public GameObject[] spawnPoint;

    public float troopDamage;
    public float troopHealth;
    public float troopCoolDown;

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
                    if (tempNum <= 75)
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

            GameObject spawn = Instantiate(troopPrefab, spawnPoint[i].transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
            
            spawn.SendMessage("Initialize", closestPlayer);

            spawn.SendMessage("initHealth", troopHealth);
            spawn.SendMessage("initDamage", troopDamage);
            spawn.SendMessage("initCooldown", troopCoolDown);
        }
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
