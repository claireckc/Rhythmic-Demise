using UnityEngine;
using System.Collections;

public class BossLargeIntestine : Boss {

    public GameObject projectile;
    public GameObject spellCircle;
    public GameObject[] spellLocation;
    public GameObject spellPrefab;

    private float launchAttackTime;
    private GameObject targetPos;
    private bool attacked;

	void Start () {
        base.Start();

        attacked = true;
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
                    if (tempNum <= 80)
                        Action();
                    else
                        specialAction();
                }

                if (Time.time >= launchAttackTime && !attacked)
                {
                    attacked = true;

                    GameObject sc = Instantiate(spellPrefab, targetPos.transform.position, spellPrefab.transform.rotation) as GameObject;
                    Destroy(sc, 2);

                    if (ArmyController.armyController.currPos == targetPos)
                    {
                        ArmyController.armyController.takeDamage(damage * 2);
                    }
                }
            }
        }
	}

    protected override void specialAction()
    {
        int index = Random.Range(0, spellLocation.Length);

        targetPos = spellLocation[index];

        GameObject sc = Instantiate(spellCircle, targetPos.transform.position, spellCircle.transform.rotation) as GameObject;
        launchAttackTime = Time.time + 2;
        Destroy(sc, 2);
        attacked = false;
    }

    protected override void Action()
    {
        Vector3 dir = closestPlayer.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject shoot = Instantiate(projectile, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        shoot.SendMessage("Initialize", closestPlayer.transform.position);
        shoot.SendMessage("initDamage", damage);
    }
}
