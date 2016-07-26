using UnityEngine;
using System.Collections;

public class Priest : Character {

    public GameObject orb;
    private int hexDuration;

	// Use this for initialization
	protected new void Start () {
        base.Start();

        isAttacking = false;
        skill = "Hex";
        job = Enums.JobType.Priest;

        //2 is priest index
        currentHealth = maxHealth = PlayerScript.playerdata.troopData[2].maxHealth;
        damage = PlayerScript.playerdata.troopData[2].attack;
        hexDuration = 3;
	}
	
	// Update is called once per frame
	protected new void Update () {
        base.Update();
	}

    public override void attack()
    {
        if (!isAttacking)
        {
            if (ArmyController.enemyList.Count > 0)
            {
                //Debug.Log("Attack!");
                //start attack animation and instatiate projectile

                Vector3 dir = ArmyController.closestEnemy.transform.position - this.transform.position;
                float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

                GameObject shoot = Instantiate(orb, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
                shoot.SendMessage("Initialize", ArmyController.closestEnemy);
                shoot.SendMessage("initDamage", 0.5f);

                isAttacking = true;
            }
            else
            {
                Debug.Log("No enemy in range");
            }
        }
    }

    public override void useSkill()
    {
        if(skill.Equals("Hex"))
        {
            if (!isAttacking)
            {
                if (ArmyController.closestEnemy.tag == "Enemy")
                {
                    Enemy enemy = ArmyController.closestEnemy.GetComponent<Enemy>();
                    enemy.disabled(hexDuration);
                    isAttacking = true;
                }
            }
        }
    }
}
