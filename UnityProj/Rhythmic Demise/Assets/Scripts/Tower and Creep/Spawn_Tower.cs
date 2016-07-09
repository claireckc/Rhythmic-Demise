using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawn_Tower : Tower {

    //tower inst
    public GameObject creep;

    // Use this for initialization
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        if (!IsDead)
        {
            base.Update();

            if (closestPlayer != null)
            {
                //start attacking it
                if (Time.time >= nextActionTime)
                {
                    Action();
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    protected override void Action()
    {
        nextActionTime = Time.time + cooldown;

        Vector3 dir = closestPlayer.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;

        GameObject spawn = Instantiate(creep, this.transform.position, Quaternion.Euler(0, 0, angle)) as GameObject;
        spawn.SendMessage("Initialize", closestPlayer);

        //Decreasing the health
        TakeDamage(1);
    }
}
