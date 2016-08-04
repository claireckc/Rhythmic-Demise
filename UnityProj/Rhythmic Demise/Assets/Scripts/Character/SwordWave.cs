using UnityEngine;
using System.Collections;

public class SwordWave : MonoBehaviour {

    private float damage;
    private GameObject enemy;

    // Use this for initialization
    void Start()
    {
        Destroy(gameObject, 1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    void initDamage(float damage)
    {
        this.damage = damage;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy e = other.gameObject.GetComponent<Enemy>();
            e.TakeDamage(damage);
        }
    }
}
