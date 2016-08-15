using UnityEngine;
using System.Collections;

public class EventLiver : MonoBehaviour {

    public GameObject spellPrefab;
    public GameObject spellCircle;
    public GameObject[] spellLocation;

    private float nextActionTime;
    private float cooldown;
    private float launchAttackTime;
    private GameObject targetPos;
    private bool attacked;

    // Use this for initialization
	void Start () {
        nextActionTime = cooldown = 5f;
        launchAttackTime = 2f;
	}
	
	// Update is called once per frame
	void Update () {
        Action();
	}

    void Action()
    {
        if (Time.time >= nextActionTime)
        {
            nextActionTime = Time.time + cooldown;

            int index = Random.Range(0, spellLocation.Length);

            targetPos = spellLocation[index];

            GameObject sc = Instantiate(spellCircle, targetPos.transform.position, spellCircle.transform.rotation) as GameObject;
            launchAttackTime = Time.time + 2;
            Destroy(sc, 2);
            attacked = false;
        }

        if (Time.time >= launchAttackTime && !attacked)
        {
            attacked = true;

            Instantiate(spellPrefab, targetPos.transform.position, targetPos.transform.rotation);
        }
    }
}
