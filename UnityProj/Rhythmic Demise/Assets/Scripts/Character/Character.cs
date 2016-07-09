using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Character : MonoBehaviour {

    protected float movementSpeed;
    protected float currentHealth;
    protected float maxHealth;
    protected float damage;
    protected string skill;
    protected Enums.CharacterType race;
    protected Enums.JobType job;
    protected bool isAttacking;

    public float closestDist;
    public GameObject firstEnemy;
    public GameObject closestEnemy;

    public Enums.PlayerState currentAction;
    public List<GameObject> enemyList;

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
        currentHealth = maxHealth = 10;
        movementSpeed = 1f;
        damage = 1;
        enemyList = new List<GameObject>();
	}
	
	// Update is called once per frame
    protected void Update()
    {
        if (IsDead)
        {
        }

        SetHealthVisual(currentHealth / maxHealth);
    }

    public void moveTo(MovingPoint pos)
    {
        transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
        StartCoroutine(MoveOverSpeed(gameObject, pos.transform.position, movementSpeed));
    }

    public void attack()
    {
    }

    public void setCurrentState(Enums.PlayerState currAct)
    {
        currentAction = currAct;
    }

    protected void UpdateEnemyList()
    {
        for (int i = 0; i < enemyList.Count; i++)
        {
            Enemy e = enemyList[i].GetComponent<Enemy>();

            if (e.IsDead)
            {
                enemyList.Remove(enemyList[i]);

                Destroy(e.gameObject);

                //add score
                ScoreManager.score += 10;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log ("Enter" + other.gameObject.tag);

        //add enemy to enemyList if in range of player range
        if (other.tag == "Enemy")
        {
            enemyList.Add(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Remove(other.gameObject);
        }
    }

    public void findClosestEnemy()
    {
        UpdateEnemyList();

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

    // Health between [0.0f,1.0f] == (currentHealth / totalHealth)
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
}
