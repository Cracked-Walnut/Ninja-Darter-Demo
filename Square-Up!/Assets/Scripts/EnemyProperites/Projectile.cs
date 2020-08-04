using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {

    [SerializeField] private float health;

    public void takeDamage(float damage) {
        health -= damage;
        if (health <= 0)
            Destroy(gameObject);
    }
}
