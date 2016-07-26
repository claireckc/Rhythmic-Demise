using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Enemy : MonoBehaviour {

    protected float currentHealth;
    protected float maxHealth;
    protected float damage;
    protected float cooldown;
    protected float nextActionTime;

    protected float closestDist;
    protected GameObject firstPlayer;
    protected GameObject closestPlayer;
    protected GameObject toRemove;
    protected List<GameObject> playerList;

    public GameObject healthBar;

    public bool IsDead
    {
        get
        {
            return currentHealth <= 0;
        }
    }
 
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    protected abstract void Action();
    protected abstract void FindClosestEnemy();
    protected abstract void UpdateEnemyList();
    protected abstract void OnTriggerEnter2D(Collider2D other);
    protected abstract void OnTriggerExit2D(Collider2D other);
    public abstract void TakeDamage(float damage);
    public abstract void disabled(float duration);
}
