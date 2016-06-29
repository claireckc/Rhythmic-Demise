using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class ResourceManagement : MonoBehaviour {

    public Sprite cancerKnightSprite, cancerArcherSprite, cancerPriestSprite;
    public Sprite diabeticKnightSprite, diabeticArcherSprite, diabeticPriestSprite;
    public Image slot1, slot2, slot3;
    private Text noneText1, noneText2, noneText3;
    public Canvas chooseCanvas;

	// Use this for initialization
	void Start () {
        
        slot1 = slot1.GetComponent<Image>();
        slot2 = slot2.GetComponent<Image>();
        slot3 = slot3.GetComponent<Image>();
        
        noneText1 = slot1.GetComponentInChildren<Text>();
        noneText2 = slot2.GetComponentInChildren<Text>();
        noneText3 = slot3.GetComponentInChildren<Text>();
        
        chooseCanvas = chooseCanvas.GetComponent<Canvas>();
        chooseCanvas.enabled = false;

        slot1.enabled = true;
        slot2.enabled = true;
        slot3.enabled = true;
        
    }
	
    public void Init()
    {
        //check with playerdata and initialize accordingly
    }

    public void Slot1_Click()
    {

    }

    public void Slot2_Click()
    {

    }

    public void Slot3_Click()
    {

    }
}
