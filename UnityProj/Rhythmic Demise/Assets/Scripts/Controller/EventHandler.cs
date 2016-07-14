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
    
	// Use this for initialization
	void Start ()
    {
        platform = Application.platform;
        labelText = labelText.GetComponent<Text>();
        labelAnim = labelAnim.GetComponent<Animator>();
        for (int i = 0; i < partsAnim.Length; i++)
        {
            partsAnim[i].GetComponent<Animator>();
            highlights[i].GetComponent<GameObject>();
        }

        for (int i = 0; i < highlights.Length; i++)
            highlights[i].SetActive(false);
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
        else if(platform == RuntimePlatform.WindowsEditor)
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
                    if (partsAnim[0].GetBool("isPending"))
                    {
                        partsAnim[0].SetBool("isPending", false);
                        Application.LoadLevel("MouthStage");
                    }
                    else
                        SetNewAnimation(0);

                    break;
                case "Larnyx":
                    if (partsAnim[1].GetBool("isPending"))
                    {
                        partsAnim[1].SetBool("isPending", false);
                        Application.LoadLevel("LarnyxStage");
                    }
                    else
                        SetNewAnimation(1);

                        break;
                case "Trachea":
                    if (partsAnim[2].GetBool("isPending"))
                    {
                        partsAnim[2].SetBool("isPending", false);
                        Application.LoadLevel("LungStage");
                    }
                    else
                        SetNewAnimation(2);
                    break;
                case "Lungs":
                    if (partsAnim[3].GetBool("isPending"))
                    {
                        partsAnim[3].SetBool("isPending", false);
                        Application.LoadLevel("HeartStage");
                    }
                    else
                        SetNewAnimation(3);
                    break;

                case "Heart":
                    if (partsAnim[4].GetBool("isPending"))
                    {
                        partsAnim[4].SetBool("isPending", false);
                        Application.LoadLevel("LiverStage");
                    }
                    else
                        SetNewAnimation(4);
                    break;
                case "Liver":
                    if (partsAnim[5].GetBool("isPending"))
                    {
                        partsAnim[5].SetBool("isPending", false);
                        Application.LoadLevel("SpleenStage");
                    }
                    else
                        SetNewAnimation(5);
                    break;
                case "Spleen":
                    if (partsAnim[6].GetBool("isPending"))
                    {
                        partsAnim[6].SetBool("isPending", false);
                        Application.LoadLevel("PancreasStage");
                    }
                    else
                        SetNewAnimation(6);
                    break;
                case "Pancreas":
                    if (partsAnim[7].GetBool("isPending"))
                    {
                        partsAnim[7].SetBool("isPending", false);
                        Application.LoadLevel("KidneyStage");
                    }
                    else
                        SetNewAnimation(7);
                    break;
                case "Kidney":
                    if (partsAnim[8].GetBool("isPending"))
                    {
                        partsAnim[8].SetBool("isPending", false);
                        Application.LoadLevel("LargeIntesStage");
                    }
                    else
                        SetNewAnimation(8);
                    break;
                case "Lintes":
                    if (partsAnim[9].GetBool("isPending"))
                    {
                        partsAnim[9].SetBool("isPending", false);
                        Application.LoadLevel("SmallIntesStage");
                    }
                    else
                        SetNewAnimation(9);
                    break;
                case "Sintes":
                    if (partsAnim[10].GetBool("isPending"))
                    {
                        partsAnim[10].SetBool("isPending", false);
                        Application.LoadLevel("BrainStage");
                    }
                    else
                        SetNewAnimation(10);
                    break;
                case "Brain":
                    if (partsAnim[10].GetBool("isPending"))
                    {
                        partsAnim[10].SetBool("isPending", false);
                        Application.LoadLevel("BrainStage");
                    }
                    else
                        SetNewAnimation(11);
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

    public void SetNewAnimation(int index)
    {
        StopAllAnim();
        partsAnim[index].SetBool("isPending", true);
        highlights[index].SetActive(true);
        UpdateText(index);
    }

    public void UpdateText(int index)
    {

        switch (index)
        {
            case 0:
                if(labelText.text != "Mouth" && labelText.text != "Parts")
                {
                    labelAnim.ResetTrigger("newSelection");
                    labelText.text = "Mouth";
                    labelAnim.Play("NewLabelAnimation", -1, 0.0f);
                    
                }
                else if(labelText.text != "Mouth" && labelText.text == "Parts")
                {
                    labelText.text = "Mouth";
                    labelAnim.SetTrigger("newSelection");
                }

                SetFontSize("Mouth");
                break;
            case 1:
                if (labelText.text != "Larnyx" && labelText.text != "Parts")
                {
                    labelAnim.ResetTrigger("newSelection");
                    labelText.text = "Larnyx";
                    labelAnim.Play("NewLabelAnimation", -1, 0.0f);

                }
                else if (labelText.text != "Larnyx" && labelText.text == "Parts")
                {
                    labelText.text = "Larnyx";
                    labelAnim.SetTrigger("newSelection");
                }
                SetFontSize("Larnyx");
                break;
            case 2:
                if (labelText.text != "Trachea" && labelText.text != "Parts")
                {
                    labelAnim.ResetTrigger("newSelection");
                    labelText.text = "Trachea";
                    labelAnim.Play("NewLabelAnimation", -1, 0.0f);

                }
                else if (labelText.text != "Trachea" && labelText.text == "Parts")
                {
                    labelText.text = "Trachea";
                    labelAnim.SetTrigger("newSelection");
                }
                SetFontSize("Trachea");
                break;
            case 3:
                if (labelText.text != "Lungs" && labelText.text != "Parts")
                {
                    labelAnim.ResetTrigger("newSelection");
                    labelText.text = "Lungs";
                    labelAnim.Play("NewLabelAnimation", -1, 0.0f);

                }
                else if (labelText.text != "Lungs" && labelText.text == "Parts")
                {
                    labelText.text = "Lungs";
                    labelAnim.SetTrigger("newSelection");
                }
                SetFontSize("Lungs");
                break;
            case 4:
                if (labelText.text != "Heart" && labelText.text != "Parts")
                {
                    labelAnim.ResetTrigger("newSelection");
                    labelText.text = "Heart";
                    labelAnim.Play("NewLabelAnimation", -1, 0.0f);

                }
                else if (labelText.text != "Heart" && labelText.text == "Parts")
                {
                    labelText.text = "Heart";
                    labelAnim.SetTrigger("newSelection");
                }
                SetFontSize("Heart");
                break;
            case 5:
                if (labelText.text != "Liver" && labelText.text != "Parts")
                {
                    labelAnim.ResetTrigger("newSelection");
                    labelText.text = "Liver";
                    labelAnim.Play("NewLabelAnimation", -1, 0.0f);

                }
                else if (labelText.text != "Liver" && labelText.text == "Parts")
                {
                    labelText.text = "Liver";
                    labelAnim.SetTrigger("newSelection");
                }
                break;
                SetFontSize("Liver");
            case 6:
                if (labelText.text != "Spleen" && labelText.text != "Parts")
                {
                    labelAnim.ResetTrigger("newSelection");
                    labelText.text = "Spleen";
                    labelAnim.Play("NewLabelAnimation", -1, 0.0f);

                }
                else if (labelText.text != "Spleen" && labelText.text == "Parts")
                {
                    labelText.text = "Spleen";
                    labelAnim.SetTrigger("newSelection");
                }
                SetFontSize("Spleen");
                break;
            case 7:
                if (labelText.text != "Pancreas" && labelText.text != "Parts")
                {
                    labelAnim.ResetTrigger("newSelection");
                    labelText.text = "Pancreas";
                    labelAnim.Play("NewLabelAnimation", -1, 0.0f);

                }
                else if (labelText.text != "Pancreas" && labelText.text == "Parts")
                {
                    labelText.text = "Pancreas";
                    labelAnim.SetTrigger("newSelection");
                }
                SetFontSize("Pancreas");
                break;
            case 8:
                if (labelText.text != "Kidney" && labelText.text != "Parts")
                {
                    labelAnim.ResetTrigger("newSelection");
                    labelText.text = "Kidney";
                    labelAnim.Play("NewLabelAnimation", -1, 0.0f);

                }
                else if (labelText.text != "Kidney" && labelText.text == "Parts")
                {
                    labelText.text = "Kidney";
                    labelAnim.SetTrigger("newSelection");
                }
                SetFontSize("Kidney");
                break;
            case 9:
                if (labelText.text != "Large Intestine" && labelText.text != "Parts")
                {
                    labelAnim.ResetTrigger("newSelection");
                    labelText.text = "Large Intestine";
                    labelAnim.Play("NewLabelAnimation", -1, 0.0f);

                }
                else if (labelText.text != "Large Intestine" && labelText.text == "Parts")
                {
                    labelText.text = "Large Intestine";
                    labelAnim.SetTrigger("newSelection");
                }
                SetFontSize("Large Intestine");
                break;
            case 10:
                if (labelText.text != "Small Intestine" && labelText.text != "Parts")
                {
                    labelAnim.ResetTrigger("newSelection");
                    labelText.text = "Small Intestine";
                    labelAnim.Play("NewLabelAnimation", -1, 0.0f);

                }
                else if (labelText.text != "Small Intestine" && labelText.text == "Parts")
                {
                    labelText.text = "Small Intestine";
                    labelAnim.SetTrigger("newSelection");
                }
                SetFontSize("Small Intestine");
                break;
            case 11:
                if (labelText.text != "Brain" && labelText.text != "Parts")
                {
                    labelAnim.ResetTrigger("newSelection");
                    labelText.text = "Brain";
                    labelAnim.Play("NewLabelAnimation", -1, 0.0f);

                }
                else if (labelText.text != "Brain" && labelText.text == "Parts")
                {
                    labelText.text = "Brain";
                    labelAnim.SetTrigger("newSelection");
                }
                SetFontSize("Brain");
                break;
        }
    }
}
