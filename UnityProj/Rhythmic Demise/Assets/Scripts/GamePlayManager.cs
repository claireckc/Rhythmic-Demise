using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour {

    Image archerImage;
    Image knightImage;
    Image priestImage;

    Text scoreText;

    public Sprite cancerArcherSprite;
    public Sprite cancerKnightSprite;
    public Sprite cancerPriestsprite;

    public Sprite diabeticArcherSprite;
    public Sprite diabeticKnightSprite;
    public Sprite diabeticPriestsprite;

	void Start () {
        archerImage = GameObject.Find("ArcherCountImage").GetComponent<Image>();
        knightImage = GameObject.Find("KnightCountImage").GetComponent<Image>();
        priestImage = GameObject.Find("PriestCountImage").GetComponent<Image>();

        scoreText = GameObject.Find("ScoreText").GetComponent<Text>();

        if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
        {
            archerImage.sprite = cancerArcherSprite;
            knightImage.sprite = cancerKnightSprite;
            priestImage.sprite = cancerPriestsprite;

            scoreText.text = "Carbon";
        }
        else if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
        {
            archerImage.sprite = diabeticArcherSprite;
            knightImage.sprite = diabeticKnightSprite;
            priestImage.sprite = diabeticPriestsprite;

            scoreText.text = "Sugar";
        }
	}
	
	void Update () {
	
	}
}
