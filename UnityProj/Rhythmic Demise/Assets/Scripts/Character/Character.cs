using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

    protected float movementSpeed;
    protected float health;
    protected float damage;
    protected string skill;
    protected Enums.CharacterType type;

    public Enums.PlayerState currentAction;
    public List<GameObject> enemyList;

	// Use this for initialization
	void Start () {
        movementSpeed = 0.5f;
        damage = 1;
        enemyList = new List<GameObject>();
	}
	
	// Update is called once per frame
	protected void Update () {
        switch (currentAction)
        {
            case Enums.PlayerState.MoveUp:
                moveUp();
                break;
            case Enums.PlayerState.MoveDown:
                moveDown();
                break;
            case Enums.PlayerState.MoveLeft:
                moveLeft();
                break;
            case Enums.PlayerState.MoveRight:
                moveRight();
                break;
            case Enums.PlayerState.Attack:
                attack();
                break;
        }
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
        if (enemyList.Count > 0)
        {
            Debug.Log("Attack!");
            //start attack animation and instatiate projectile
        }
        else
        {
            Debug.Log("No enemy in range");
        }
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
}
