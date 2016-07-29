using UnityEngine;
using System.Collections;

public class Knight : Character {

	// Use this for initialization
	protected new void Start () {
        base.Start();

        job = Enums.JobType.Knight;

        //0 is knight index
        currentHealth = maxHealth = PlayerScript.playerdata.troopData[0].maxHealth;
        damage = PlayerScript.playerdata.troopData[0].attack;
	}
	
	// Update is called once per frame
	protected new void Update () {
        base.Update();
	}

    public override void attack()
    {
        if (ArmyController.armyController.closestEnemy != null)
        {
            float distance = Vector3.Distance(transform.position, ArmyController.armyController.closestEnemy.transform.position);
            if (distance < 2)
            {
                if (!isAttacking)
                {
                    if (ArmyController.armyController.enemyList.Count > 0)
                    {
                        Enemy enemy = ArmyController.armyController.closestEnemy.GetComponent<Enemy>();
                        enemy.TakeDamage(damage);
                        isAttacking = true;
                    }
                }
            }
            else
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

    }
}
