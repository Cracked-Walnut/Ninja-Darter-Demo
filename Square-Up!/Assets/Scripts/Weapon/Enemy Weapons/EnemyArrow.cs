using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour {

    private float speed;
    private float damage;
    private float range;
    private Rigidbody2D rigidbody2D;
    private Player player;

    // Start is called before the first frame update
    void Start() {
        speed = 20f;
        damage = 1f;
        range = 2f;
        rigidbody2D.velocity = (transform.right * speed) * -1;
    }

    // Update is called once per frame
    void Update() {
        range -= Time.deltaTime;
        if (range <= 0.0f)
            Destroy(gameObject);
    }

    void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<Player>();
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
