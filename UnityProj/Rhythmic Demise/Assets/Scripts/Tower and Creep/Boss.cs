﻿using UnityEngine;
using System.Collections;

public abstract class Boss : Enemy{
    protected abstract void specialAction();

    protected override void FindClosestEnemy()
    {
        if (playerList.Count > 0)
        {
            //start finding the closest enemy
            firstPlayer = playerList[0];
            closestDist = Vector2.Distance(this.transform.position, firstPlayer.transform.position);
            closestPlayer = firstPlayer;

            foreach (GameObject go in playerList)
            {
                float currentDist = Vector2.Distance(this.transform.position, go.transform.position);
                if (closestDist > currentDist)
                {
                    closestDist = currentDist;
                    closestPlayer = go;
                    break;
                }
            }
        }
        else
            closestPlayer = null;
    }

    protected override void UpdateEnemyList()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            Character c = playerList[i].GetComponent<Character>();

            if (c.IsDead)
            {
                //Need to be re-arrange soon
                playerList.Remove(playerList[i]);
                ArmyController.armyController.army.Remove(c);
                GameController.gameController.updateUI();

                Destroy(c.gameObject);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerColllider")
        {
            playerList.Add(other.gameObject);
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "PlayerCollider")
        {
            foreach (GameObject go in playerList)
            {
                if (other.gameObject == go)
                {
                    toRemove = go;
                    break;
                }
            }

            if (toRemove != null)
            {
                playerList.Remove(toRemove);
            }

            //closestEnemy = null;
        }
    }

    public override void TakeDamage(float damage)
    {
        FloatingTextController.CreateFloatingText(damage.ToString(), transform);
        currentHealth -= damage;
    }

    public override void disabled(float duration)
    {
        nextActionTime += duration;
    }

    public override void SetHealthVisual(float healthNormalized)
    {
        healthBar.transform.localScale = new Vector3(healthNormalized,
                                                     healthBar.transform.localScale.y,
                                                     healthBar.transform.localScale.z);
    }
}
