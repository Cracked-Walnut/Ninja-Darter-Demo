using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCollision : MonoBehaviour {

    private Player player;
    private AudioManager audioManager;
    private GameOverMenu gameOverMenu;

    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<Player>();
        gameOverMenu = FindObjectOfType<GameOverMenu>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player")
            player.takeDamage(1, 250);
            Debug.Log("No! It's this one!");
        
	}


}
