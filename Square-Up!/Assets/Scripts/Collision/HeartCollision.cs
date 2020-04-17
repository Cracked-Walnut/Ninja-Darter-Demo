using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script will trigger if the player collects a heart*/

public class HeartCollision : MonoBehaviour {

    public int healAmount = 1;
    private bool isPickedUp = false, isPlayer = false;
    private Player player;
    private GameObject playerObject;
    private Bullet bullet;
    private BoxCollider2D boxCollider2D;

    private void Awake() {
        player = FindObjectOfType<Player>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        Debug.Log(boxCollider2D.friction);
        if (collider.gameObject.name == "Bullet")
            return;
        checkPickUpState();
    }

    void checkPickUpState() {
        if (!isPickedUp) {
            if (player.healthIsFull())
                return;
            else {
                player.gainHealth(healAmount);
                player.healthIsFull();
                isPickedUp = true;
                Destroy(gameObject);
            }
        }
    }
}//end of class