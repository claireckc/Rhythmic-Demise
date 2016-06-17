using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {



	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void move(int x, int y)
    {
        Vector3 temp = new Vector3(x, y, 0);
        transform.position += temp;
    }
}
