using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour {

    protected float movementSpeed;
    protected float currentHealth;
    protected float maxHealth;
    protected float damage;
    protected string skill;
    protected Enums.CharacterType race;
    protected Enums.JobType job;
    protected bool isAttacking;
    protected Vector3 goalPos;

    public Enums.PlayerState currentAction;

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
        movementSpeed = 2f;
	}

	// Update is called once per frame
    protected void Update()
    {
        SetHealthVisual(currentHealth / maxHealth);
    }

    public void moveTo(Vector3 pos)
    {
        transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        transform.position = Vector3.MoveTowards(transform.position, pos, movementSpeed * Time.deltaTime);
    }

    public abstract void attack();
    public abstract void useSkill();

    public void setCurrentState(Enums.PlayerState currAct)
    {
        currentAction = currAct;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //add enemy to enemyList if in range of player range
        if (other.tag == "Enemy")
        {
            ArmyController.addEnemyList(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            ArmyController.removeEnemyList(other.gameObject);
        }
    }

    public void reset()
    {
        isAttacking = false;
    }

    public void SetHealthVisual(float healthNormalized)
    {
        healthBar.transform.localScale = new Vector3(healthNormalized,
                                                     healthBar.transform.localScale.y,
                                                     healthBar.transform.localScale.z);
    }

    public void TakeDamage(float damage)
    {
        FloatingTextController.CreateFloatingText(damage.ToString(), transform);
        currentHealth -= damage;
    }

    public Enums.JobType getJobType()
    {
        return job;
    }

    public Vector3 getGoalPos()
    {
        return goalPos;
    }

    public void setGoalPos(Vector3 pos)
    {
        goalPos = pos;
    }
    /*
  public IEnumerator MoveOverSpeed (GameObject objectToMove, Vector3 end, float speed){
     // speed should be 1 unit per second
     while (objectToMove.transform.position != end)
     {
         objectToMove.transform.position = Vector3.MoveTowards(objectToMove.transform.position, end, speed * Time.deltaTime);

         yield return new WaitForEndOfFrame ();
     }
 }
 public IEnumerator MoveOverSeconds (GameObject objectToMove, Vector3 end, float seconds)
 {
     float elapsedTime = 0;
     Vector3 startingPos = objectToMove.transform.position;
     while (elapsedTime < seconds)
     {
         objectToMove.transform.position = Vector3.Lerp(startingPos, end, (elapsedTime / seconds));
         elapsedTime += Time.deltaTime;
         yield return new WaitForEndOfFrame();
     }
     objectToMove.transform.position = end;
 }
    */
}
