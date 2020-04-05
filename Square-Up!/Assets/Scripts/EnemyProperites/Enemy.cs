using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 100f;
    // public GameObject deathEffect;

    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0)
            Die();
    }

    void Die () {
        // Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
