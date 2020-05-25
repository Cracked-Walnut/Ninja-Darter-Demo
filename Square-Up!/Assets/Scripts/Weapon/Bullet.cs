using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    private float damage = 34f, speed = 40f, timer = 2.0f;
    private bool isItemPickUp = false;
    private CharacterController2D characterController2D;
    private Enemy enemy;
    private CoinCollision coinCollision;
    private HeartCollision heartCollision;
    public Rigidbody2D rigidbody2D;
    
    void Awake() {
        characterController2D = FindObjectOfType<CharacterController2D>();
    }

    void Start() {
        rigidbody2D.velocity = transform.right * speed;
    }

    void Update() {
        timer -= Time.deltaTime;
        if (timer <= 0.0f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D (Collider2D collider) {
        enemy = collider.GetComponent<Enemy>();
        coinCollision = collider.GetComponent<CoinCollision>();
        heartCollision = collider.GetComponent<HeartCollision>();
        
        if (enemy != null)
            enemy.takeDamage(damage);
        
        if (coinCollision != null || heartCollision != null)
            return; /*Bullets will not disappear when colliding with coins or hearts*/
        
        Destroy(gameObject);
    }

    public float getDamage() {
        return damage;
    }

    public void setDamage(float damage) {
        this.damage = damage;
    }
}

/*
Sources:
1) B., Brackeys, '2D Shooting in Unity (Tutorial)', 2018. [Online]. Available: https://www.youtube.com/watch?v=wkKsl1Mfp5M [Accessed: 21-Mar-2020].
*/