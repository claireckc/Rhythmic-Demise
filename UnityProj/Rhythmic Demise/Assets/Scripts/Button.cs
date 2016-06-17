using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public int buttonID;
    public GameController gc;

    private ParticleSystem particleBurst;

	// Use this for initialization
	void Start () {
        particleBurst = GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if (!gc.bs.moveActionTurn)
        {
            if (Input.GetKeyDown(KeyCode.A) && buttonID == 1)
            {
                Destroy(other.gameObject);
                particleBurst.Play();
                gc.addHit("A");
            }
            else if (Input.GetKeyDown(KeyCode.D) && buttonID == 2)
            {
                Destroy(other.gameObject);
                particleBurst.Play();
                gc.addHit("D");
            }
            else if (Input.GetKeyDown(KeyCode.W) && buttonID == 3)
            {
                Destroy(other.gameObject);
                particleBurst.Play();
                gc.addHit("W");
            }
            else if (Input.GetKeyDown(KeyCode.S) && buttonID == 4)
            {
                Destroy(other.gameObject);
                particleBurst.Play();
                gc.addHit("S");
            }
        }
    }
}
