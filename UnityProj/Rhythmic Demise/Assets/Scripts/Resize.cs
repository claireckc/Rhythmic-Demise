using UnityEngine;
using System.Collections;

public class Resize : MonoBehaviour {

	// Use this for initialization
	void Start () {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();

        float worldScreenHeight = Camera.main.orthographicSize * 2;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(
        worldScreenWidth / sr.sprite.bounds.size.x,
        worldScreenHeight / sr.sprite.bounds.size.y, 1);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
