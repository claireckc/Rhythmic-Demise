using UnityEngine;
using System.Collections;

public class WindArt : MonoBehaviour {

    float movementSpeed;

	void Start () {
        movementSpeed = 4;
	}
	
	void Update () {
        transform.Translate(Vector2.left * movementSpeed * Time.deltaTime);
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            this.GetComponent<BoxCollider2D>().enabled = false;

            if (ArmyController.armyController.currentAction == Enums.PlayerState.Defend)
            {

            }
            else
            {
                ArmyController.armyController.setCurrentState(Enums.PlayerState.MoveLeft);
                GameController.gameController.setActionTurn(true);
            }
        }
    }

    void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
