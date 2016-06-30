using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

    public int buttonID;
    public GameController gc;

    public bool active;

	// Use this for initialization
	void Start () {
        InvokeRepeating("Trigger", 0f, 0.5f);
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    void Trigger()
    {
        active = true;

        Invoke("Trigger2", 0.3f);
    }

    void Trigger2()
    {
        active = false;
    }

    public void Hit()
    {
        if (active)
        {
            Debug.Log("A");
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {/*
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
        }*/

        
    }
}
