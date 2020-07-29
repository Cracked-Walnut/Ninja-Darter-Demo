using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : Arrow {

    // Start is called before the first frame update
    void Start() {
        speed = 40f;
        damage = 1f;
        range = 2f;
        rigidbody2D.velocity = new Vector2(-1, 0) * speed;
    }

    // Update is called once per frame
    void Update() {
        range -= Time.deltaTime;
        if (range <= 0.0f)
            Destroy(gameObject);
    }

    void Awake() {
        rigidbody2D = GetComponent<Rigidbody2D>();
    }
}
