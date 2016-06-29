using UnityEngine;
using System.Collections;

public class BaseTower : MonoBehaviour {

    protected float lastTick;
    protected float refreshRate = 0.10f;

    protected float lastAction;
    protected float cooldown = 1.0f;

    protected float range = 5.0f;
	
	// Update is called once per frame
	private void Update () 
    {
        //If the tower is ready to shoot
        if (Time.time - lastAction > cooldown)
        {
            //Refresh every 0.10f to find a target
            if (Time.time - lastTick > refreshRate)
            {
                lastTick = Time.time;
                //Get a target
                Transform target = GetNearestEnemy();
                if (target != null)
                {
                    Action(target);
                }
            }
        }
	}

    private Transform GetNearestEnemy()
    {
        //Test if there are enemies within our range
        Collider[] allEnemies = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Player"));

        if (allEnemies.Length != 0)
        {
            int closestIndex = 0;
            float nearestDistance = Vector3.SqrMagnitude(transform.position - allEnemies[0].transform.position);

            for (int i = 1; i < allEnemies.Length; i++)
            {
                float distance = Vector3.SqrMagnitude(transform.position - allEnemies[i].transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    closestIndex = i;
                }
            }

            return allEnemies[closestIndex].transform;
        }

        return null;
    }

    private void Action(Transform target)
    {
        lastAction = Time.time;

    }
}
