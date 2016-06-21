using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

    protected float movementSpeed;
    protected float health;
    protected float damage;
    protected string skill;
    protected Enums.CharacterType type;
    protected bool isAttacking;

    public float closestDist;
    public GameObject firstEnemy;
    public GameObject closestEnemy;

    public Enums.PlayerState currentAction;
    public List<GameObject> enemyList;

	// Use this for initialization
	void Start () {
        movementSpeed = 0.5f;
        damage = 1;
        enemyList = new List<GameObject>();
	}
	
	// Update is called once per frame
    protected void Update()
    {

    }

    public void moveUp()
    {
        transform.Translate(Vector2.up * (movementSpeed * Time.deltaTime));
    }

    public void moveDown()
    {
        transform.Translate(Vector2.down * (movementSpeed * Time.deltaTime));
    }

    public void moveLeft()
    {
        transform.Translate(Vector2.left * (movementSpeed * Time.deltaTime));
    }

    public void moveRight()
    {
        transform.Translate(Vector2.right * (movementSpeed * Time.deltaTime));
    }

    public void attack()
    {
    }

    public void setCurrentState(Enums.PlayerState currAct)
    {
        currentAction = currAct;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log ("Enter" + other.gameObject.tag);

        //add enemy to enemyList if in range of player range
        if (other.gameObject.tag.Contains("Tower"))
        {
            enemyList.Add(other.transform.parent.gameObject);
        }
    }

    public void findClosestEnemy()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            TowerAI e = enemyList[i].GetComponent<TowerAI>();

            if (e.IsDead)
            {
                enemyList.Remove(enemyList[i]);

                Destroy(e.gameObject);
            }
        }

        if (enemyList.Count > 0)
        {
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

    public void reset()
    {
        isAttacking = false;
    }
}
