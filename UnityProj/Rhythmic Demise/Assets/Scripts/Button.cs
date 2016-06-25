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
        if (other.tag == "Note")
        {
            if (!gc.bs.moveActionTurn)
            {
                if (Input.GetKeyDown(gc.buttonsKeyCode[0]) && buttonID == 1)
                {
                    Destroy(other.gameObject);
                    particleBurst.Play();
                    gc.addHit("1");
                }
                else if (Input.GetKeyDown(gc.buttonsKeyCode[1]) && buttonID == 2)
                {
                    Destroy(other.gameObject);
                    particleBurst.Play();
                    gc.addHit("2");
                }
                else if (Input.GetKeyDown(gc.buttonsKeyCode[2]) && buttonID == 3)
                {
                    Destroy(other.gameObject);
                    particleBurst.Play();
                    gc.addHit("3");
                }
                else if (Input.GetKeyDown(gc.buttonsKeyCode[3]) && buttonID == 4)
                {
                    Destroy(other.gameObject);
                    particleBurst.Play();
                    gc.addHit("4");
                }
            }
        }
    }
}
