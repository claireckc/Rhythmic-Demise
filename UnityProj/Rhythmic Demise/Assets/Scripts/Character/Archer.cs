using UnityEngine;
using System.Collections;

public class Archer : Character {

    public GameObject arrow;
    public GameObject focusArrow;

	// Use this for initialization
	protected new void Start () {
        base.Start();

        isAttacking = false;
        skill = "RainArrow";
        job = Enums.JobType.Archer;
        
        //1 is archer index
        currentHealth = maxHealth = PlayerScript.playerdata.troopData[1].maxHealth;
        damage = PlayerScript.playerdata.troopData[1].damage;
        armor = PlayerScript.playerdata.troopData[1].armor;
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
                //Debug.Log("Attack!");
                //start attack animation and instatiate projectile

                Vector3 dir = ArmyController.armyController.closestEnemy.transform.position - this.transform.position;
                float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

                GameObject shoot = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
                shoot.SendMessage("Initialize", ArmyController.armyController.closestEnemy.transform.position);
                shoot.SendMessage("initDamage", damage);

                isAttacking = true;
            }
            else
            {
                //Debug.Log("No enemy in range");
            }
        }
    }

    public override void useSkill()
    {
        if (skill.Equals("FocusArrow"))
        {
            if (!isAttacking)
            {
                if (ArmyController.armyController.enemyList.Count > 0)
                {
                    //Debug.Log("Attack!");
                    //start attack animation and instatiate projectile

                    Vector3 dir = ArmyController.armyController.closestEnemy.transform.position - this.transform.position;
                    float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

                    GameObject shoot = Instantiate(focusArrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
                    shoot.SendMessage("Initialize", ArmyController.armyController.closestEnemy.transform.position);
                    shoot.SendMessage("initDamage", damage * 2);
                    
                    isAttacking = true;
                }
                else
                {
                    Debug.Log("No enemy in range");
                }
            }
        }
        else if (skill.Equals("RainArrow"))
        {
            if (!isAttacking)
            {
                if (ArmyController.armyController.enemyList.Count > 0)
                {
                    //Debug.Log("Attack!");
                    //start attack animation and instatiate projectile

                    Vector3 dir = ArmyController.armyController.closestEnemy.transform.position - this.transform.position;
                    float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

                    GameObject shoot1 = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
                    GameObject shoot2 = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
                    GameObject shoot3 = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
                    GameObject shoot4 = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
                    GameObject shoot5 = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
                    shoot1.SendMessage("Initialize", ArmyController.armyController.closestEnemy.transform.position + new Vector3(1, 1, 0));
                    shoot2.SendMessage("Initialize", ArmyController.armyController.closestEnemy.transform.position + new Vector3(1, -1, 0));
                    shoot3.SendMessage("Initialize", ArmyController.armyController.closestEnemy.transform.position + new Vector3(0, 0, 0));
                    shoot4.SendMessage("Initialize", ArmyController.armyController.closestEnemy.transform.position + new Vector3(-1, -1, 0));
                    shoot5.SendMessage("Initialize", ArmyController.armyController.closestEnemy.transform.position + new Vector3(-1, 1, 0));
                    shoot1.SendMessage("initDamage", damage);
                    shoot2.SendMessage("initDamage", damage);
                    shoot3.SendMessage("initDamage", damage);
                    shoot4.SendMessage("initDamage", damage);
                    shoot5.SendMessage("initDamage", damage);

                    isAttacking = true;
                }
                else
                {
                    Debug.Log("No enemy in range");
                }
            }
        }
    }
}
