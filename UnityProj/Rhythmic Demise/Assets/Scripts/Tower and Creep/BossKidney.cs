using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BossKidney : Boss {

    public GameObject projectile;
    public GameObject troopPrefab; //wbc

	void Start () {
        currentHealth = maxHealth = 20;
        playerList = new List<GameObject>();
        cooldown = nextActionTime = 5.0f;
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
                    if (tempNum <= 85)
                        Action();
                    else
                        specialAction();
                }
            }
        }
	}

    protected override void specialAction()
    {
        int randNum = Random.Range(0, playerList.Count);

        //destroy one random character
        Character c = playerList[randNum].GetComponent<Character>();
        Vector3 tempPos = playerList[randNum].transform.position;

        playerList.Remove(playerList[randNum]);
        ArmyController.armyController.army.Remove(c);
        GameController.gameController.updateUI();

        Destroy(c.gameObject);

        //spawn wbc
        Vector3 dir = closestPlayer.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject spawn = Instantiate(troopPrefab, tempPos, Quaternion.Euler(0, 0, angle)) as GameObject;

        spawn.SendMessage("Initialize", closestPlayer);
    }

    protected override void Action()
    {
        Vector3 dir = closestPlayer.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject shoot = Instantiate(projectile, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        shoot.SendMessage("Initialize", closestPlayer);
    }
}
