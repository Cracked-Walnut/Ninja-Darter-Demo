using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

    [SerializeField] private int damage = 1; 
    [SerializeField] private float bulletRange = 2.0f;
    [SerializeField] private float speed = 40f;
    [SerializeField] private Rigidbody2D rigidbody2D;
    private Player player;

    void Start() {
        rigidbody2D.velocity = transform.right * speed;
    }

    void Update() {
        /*Destroy bullet after two seconds of not colliding*/
        bulletRange -= Time.deltaTime; 
        if (bulletRange <= 0.0f)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D (Collider2D collider) {
        player = collider.GetComponent<Player>();

        if (player != null)
            player.takeDamage(damage, 1);
        
        Destroy(gameObject);
    }
}
