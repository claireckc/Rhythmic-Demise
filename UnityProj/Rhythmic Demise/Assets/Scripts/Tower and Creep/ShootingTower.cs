using UnityEngine;
using System.Collections;

public class ShootingTower : Tower {

	// Use this for initialization
	new void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	new void Update () {
        if (!IsDead)
        {
            base.Update();

            if (closestEnemy != null)
            {
                //start attacking it
                if (Time.time >= nextFireTime)
                {
                    AttackEnemy();
                }
            }
        }
	}
}
