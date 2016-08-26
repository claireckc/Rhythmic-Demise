using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StageClearManager : MonoBehaviour {

    Text scoreText;
    Text coinText;
    int stars;

    Image star1;
    Image star2;
    Image star3;

    EndStage es;

    public Sprite fullStar;

	// Use this for initialization
	void Start () {
        GameObject go = GameObject.Find("CoinAmountText");
        scoreText = go.GetComponent<Text>();


        GameObject end = GameObject.Find("End Stage(Clone)");
        es = end.GetComponent<EndStage>();

        star1 = GameObject.Find("1stStar").GetComponent<Image>();
        star2 = GameObject.Find("2ndStar").GetComponent<Image>();
        star3 = GameObject.Find("3rdStar").GetComponent<Image>();
        
        coinText = GameObject.Find("CoinText").GetComponent<Text>();

        if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
        {
            coinText.text = "Carbon";
        }
        else if (PlayerScript.playerdata.pathogenType == Enums.CharacterType.Cancer)
        {
            coinText.text = "Sugar";
        }
	}
	
	// Update is called once per frame
	void Update () {
        scoreText.text = "X" + ScoreManager.score;

        if (es.checker)
        {
            stars = es.starsAttained;
        }

        switch (stars)
        {
            case 0:

                break;
            case 1:
                star1.sprite = fullStar;
                break;
            case 2:
                star1.sprite = fullStar;
                star2.sprite = fullStar;
                break;
            case 3:
                star1.sprite = fullStar;
                star2.sprite = fullStar;
                star3.sprite = fullStar;
                break;
        }
	}
}
