using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tower : MonoBehaviour {

    //tower
    protected float currentHealth;
    protected float maxHealth;
    protected float damage;
    protected float cooldown, nextFireTime;
    protected float spawnTime, nextSpawnTime;
    protected float towerWidth, towerHeight;
    protected RectTransform size;

    //enemy
    protected float closestDist;
    protected GameObject firstEnemy;
    protected GameObject closestEnemy;
    protected GameObject toRemove;
    protected List<GameObject> enemyList;

    //tower inst
    public GameObject arrow, creep;

    public GameObject healthBar;

    public bool IsDead
    {
        get
        {
            return currentHealth <= 0;
        }
    }

	// Use this for initialization
	protected void Start () {
        currentHealth = maxHealth = 2;
        enemyList = new List<GameObject>();
        closestDist = 1000.0f;
        cooldown = nextFireTime = 5.0f;
        spawnTime = nextSpawnTime = 10.0f;
	}
	
	// Update is called once per frame
	protected void Update () {
        //Set health bar
        SetHealthVisual(currentHealth / maxHealth);

        //update enemy list
        for (int i = 0; i < enemyList.Count; i++)
        {
            Character c = enemyList[i].GetComponent<Character>();

            if (c.IsDead)
            {
                //Need to be re-arrange soon
                enemyList.Remove(enemyList[i]);
                GameController gc = GameObject.Find("GameController").GetComponent<GameController>();
                gc.army.Remove(c);
                gc.updateUI();

                Destroy(c.gameObject);
            }
        }

        if (enemyList.Count > 0)
        {
            //start finding the closest enemy
            firstEnemy = enemyList[0];
            closestDist = Vector2.Distance(this.transform.position, firstEnemy.transform.position);
            closestEnemy = firstEnemy;

            foreach (GameObject go in enemyList)
            {
                float currentDist = Vector2.Distance(this.transform.position, go.transform.position);
                if (closestDist > currentDist)
                {
                    closestDist = currentDist;
                    closestEnemy = go;
                    break;
                }
            }
        }
        else
            closestEnemy = null;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            foreach (GameObject go in enemyList)
            {
                if (other.gameObject == go)
                {
                    toRemove = go;
                    break;
                }
            }

            if (toRemove != null)
            {
                enemyList.Remove(toRemove);
            }

            //closestEnemy = null;
        }
    }

    protected void AttackEnemy()
    {
        nextFireTime = Time.time + cooldown;

        Vector3 dir = closestEnemy.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject shoot = Instantiate(arrow, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        shoot.SendMessage("Initialize", closestEnemy);
    }

    public void TakeDamage(float damage)
    {
        FloatingTextController.CreateFloatingText(damage.ToString(), transform);
        currentHealth -= damage;
    }

    // Health between [0.0f,1.0f] == (currentHealth / totalHealth)
    public void SetHealthVisual(float healthNormalized)
    {
        healthBar.transform.localScale = new Vector3(healthNormalized,
                                                     healthBar.transform.localScale.y,
                                                     healthBar.transform.localScale.z);
    }
}
