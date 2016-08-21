using UnityEngine;
using System.Collections;

public class Creep : Enemy {

    private float movementSpeed;

	// Use this for initialization
	void Start () {
        movementSpeed = 1.0f;
	}

    void Initialize(GameObject target){
        playerList.Add(target);
        //closestPlayer = target;
    }

    void initHealth(float health)
    {
        this.currentHealth = this.maxHealth = health;
    }

    void initDamage(float d)
    {
        this.damage = d;
    }

    void initCooldown(float cd)
    {
        this.cooldown = cd;
    }

    void Update(){
        if (!IsDead)
        {
            SetHealthVisual(currentHealth / maxHealth);

            UpdateEnemyList();

            FindClosestEnemy();

            if (closestPlayer != null)
            {
                if ((transform.position - closestPlayer.transform.position).sqrMagnitude <= 1 * 1)
                {
                    //start attacking it
                    if (Time.time >= nextActionTime)
                    {
                        Action();
                    }
                }
                else
                {
                    Vector3 dir = closestPlayer.transform.position - this.transform.position;
                    float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
                    this.transform.rotation = Quaternion.Euler(0, 0, angle);
                    transform.position = Vector2.MoveTowards(transform.position, closestPlayer.transform.position, movementSpeed * Time.deltaTime);
                }
            }
        }
    }

    protected override void Action()
    {
        nextActionTime = Time.time + cooldown;

        //Attack animation start
        Character c = closestPlayer.GetComponent<Character>();
        c.TakeDamage(damage);
    }

    protected override void FindClosestEnemy()
    {
        if (playerList.Count > 0)
        {
            //start finding the closest enemy
            firstPlayer = playerList[0];
            closestDist = Vector2.Distance(this.transform.position, firstPlayer.transform.position);
            closestPlayer = firstPlayer;

            foreach (GameObject go in playerList)
            {
                float currentDist = Vector2.Distance(this.transform.position, go.transform.position);
                if (closestDist > currentDist)
                {
                    closestDist = currentDist;
                    closestPlayer = go;
                    break;
                }
            }
        }
        else
            closestPlayer = null;
    }

    protected override void UpdateEnemyList()
    {
        for (int i = 0; i < playerList.Count; i++)
        {
            if (playerList[i] == null)
            {
                playerList.Remove(playerList[i]);
            }
            else
            {
                Character c = playerList[i].GetComponent<Character>();

                if (c.IsDead)
                {
                    playerList.Remove(playerList[i]);
                    ArmyController.armyController.army.Remove(c);
                    GameController.gameController.updateUI();

                    Destroy(c.gameObject);
                }
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (!playerList.Contains(other.gameObject))
            {
                playerList.Add(other.gameObject);
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            foreach (GameObject go in playerList)
            {
                if (other.gameObject == go)
                {
                    toRemove = go;
                    break;
                }
            }

            if (toRemove != null)
            {
                playerList.Remove(toRemove);
            }

            //closestEnemy = null;
        }
    }

    public override void TakeDamage(float damage)
    {
        FloatingTextController.CreateFloatingText(damage.ToString(), transform);
        currentHealth -= damage;
    }

    public override void disabled(float duration)
    {
        nextActionTime += duration;
    }

    public override void SetHealthVisual(float healthNormalized)
    {
        healthBar.transform.localScale = new Vector3(healthNormalized,
                                                     healthBar.transform.localScale.y,
                                                     healthBar.transform.localScale.z);
    }
}
