using UnityEngine;
using System.Collections;

public class Priest : Character {

    public GameObject orb;

	// Use this for initialization
	protected new void Start () {
        base.Start();

        movementSpeed = 1f;
        isAttacking = false;
        skill = "Hex";
        job = Enums.JobType.Priest;
	}
	
	// Update is called once per frame
	protected new void Update () {
        base.Update();

        findClosestEnemy();

        switch (currentAction)
        {
            case Enums.PlayerState.MoveUp:
                moveUp();
                break;
            case Enums.PlayerState.MoveDown:
                moveDown();
                break;
            case Enums.PlayerState.MoveLeft:
                moveLeft();
                break;
            case Enums.PlayerState.MoveRight:
                moveRight();
                break;
            case Enums.PlayerState.Attack:
                attack();
                break;
            case Enums.PlayerState.Skill:
                useSkill();
                break;
        }
	}

    public new void attack()
    {
        if (!isAttacking)
        {
            if (enemyList.Count > 0)
            {
                //Debug.Log("Attack!");
                //start attack animation and instatiate projectile

                Vector3 dir = closestEnemy.transform.position - this.transform.position;
                float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

                GameObject shoot = Instantiate(orb, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
                shoot.SendMessage("Initialize", closestEnemy);
                shoot.SendMessage("initDamage", 0.5f);

                isAttacking = true;
            }
            else
            {
                Debug.Log("No enemy in range");
            }
        }
    }

    public void useSkill()
    {
        if(skill.Equals("Hex"))
        {
            if (!isAttacking)
            {
                if (closestEnemy.tag == "Tower")
                {
                    TowerAI tower = closestEnemy.GetComponent<TowerAI>();
                    tower.nextFireTime += 4;
                    isAttacking = true;
                }
            }
        }
    }
}
