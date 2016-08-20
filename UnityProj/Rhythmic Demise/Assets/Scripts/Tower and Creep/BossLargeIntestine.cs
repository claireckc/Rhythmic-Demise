using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossLargeIntestine : Boss {

    public GameObject projectile;
    public GameObject spellCircle;
    public GameObject[] spellLocation;

    private float launchAttackTime;
    private GameObject targetPos;
    private bool attacked;

	void Start () {
        currentHealth = maxHealth = 20;
        playerList = new List<GameObject>();
        cooldown = nextActionTime = 7.0f;
        launchAttackTime = nextActionTime + 2;
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

                    int index = Random.Range(0, spellLocation.Length);

                    targetPos = spellLocation[index];

                    GameObject sc = Instantiate(spellCircle, targetPos.transform.position, spellCircle.transform.rotation) as GameObject;
                    launchAttackTime = Time.time + 2;
                    Destroy(sc, 2);
                    attacked = false;
                }

                if (Time.time >= launchAttackTime && !attacked)
                {
                    attacked = true;

                    Vector3 dir = closestPlayer.transform.position - this.transform.position;
                    float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

                    GameObject shoot = Instantiate(projectile, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
                    shoot.SendMessage("Initialize", targetPos.transform.position);
                }
            }
        }
	}

    protected override void specialAction()
    {
    }

    protected override void Action()
    {
    }
}
