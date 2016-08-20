using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour {

    protected float currentHealth;
    public float maxHealth;
    public float damage;
    public float cooldown;
    protected float nextActionTime;

    protected float closestDist;
    protected GameObject firstPlayer;
    protected GameObject closestPlayer;
    protected GameObject toRemove;
    public List<GameObject> playerList = new List<GameObject>();

    public GameObject healthBar;

    public bool IsDead
    {
        get
        {
            return currentHealth <= 0;
        }
    }
 
    protected abstract void Action();
    protected abstract void FindClosestEnemy();
    protected abstract void UpdateEnemyList();
    protected abstract void OnTriggerEnter2D(Collider2D other);
    protected abstract void OnTriggerExit2D(Collider2D other);
    public abstract void TakeDamage(float damage);
    public abstract void disabled(float duration);
    public abstract void SetHealthVisual(float healthNormalized);
}
