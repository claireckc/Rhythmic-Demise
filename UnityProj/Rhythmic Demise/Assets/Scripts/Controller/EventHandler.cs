using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;

public class EventHandler : MonoBehaviour {

    public RuntimePlatform platform;
    public Ray ray;
    public RaycastHit2D rayHit;

    public Animator labelAnim;
    public Animator[] partsAnim;
    public GameObject[] highlights;

    public Text labelText;
    private int dFontSize, iFontSize;

    private Color whiteColor, lockedColor;

    public SpriteRenderer[] partsSprite;
    public GameObject stars;
    
	// Use this for initialization
	void Start ()
    {
        platform = Application.platform;
        labelText = labelText.GetComponent<Text>();
        labelAnim = labelAnim.GetComponent<Animator>();
        whiteColor = new Color(255f/255f, 255f/255f, 255f/255f);
        lockedColor = new Color(146f/255f, 146f/255f, 255f/255f);

        for (int i = 0; i < partsAnim.Length; i++)
        {
            partsAnim[i].GetComponent<Animator>();
            highlights[i].GetComponent<GameObject>();
        }
        
        for(int i = 0; i < highlights.Length; i++)
        {
            highlights[i].GetComponent<GameObject>();
            highlights[i].SetActive(false);
        }

        for(int i = 0; i < partsSprite.Length; i++)
        {
            partsSprite[i].GetComponent<SpriteRenderer>();

            if (PlayerScript.playerdata.mapProgress[i].isLocked)
                partsSprite[i].color = lockedColor;
            else
                partsSprite[i].color = whiteColor;
        }

        dFontSize = 40;
        iFontSize = 32;
        
    }
	
	// Update is called once per frame
	void Update () {
	
        if(platform == RuntimePlatform.Android || platform == RuntimePlatform.IPhonePlayer)
        {
            if(Input.touchCount > 0)
            {
                if(Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    DetermineTouchPosition(Input.GetTouch(0).position);
                }
            }
        }
		else if(platform == RuntimePlatform.WindowsEditor || platform == RuntimePlatform.OSXEditor)
        {
            if (Input.GetMouseButtonDown(0))
                DetermineTouchPosition(Input.mousePosition);
        }
	}

    public void DetermineTouchPosition(Vector2 touchPosition)
    {
        ray = Camera.main.ScreenPointToRay(touchPosition);
        rayHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << 13);
       if(rayHit.collider != null)
        {
            switch (rayHit.collider.gameObject.tag)
            {
                case "Mouth":
                    if (!PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Mouth].isLocked)
                    {
                        if (partsAnim[(int)Enums.MainMap.Mouth].GetBool("isPending"))
                        {
                            partsAnim[(int)Enums.MainMap.Mouth].SetBool("isPending", false);
                            Application.LoadLevel("MouthStage");
                        }
                    }

                    SetNewAnimation((int)Enums.MainMap.Mouth, PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Mouth].isLocked);


                    break;
                case "Larnyx":
                    if (!PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Larnyx].isLocked)
                    {
                        if (partsAnim[(int)Enums.MainMap.Larnyx].GetBool("isPending"))
                        {
                            partsAnim[(int)Enums.MainMap.Larnyx].SetBool("isPending", false);
                            Application.LoadLevel("LarnyxStage");
                        }
                    }
                    SetNewAnimation((int)Enums.MainMap.Larnyx, PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Larnyx].isLocked);


                    break;
                case "Trachea":
                    if (!PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Trachea].isLocked)
                    {
                        if (partsAnim[(int)Enums.MainMap.Trachea].GetBool("isPending"))
                        {
                            partsAnim[(int)Enums.MainMap.Trachea].SetBool("isPending", false);
                            Application.LoadLevel("LungStage");
                        }
                    }
                    SetNewAnimation((int)Enums.MainMap.Trachea, PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Trachea].isLocked);

                    break;
                case "Lungs":
                    if (!PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Lung].isLocked)
                    {
                        if (partsAnim[(int)Enums.MainMap.Lung].GetBool("isPending"))
                        {
                            partsAnim[(int)Enums.MainMap.Lung].SetBool("isPending", false);
                            Application.LoadLevel("HeartStage");
                        }
                    }
                    SetNewAnimation((int)Enums.MainMap.Lung, PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Lung].isLocked);

                    break;

                case "Heart":
                    if (!PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Heart].isLocked)
                    {
                        if (partsAnim[(int)Enums.MainMap.Heart].GetBool("isPending"))
                        {
                            partsAnim[(int)Enums.MainMap.Heart].SetBool("isPending", false);
                            Application.LoadLevel("LiverStage");
                        }
                    }
                    SetNewAnimation((int)Enums.MainMap.Heart, PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Heart].isLocked);

                    break;
                case "Liver":
                    if (!PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Liver].isLocked)
                    {
                        if (partsAnim[(int)Enums.MainMap.Liver].GetBool("isPending"))
                        {
                            partsAnim[(int)Enums.MainMap.Liver].SetBool("isPending", false);
                            Application.LoadLevel("SpleenStage");
                        }
                    }
                    SetNewAnimation((int)Enums.MainMap.Liver, PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Liver].isLocked);

                    break;
                case "Spleen":
                    if (!PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Spleen].isLocked)
                    {
                        if (partsAnim[(int)Enums.MainMap.Spleen].GetBool("isPending"))
                        {
                            partsAnim[(int)Enums.MainMap.Spleen].SetBool("isPending", false);
                            Application.LoadLevel("PancreasStage");
                        }
                    }
                    SetNewAnimation((int)Enums.MainMap.Spleen, PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Spleen].isLocked);

                    break;
                case "Pancreas":
                    if (!PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Pancreas].isLocked)
                    {
                        if (partsAnim[(int)Enums.MainMap.Pancreas].GetBool("isPending"))
                        {
                            partsAnim[(int)Enums.MainMap.Pancreas].SetBool("isPending", false);
                            Application.LoadLevel("KidneyStage");
                        }
                    }
                    SetNewAnimation((int)Enums.MainMap.Pancreas, PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Pancreas].isLocked);

                    break;
                case "Kidney":
                    if (!PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Kidney].isLocked)
                    {
                        if (partsAnim[(int)Enums.MainMap.Kidney].GetBool("isPending"))
                        {
                            partsAnim[(int)Enums.MainMap.Kidney].SetBool("isPending", false);
                            Application.LoadLevel("LargeIntesStage");
                        }
                    }
                    SetNewAnimation((int)Enums.MainMap.Kidney, PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Kidney].isLocked);

                    break;
                case "Lintes":
                    if (!PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.LIntes].isLocked)
                    {
                        if (partsAnim[(int)Enums.MainMap.LIntes].GetBool("isPending"))
                        {
                            partsAnim[(int)Enums.MainMap.LIntes].SetBool("isPending", false);
                            Application.LoadLevel("SmallIntesStage");
                        }
                    }
                    SetNewAnimation((int)Enums.MainMap.LIntes, PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.LIntes].isLocked);

                    break;
                case "Sintes":
                    if (!PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.SIntes].isLocked)
                    {
                        if (partsAnim[(int)Enums.MainMap.SIntes].GetBool("isPending"))
                        {
                            partsAnim[(int)Enums.MainMap.SIntes].SetBool("isPending", false);
                            Application.LoadLevel("BrainStage");
                        }
                    }
                    SetNewAnimation((int)Enums.MainMap.SIntes, PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.SIntes].isLocked);

                    break;
                case "Brain":
                    if (!PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Brain].isLocked)
                    {
                        if (partsAnim[(int)Enums.MainMap.Brain].GetBool("isPending"))
                        {
                            partsAnim[(int)Enums.MainMap.Brain].SetBool("isPending", false);
                            Application.LoadLevel("BrainStage");
                        }
                    }
                    SetNewAnimation((int)Enums.MainMap.Brain, PlayerScript.playerdata.mapProgress[(int)Enums.MainMap.Brain].isLocked);

                    break;
            }

        }
    }

    public void Map_ReturnClick()
    {
        Application.LoadLevel("StartScreen");
    }

    public void StopAllAnim()
    {
        for(int i = 0; i < partsAnim.Length; i++)
        {
            partsAnim[i].SetBool("isPending", false);
            highlights[i].SetActive(false);

        }
    }
    
    public void SetFontSize(string part)
    {
        if (part.Contains("Intestine"))
            labelText.fontSize = iFontSize;
        else
            labelText.fontSize = dFontSize;
    }

    public void SetNewAnimation(int index, bool status)
    {
        if (!status)
        {
            StopAllAnim();
            partsAnim[index].SetBool("isPending", true);
            highlights[index].SetActive(true);
            UpdateText(index, "");
        }
        else
            UpdateText(index, "Locked: ");
    }

    public void DisplayStars(Enums.MainMap mapName)
    {
        GameObject starObj = Instantiate(stars, stars.transform.position, stars.transform.rotation) as GameObject;
        starObj.SendMessage("Init", mapName);
    }

    public void UpdateText(int index, string addon)
    {
        switch (index)
        {
            case 0:
                if (labelText.text != "Mouth")
                {
                    if (labelText.text == "Parts")
                        labelAnim.SetTrigger("newSelection");
                    else
                    {
                        labelAnim.ResetTrigger("newSelection");
                        labelAnim.Play("NewLabelAnimation", -1, 0.0f);
                    }
                    if (addon == "")
                        labelText.text = "Mouth";
                    else
                        labelText.text = "";
                }
                DisplayStars(Enums.MainMap.Mouth);
                SetFontSize("Mouth");
                break;
            case 1:
                if (labelText.text != "Larnyx")
                {
                    if (labelText.text == "Parts")
                        labelAnim.SetTrigger("newSelection");
                    else
                    {
                        labelAnim.ResetTrigger("newSelection");
                        labelAnim.Play("NewLabelAnimation", -1, 0.0f);
                    }
                    if (addon == "")
                        labelText.text = "Larnyx";
                    else
                        labelText.text = "Locked";
                }
                DisplayStars(Enums.MainMap.Larnyx);
                SetFontSize("Larnyx");
                break;
            case 2:
                if (labelText.text != "Trachea")
                {
                    if (labelText.text == "Parts")
                        labelAnim.SetTrigger("newSelection");
                    else
                    {
                        labelAnim.ResetTrigger("newSelection");
                        labelAnim.Play("NewLabelAnimation", -1, 0.0f);
                    }
                    if (addon == "")
                        labelText.text = "Trachea";
                    else
                        labelText.text = "Locked";
                }
                DisplayStars(Enums.MainMap.Trachea);
                SetFontSize("Trachea");
                break;
            case 3:
                if (labelText.text != "Lungs")
                {
                    if (labelText.text == "Parts")
                        labelAnim.SetTrigger("newSelection");
                    else
                    {
                        labelAnim.ResetTrigger("newSelection");
                        labelAnim.Play("NewLabelAnimation", -1, 0.0f);
                    }
                    if (addon == "")
                        labelText.text = "Lungs";
                    else
                        labelText.text = "Locked";
                }
                DisplayStars(Enums.MainMap.Lung);
                SetFontSize("Lungs");
                break;
            case 4:
                if (labelText.text != "Heart")
                {
                    if (labelText.text == "Parts")
                        labelAnim.SetTrigger("newSelection");
                    else
                    {
                        labelAnim.ResetTrigger("newSelection");
                        labelAnim.Play("NewLabelAnimation", -1, 0.0f);
                    }
                    if (addon == "")
                        labelText.text = "Heart";
                    else
                        labelText.text = "Locked";
                }
                DisplayStars(Enums.MainMap.Heart);

                SetFontSize("Heart");
                break;
            case 5:
                if (labelText.text != "Liver")
                {
                    if (labelText.text == "Parts")
                    {
                        labelText.text = "Liver";
                        labelAnim.SetTrigger("newSelection");
                    }
                    else
                    {
                        labelAnim.ResetTrigger("newSelection");
                        labelText.text = "Liver";
                        labelAnim.Play("NewLabelAnimation", -1, 0.0f);
                    }
                    if (addon == "")
                        labelText.text = "Liver";
                    else
                        labelText.text = "Locked";
                }
                DisplayStars(Enums.MainMap.Liver);
                SetFontSize("Liver");
                break;
            case 6:
                if (labelText.text != "Spleen")
                {
                    if (labelText.text == "Parts")
                        labelAnim.SetTrigger("newSelection");
                    else
                    {
                        labelAnim.ResetTrigger("newSelection");
                        labelAnim.Play("NewLabelAnimation", -1, 0.0f);
                    }
                    if (addon == "")
                        labelText.text = "Spleen";
                    else
                        labelText.text = "Locked";
                }
                DisplayStars(Enums.MainMap.Spleen);

                SetFontSize("Spleen");
                break;
            case 7:
                if (labelText.text != "Pancreas")
                {
                    if (labelText.text == "Parts")
                        labelAnim.SetTrigger("newSelection");
                    else
                    {
                        labelAnim.ResetTrigger("newSelection");
                        labelAnim.Play("NewLabelAnimation", -1, 0.0f);
                    }
                    if (addon == "")
                        labelText.text = "Pancreas";
                    else
                        labelText.text = "Locked";
                }
                DisplayStars(Enums.MainMap.Pancreas);
                SetFontSize("Pancreas");
                break;
            case 8:
                if (labelText.text != "Kidney")
                {
                    if (labelText.text == "Parts")
                        labelAnim.SetTrigger("newSelection");
                    else
                    {
                        labelAnim.ResetTrigger("newSelection");
                        labelAnim.Play("NewLabelAnimation", -1, 0.0f);
                    }
                    if (addon == "")
                        labelText.text = "Kidney";
                    else
                        labelText.text = "Locked";
                }
                DisplayStars(Enums.MainMap.Kidney);
                SetFontSize("Kidney");
                break;
            case 9:
                if (labelText.text != "Large Intestine")
                {
                    if (labelText.text == "Parts")
                        labelAnim.SetTrigger("newSelection");
                    else
                    {
                        labelAnim.ResetTrigger("newSelection");
                        labelAnim.Play("NewLabelAnimation", -1, 0.0f);
                    }
                    if (addon == "")
                        labelText.text = "Large Intestine";
                    else
                        labelText.text = "Locked";

                }
                DisplayStars(Enums.MainMap.LIntes);
                SetFontSize("Large Intestine");
                break;
            case 10:
                if (labelText.text != "Small Intestine")
                {
                    if (labelText.text == "Parts")
                        labelAnim.SetTrigger("newSelection");
                    else
                    {
                        labelAnim.ResetTrigger("newSelection");
                        labelAnim.Play("NewLabelAnimation", -1, 0.0f);
                    }
                    if (addon == "")
                        labelText.text = "Small Intestine";
                    else
                        labelText.text = "Locked";
                }
                DisplayStars(Enums.MainMap.SIntes);
                SetFontSize("Small Intestine");
                break;
            case 11:
                if(labelText.text != "Brain")
                {
                    if (labelText.text == "Parts")
                        labelAnim.SetTrigger("newSelection");
                    else
                    {
                        labelAnim.ResetTrigger("newSelection");
                        labelAnim.Play("NewLabelAnimation", -1, 0.0f);
                    }
                    if (addon == "")
                        labelText.text = "Brain";
                    else
                        labelText.text = "Locked";

                }
                DisplayStars(Enums.MainMap.Brain);
                SetFontSize("Brain");
                break;
        }
    }
}
