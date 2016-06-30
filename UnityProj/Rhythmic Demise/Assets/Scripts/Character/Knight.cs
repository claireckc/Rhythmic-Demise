using UnityEngine;
using System.Collections;

public class Knight : Character {

	// Use this for initialization
	protected new void Start () {
        base.Start();

        movementSpeed = 1f;
        job = Enums.JobType.Knight;
	}
	
	// Update is called once per frame
	protected new void Update () {
        base.Update();

        findClosestEnemy();
        switch (currentAction)
        {
            case Enums.PlayerState.MoveUp:
                //moveUp();
                break;
            case Enums.PlayerState.MoveDown:
                //moveDown();
                break;
            case Enums.PlayerState.MoveLeft:
                //moveLeft();
                break;
            case Enums.PlayerState.MoveRight:
                //moveRight();
                break;
            case Enums.PlayerState.Attack:
                if (closestEnemy != null)
                {
                    float distance = Vector3.Distance(transform.position, closestEnemy.transform.position);
                    if (distance < 3)
                    {
                        attack();
                    }
                    else
                    {
                        Vector3 dir = closestEnemy.transform.position - transform.position;
                        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                        transform.Translate(new Vector3(movementSpeed * Time.deltaTime, 0, 0));
                    }

                }
                else
                {
                    Debug.Log("No enemy in range");
                }
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
                TowerAI enemy = closestEnemy.GetComponent<TowerAI>();
                enemy.TakeDamage(damage);
                isAttacking = true;
            }
        }
    }

    public void useSkill()
    {

    }
}
