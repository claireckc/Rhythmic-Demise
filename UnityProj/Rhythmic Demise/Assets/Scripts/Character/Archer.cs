using UnityEngine;
using System.Collections;

public class Archer : Character {

    public GameObject arrow;
    public GameObject focusArrow;

	// Use this for initialization
	protected new void Start () {
        base.Start();

        isAttacking = false;
        skill = "FocusArrow";
        job = Enums.JobType.Archer;

        //1 is archer index
        currentHealth = maxHealth = PlayerScript.playerdata.troopData[1].maxHealth;
        damage = PlayerScript.playerdata.troopData[1].attack;
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
                shoot.SendMessage("Initialize", ArmyController.armyController.closestEnemy);
                shoot.SendMessage("initDamage", 1);

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
                    shoot.SendMessage("Initialize", ArmyController.armyController.closestEnemy);
                    shoot.SendMessage("initDamage", 2);

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
