﻿using UnityEngine;
using System.Collections;

public class Orb : MonoBehaviour {

    private float speed;
    private float damage;
    private GameObject enemy;
    private Vector3 direction;
    private Vector3 goal;
    private const float minDistance = 0.2f;

    // Use this for initialization
    void Start()
    {
        speed = 2;
    }

    void Initialize(GameObject target)
    {
        enemy = target;
        direction = enemy.transform.position - this.transform.position;
        goal = enemy.transform.position;
    }

    void initDamage(float damage)
    {
        this.damage = damage;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float angle = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        this.transform.rotation = Quaternion.Euler(0, 0, angle);
        transform.position = Vector2.Lerp(transform.position, goal, speed * Time.deltaTime);

        if ((transform.position - goal).sqrMagnitude <= minDistance * minDistance)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.gameObject.GetComponent<Enemy>();
            enemy.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
