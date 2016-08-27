using UnityEngine;
using System.Collections;

public class Priest : Character {

    public GameObject orb;

    float healPower;

	// Use this for initialization
	protected new void Start () {
        base.Start();

        isAttacking = false;
        job = Enums.JobType.Priest;

        //2 is priest index
        currentHealth = maxHealth = PlayerScript.playerdata.troopData[2].maxHealth;
        damage = PlayerScript.playerdata.troopData[2].damage;
        armor = PlayerScript.playerdata.troopData[2].armor;
        skill = PlayerScript.playerdata.skillSelected;

        healPower = PlayerScript.playerdata.troopData[2].skills[0].skillValue;

        if (skill == Enums.SkillName.PriestHealBuff)
        {
            ArmyController.armyController.initSkillBonus(2); // 1 for archer
        }
	}
	
	// Update is called once per frame
	protected new void Update () {
        base.Update();
	}

    public override void attack()
    {
        if (!isAttacking)
        {
            if (ArmyController.armyController.enemyList.Count > 0)
            {
                isAttacking = true;

                anim.SetTrigger("Attack");
            }
            else
            {
                
            }
        }
    }

    public override void useSkill()
    {
        if (Time.time >= nextSkillTime)
        {
            if (skill == Enums.SkillName.PriestHex)
            {
                nextSkillTime = Time.time + PlayerScript.playerdata.troopData[0].skills[1].skillCooldown;

                if (!isAttacking)
                {
                    if (ArmyController.armyController.enemyList.Count > 0)
                    {
                        Enemy enemy = ArmyController.armyController.closestEnemy.GetComponent<Enemy>();
                        enemy.disabled(PlayerScript.playerdata.troopData[2].skills[1].skillValue);
                        isAttacking = true;

                        anim.SetTrigger("Disable");
                    }
                }
            }
            else if (skill == Enums.SkillName.PriestHeal)
            {
                nextSkillTime = Time.time + PlayerScript.playerdata.troopData[0].skills[0].skillCooldown;

                if (!isAttacking)
                {
                    if (ArmyController.armyController.enemyList.Count > 0)
                    {
                        ArmyController.armyController.healArmy(healPower);
                        isAttacking = true;

                        anim.SetTrigger("Heal");
                    }
                }
            }
        }
    }
    void spawnOrb()
    {
        Vector3 dir = ArmyController.armyController.closestEnemy.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject shoot = Instantiate(orb, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        shoot.SendMessage("Initialize", ArmyController.armyController.closestEnemy);
        shoot.SendMessage("initDamage", damage);
    }

    public void increaseHealPower(float hp)
    {
        healPower += hp;
    }

    public override void defend()
    {
        if (!isDefending)
        {
            isDefending = true;

            armor *= 2;

            //anim.SetTrigger("Defend");
        }
    }
}
