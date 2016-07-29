using UnityEngine;
using System.Collections;

public class PathColliderController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Character c = other.GetComponent<Character>();
            c.setInPath(false);
        }
    }
}
