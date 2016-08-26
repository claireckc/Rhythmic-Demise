using UnityEngine;
using System.Collections;

public class Knight : Character {

    public GameObject swordWave;

	// Use this for initialization
	protected new void Start () {
        base.Start();

        job = Enums.JobType.Knight;
        
        //0 is knight index
        currentHealth = maxHealth = PlayerScript.playerdata.troopData[0].maxHealth;
        damage = PlayerScript.playerdata.troopData[0].damage;
        armor = PlayerScript.playerdata.troopData[0].armor;
        skill = PlayerScript.playerdata.skillSelected;

        if (skill == Enums.SkillName.KnightDefbuff)
        {
            ArmyController.armyController.initSkillBonus(0); // 1 for archer
        }
	}
	
	// Update is called once per frame
	protected new void Update () {
        base.Update();
	}

    public override void attack()
    {
        if (ArmyController.armyController.closestEnemy != null)
        {
            //float distance = Vector3.Distance(transform.position, ArmyController.armyController.closestEnemy.transform.position);
            float distance = Vector3.Distance(transform.position, ArmyController.armyController.closestEnemy.transform.GetChild(0).position);
            
            if (distance < 2) //if enemy is already within attack range
            {
                if (!isAttacking)
                {
                    if (ArmyController.armyController.enemyList.Count > 0)
                    {
                        //Enemy enemy = ArmyController.armyController.closestEnemy.GetComponent<Enemy>();
                        //enemy.TakeDamage(damage);
                        isAttacking = true;

                        //set attack animation
                        anim.SetTrigger("Attack");
                    }
                }
            }
            else //move while enemy not in attack range
            {   
                Vector3 dir = ArmyController.armyController.closestEnemy.transform.position - transform.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                transform.Translate(new Vector3(movementSpeed * Time.deltaTime, 0, 0));
            }
        }
        else
        {
            //Debug.Log("No enemy in range");
        }
    }

    public override void useSkill()
    {
        if (Time.time >= nextSkillTime)
        {
            if (skill == Enums.SkillName.KnightHigh)
            {
                nextSkillTime = Time.time + PlayerScript.playerdata.troopData[0].skills[1].skillCooldown;

                if (ArmyController.armyController.closestEnemy != null)
                {
                    //float distance = Vector3.Distance(transform.position, ArmyController.armyController.closestEnemy.transform.position);
                    float distance = Vector3.Distance(transform.position, ArmyController.armyController.closestEnemy.transform.GetChild(0).position);

                    if (distance < 3) //if enemy is already within attack range
                    {
                        if (!isAttacking)
                        {
                            if (ArmyController.armyController.enemyList.Count > 0)
                            {
                                isAttacking = true;

                                anim.SetTrigger("SkillBash");
                            }
                        }
                    }
                    else //move while enemy not in attack range
                    {
                        Vector3 dir = ArmyController.armyController.closestEnemy.transform.position - transform.position;
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                        transform.Translate(new Vector3(movementSpeed * Time.deltaTime, 0, 0));
                    }
                }
                else
                {
                    //Debug.Log("No enemy in range");
                }
            }
            else if (skill == Enums.SkillName.KnightCharge)
            {
                nextSkillTime = Time.time + PlayerScript.playerdata.troopData[0].skills[0].skillCooldown;

                if (!isAttacking)
                {
                    if (ArmyController.armyController.enemyList.Count > 0)
                    {
                        isAttacking = true;

                        anim.SetTrigger("SkillAOE");
                    }
                    else
                    {
                        Debug.Log("No enemy in range");
                    }
                }
            }
        }
    }

    void hitEnemy()
    {
        if (ArmyController.armyController.closestEnemy != null)
        {
            Enemy enemy = ArmyController.armyController.closestEnemy.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
        }
    }

    void SkillAOE()
    {
        Vector3 dir = ArmyController.armyController.closestEnemy.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject wave = Instantiate(swordWave, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        wave.SendMessage("initDamage", damage * 2);
    }

    public override void defend()
    {
        if (!isDefending)
        {
            isDefending = true;

            armor *= 2;

            anim.SetTrigger("Defend");
        }
    }
}
