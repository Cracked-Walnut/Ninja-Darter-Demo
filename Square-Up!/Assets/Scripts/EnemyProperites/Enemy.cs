using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] private float health = 100f;
    private bool isAlive = true;
    // public GameObject deathEffect;

    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0)
            Die();
    }

    void Die() {
        // Instantiate(deathEffect, transform.position, Quaternion.identity);
        isAlive = false;
        Destroy(gameObject);
    }

    public bool getIsAlive() {
        return isAlive;
    }
}
