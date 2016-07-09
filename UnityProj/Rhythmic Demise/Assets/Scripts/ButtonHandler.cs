using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

    RuntimePlatform platform;

    public AudioSource buttonClickSound;

    public GameController gc;

	// Use this for initialization
	void Start () {
        platform = Application.platform;
	}
	
	// Update is called once per frame
	void Update () {
        if (platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if (Input.touchCount > 0)
            {
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    DetermineTouchPosition(Input.GetTouch(0).position);
                }
            }
        }
        else if (platform == RuntimePlatform.WindowsEditor)
        {
            if (Input.GetMouseButtonDown(0))
                DetermineTouchPosition(Input.mousePosition);
        }
	}

    public void DetermineTouchPosition(Vector2 touchPosition)
    {
        Ray ray = Camera.main.ScreenPointToRay(touchPosition);
        RaycastHit2D rayHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << 17);
        
        if (rayHit.collider != null)
        {
            switch (rayHit.collider.name)
            {
                case "Note1(Clone)":
                    buttonClickSound.PlayOneShot(buttonClickSound.clip);
                    gc.addHit("1");
                    Destroy(rayHit.collider.gameObject);
                    break;
                case "Note2(Clone)":
                    buttonClickSound.PlayOneShot(buttonClickSound.clip);
                    gc.addHit("2");
                    Destroy(rayHit.collider.gameObject);
                    break;
                case "Note3(Clone)":
                    buttonClickSound.PlayOneShot(buttonClickSound.clip);
                    gc.addHit("3");
                    Destroy(rayHit.collider.gameObject);
                    break;
                case "Note4(Clone)":
                    buttonClickSound.PlayOneShot(buttonClickSound.clip);
                    gc.addHit("4");
                    Destroy(rayHit.collider.gameObject);
                    break;
            }
        }
    }
}
