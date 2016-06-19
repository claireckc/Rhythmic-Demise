using UnityEngine;
using System.Collections;

public class Character : MonoBehaviour {

    protected float movementSpeed;
    protected float health;
    protected string skill;

    public string currentAction;

	// Use this for initialization
	void Start () {
        movementSpeed = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
        if(currentAction.Equals("moveUp"))
        {
            moveUp();
        }
        else if (currentAction.Equals("moveDown"))
        {
            moveDown();
        }
        else if (currentAction.Equals("moveLeft"))
        {
            moveLeft();
        }
        else if (currentAction.Equals("moveRight"))
        {
            moveRight();
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

    public void setCurrentAction(string currAct)
    {
        currentAction = currAct;
    }
}
