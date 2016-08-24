using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    GameObject stageRate;

    bool interact;

    // Use this for initialization
    void Start ()
    {
        platform = Application.platform;
        labelText = labelText.GetComponent<Text>();
        labelAnim = labelAnim.GetComponent<Animator>();
        stageRate = GameObject.Find("Map Rating");
        whiteColor = new Color(255f/255f, 255f/255f, 255f/255f);
        lockedColor = new Color(146f/255f, 146f/255f, 255f/255f);
        interact = true;

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

    void BlockInteraction()
    {
        interact = false;
    }

    void AllowInteraction()
    {
        interact = true;
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
            if (interact)
            {
                if (Input.GetMouseButtonDown(0))
                    DetermineTouchPosition(Input.mousePosition);
            }
        }
	}

    public void SetAccess(string mapTag)
    {
        int index = GetTagIndex(mapTag);
        if (index != -1)
        {
            if (!PlayerScript.playerdata.mapProgress[index].isLocked)
            {
                if (partsAnim[index].GetBool("isPending"))
                {
                    partsAnim[index].SetBool("isPending", false);
                    PlayerScript.playerdata.clickedMap = (Enums.MainMap)index;
                    Application.LoadLevel(Enums.StageName[index]);
                }
            }
            SetNewAnimation(index, PlayerScript.playerdata.mapProgress[index].isLocked);
        }

    }

    public int GetTagIndex(string mapTag)
    {
       for(int i = 0; i < Enums.MapName.Length; i++)
        {
            if (Enums.MapName[i].Equals(mapTag))
                return i;
        }
        return -1;
    }

    public void DetermineTouchPosition(Vector2 touchPosition)
    {
        ray = Camera.main.ScreenPointToRay(touchPosition);
        rayHit = Physics2D.Raycast(ray.origin, ray.direction, Mathf.Infinity, 1 << 13);
       if(rayHit.collider != null)
        {
            SetAccess(rayHit.collider.gameObject.tag);
        }
    }

    public void Map_ReturnClick()
    {
        //Application.LoadLevel("StartScreen");
        Debug.Log("Pressed!");
        SceneManager.LoadScene("StartScreen");
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
        else {
            UpdateText(index, "Locked: ");
            stageRate.SendMessage("Init", (Enums.MainMap)index);
        }
    }

    public void DisplayStars(Enums.MainMap mapName)
    {
        stageRate.SendMessage("Init", mapName);
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
                DisplayStars(Enums.MainMap.LIntes);
                SetFontSize("Large Intestine");
                break;
            case 10:
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
                    {
                        labelText.text = "Small Intestine";
                        SetFontSize("Large Intestine");
                    }
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
