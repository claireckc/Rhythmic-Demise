using UnityEngine;
using System.Collections;

public class BossSpleen : Boss {

    public GameObject troopPrefab; //rbc
    public GameObject spawnPoint;

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

                    if (tempNum <= 15)
                        regenerate();
                    else
                        specialAction();
                }
            }
        }
	}

    void regenerate()
    {
        currentHealth += 100;

        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;

        anim.SetTrigger("Regen");
    }

    protected override void specialAction()
    {
       Vector3 dir = closestPlayer.transform.position - this.transform.position;
       float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

       GameObject spawn = Instantiate(troopPrefab, spawnPoint.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;

       spawn.SendMessage("Initialize", closestPlayer);

       spawn.SendMessage("initHealth", troopHealth);
       spawn.SendMessage("initDamage", troopDamage);
       spawn.SendMessage("initCooldown", troopCoolDown);

        anim.SetTrigger("Summon");
    }

    protected override void Action()
    {
    }
}
