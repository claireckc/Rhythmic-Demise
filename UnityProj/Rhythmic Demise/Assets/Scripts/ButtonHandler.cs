using UnityEngine;
using System.Collections;

public class ButtonHandler : MonoBehaviour {

    RuntimePlatform platform;
    public AudioSource buttonClickSound;
    GameObject redButton, blueButton, greenButton, yellowButton;

	// Use this for initialization
	void Start () {
        platform = Application.platform;
        GetButtons();
        ApplyOpacity();
	}

    void GetButtons()
    {
        redButton = GameObject.Find("CanvasUI/GamePlayUI/Button1");
        blueButton = GameObject.Find("CanvasUI/GamePlayUI/Button2");
        greenButton = GameObject.Find("CanvasUI/GamePlayUI/Button3");
        yellowButton = GameObject.Find("CanvasUI/GamePlayUI/Button4");
    }

    void ApplyOpacity()
    {
        float opacityVal = PlayerScript.playerdata.buttonAlpha;

        //for red button
        Color newColor = redButton.GetComponent<UnityEngine.UI.Image>().color;
        newColor.a = opacityVal;
        redButton.GetComponent<UnityEngine.UI.Image>().color = newColor;

        //blue button
        newColor = blueButton.GetComponent<UnityEngine.UI.Image>().color;
        newColor.a = opacityVal;
        blueButton.GetComponent<UnityEngine.UI.Image>().color = newColor;

        //green button
        newColor = greenButton.GetComponent<UnityEngine.UI.Image>().color;
        newColor.a = opacityVal;
        greenButton.GetComponent<UnityEngine.UI.Image>().color = newColor;

        //yellow button
        newColor = yellowButton.GetComponent<UnityEngine.UI.Image>().color;
        newColor.a = opacityVal;
        yellowButton.GetComponent<UnityEngine.UI.Image>().color = newColor;
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
                    GameController.gameController.addHit("1");
                    Destroy(rayHit.collider.gameObject);
                    break;
                case "Note2(Clone)":
                    buttonClickSound.PlayOneShot(buttonClickSound.clip);
                    GameController.gameController.addHit("2");
                    Destroy(rayHit.collider.gameObject);
                    break;
                case "Note3(Clone)":
                    buttonClickSound.PlayOneShot(buttonClickSound.clip);
                    GameController.gameController.addHit("3");
                    Destroy(rayHit.collider.gameObject);
                    break;
                case "Note4(Clone)":
                    buttonClickSound.PlayOneShot(buttonClickSound.clip);
                    GameController.gameController.addHit("4");
                    Destroy(rayHit.collider.gameObject);
                    break;
            }
        }
    }
}
