using UnityEngine;
using System.Collections;

public class EventTrachea : MonoBehaviour {

    public GameObject windArt;
    public GameObject[] spawnPoint;
    MovingPoint targetPos;

    private float nextActionTime;
    private float cooldown;

	void Start () {
        nextActionTime = cooldown = 9f;
	}
	
	void Update () {
        if (Time.time >= nextActionTime)
        {
            nextActionTime = Time.time + cooldown;

            for (int i = 0; i < spawnPoint.Length; i++)
            {
                Instantiate(windArt, spawnPoint[i].transform.position, spawnPoint[i].transform.rotation);
            }
        }
	}
}
