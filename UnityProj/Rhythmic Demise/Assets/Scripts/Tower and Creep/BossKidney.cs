using UnityEngine;
using System.Collections;

public class BossKidney : Boss {

    public GameObject projectile;
    public GameObject troopPrefab; //wbc

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
        anim.SetTrigger("Skill");
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

    void triggerSkill()
    {
        int randNum = Random.Range(0, playerList.Count);

        //destroy one random character
        Character c = playerList[randNum].GetComponent<Character>();
        Vector3 tempPos = playerList[randNum].transform.position;

        playerList.Remove(playerList[randNum]);
        ArmyController.armyController.army.Remove(c);
        GameController.gameController.updateUI();

        Destroy(c.gameObject);

        //spawn wbc
        Vector3 dir = closestPlayer.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject spawn = Instantiate(troopPrefab, tempPos, Quaternion.Euler(0, 0, angle)) as GameObject;

        spawn.SendMessage("Initialize", closestPlayer);

        spawn.SendMessage("initHealth", troopHealth);
        spawn.SendMessage("initDamage", troopDamage);
        spawn.SendMessage("initCooldown", troopCoolDown);
    }
}
