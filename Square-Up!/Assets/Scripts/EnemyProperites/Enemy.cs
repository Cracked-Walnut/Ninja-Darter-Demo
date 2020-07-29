using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    protected List<string> animationList = new List<string>();
    private float health = 100f;
    protected float attackRate;
    protected bool isAlive;
    protected Animator animator;
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
