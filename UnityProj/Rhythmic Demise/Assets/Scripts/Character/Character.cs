using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public abstract class Character : MonoBehaviour {

    protected float movementSpeed;
    protected float currentHealth;
    protected float maxHealth;
    protected float damage;
    protected float armor;
    protected Enums.SkillName skill;
    protected Enums.CharacterType race;
    protected Enums.JobType job;
    protected bool isAttacking;
    protected bool isDefending;
    protected Vector3 goalPos;
    protected bool inPath;
    protected float nextSkillTime;
    
    public Animator anim;

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
        inPath = true;
        anim = gameObject.GetComponent<Animator>();
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

    public void setCurrentState(Enums.PlayerState currAct)
    {
        currentAction = currAct;
    }
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //add enemy to enemyList if in range of player range
        if (other.tag == "Enemy")
        {
            ArmyController.armyController.addEnemyList(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            ArmyController.armyController.removeEnemyList(other.gameObject);
        }
    }

    public void reset()
    {
        isAttacking = false;
        inPath = true;

        if (isDefending)
        {
            isDefending = false;
            armor /= 2;
        }
    }

    public void SetHealthVisual(float healthNormalized)
    {
        healthBar.transform.localScale = new Vector3(healthNormalized,
                                                     healthBar.transform.localScale.y,
                                                     healthBar.transform.localScale.z);
    }

    public void TakeDamage(float damage)
    {   
        
        float damageMultiplier = 1 - 0.06f * armor / (1 + (0.06f * Mathf.Abs(armor)));
        float finalDamage = damage * damageMultiplier;
        finalDamage = Mathf.CeilToInt(finalDamage);
        //finalDamage = Mathf.Round(finalDamage * 100f) / 100f;
        currentHealth -= finalDamage;

        FloatingTextController.CreateFloatingText(finalDamage.ToString(), transform);
        //currentHealth -= damage;

        //FloatingTextController.CreateFloatingText(damage.ToString(), transform);
    }

    public Enums.JobType getJobType()
    {
        return job;
    }

    public Vector3 getGoalPos()
    {
        return goalPos;
    }

    public bool getInPath()
    {
        return inPath;
    }

    public float getDamage()
    {
        return damage;
    }

    public float getArmor()
    {
        return armor;
    }

    public float getHealth()
    {
        return currentHealth;
    }

    public void addMaxHealth(float mh)
    {
        maxHealth += mh;

        currentHealth = maxHealth;
    }

    public void setGoalPos(Vector3 pos)
    {
        goalPos = pos;
    }

    public void setInPath(bool p)
    {
        inPath = p;
    }

    public void setDamage(float d)
    {
        damage = d;
    }

    public void setArmor(float a)
    {
        armor = a;
    }

    public void addHealth(float h)
    {
        currentHealth += h;

        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
    }

    public abstract void attack();
    public abstract void useSkill();
    public abstract void defend();
}
