using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy : MonoBehaviour {

    protected float currentHealth;
    protected float maxHealth;
    protected float damage;

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
}
