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

    void Awake() {
        player = FindObjectOfType<Player>();
    }

    void Update() {
        /*Destroy bullet after two seconds of not colliding*/
        bulletRange -= Time.deltaTime; 
        if (bulletRange <= 0.0f)
            Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (!player.getPlayerInvincible()) {
            if (collision.gameObject.name == "Player") {
                player.takeDamage(1, 25);
            }
        }
        Destroy(gameObject);
	}

}
