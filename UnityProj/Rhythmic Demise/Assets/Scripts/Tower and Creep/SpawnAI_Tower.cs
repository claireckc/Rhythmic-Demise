using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpawnAI_Tower : MonoBehaviour {

	public List <GameObject> spawnList;
    public List <GameObject> enemyList;
    public GameObject closestEnemy;
	public float spawnTime, nextSpawnTime;
	public RectTransform size;

	// Use this for initialization
	void Start () {
        
		size = (RectTransform)this.transform;
		spawnTime = nextSpawnTime = 20.0f;
		spawnList = new List<GameObject> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){

	}

	void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.tag.Contains("Enemy")){
            enemyList.Add(other.transform.parent.gameObject);
        }
	}

    void OnTriggerExit2D(Collider2D other)
    {
        GameObject removeEn = null;
        if (other.transform.parent.tag.Contains("Enemy") || other.transform.parent != null){
            foreach(GameObject en in enemyList)
            {
                if(other.transform.parent.gameObject == en)
                {
                    removeEn = en;
                }
            }
        }

        if (removeEn != null)
            enemyList.Remove(removeEn);
        closestEnemy = null;
    }

	void spawnCreep(){
		nextSpawnTime = Time.time + spawnTime;

	}
}
