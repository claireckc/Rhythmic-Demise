using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour {

    private float speed;
    public GameObject enemy;

    // Use this for initialization
    void Start()
    {
        speed = 2;
    }

    void Initialize(GameObject target)
    {
        enemy = target;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 dir = enemy.transform.position - this.transform.position;
        float angle = Mathf.Atan2(-dir.y, -dir.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = Vector2.Lerp(transform.position, enemy.transform.position, speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Tower")
        {
            Destroy(gameObject);
        }
    }
}
