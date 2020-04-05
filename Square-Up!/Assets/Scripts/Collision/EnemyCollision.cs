using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*This script will detect collision with the player*/

public class EnemyCollision : MonoBehaviour {

    // private bool isPlayerInvincible = false; /*Put this in Player.cs [DONE]*/
    private Player player;
    private AudioManager audioManager;
    private GameOverMenu gameOverMenu;

    public void Restart_Scene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void Awake() {
        audioManager = FindObjectOfType<AudioManager>();
        player = FindObjectOfType<Player>();
        gameOverMenu = FindObjectOfType<GameOverMenu>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (!player.getPlayerInvincible()) {
            if (collision.gameObject.name == "Player") {
                player.takeDamage(1, 25);
                Debug.Log("It's this one!");
            }
        }
	}

    public bool checkGameOver() {
        if (player.currentHealth <= 0) {
            if (Time.timeScale == 1f) {
                Time.timeScale = 0f;
                gameOverMenu.showGameOverScreen();
            }
            return true;
        }
        else
            return false;
    }
} // end of class
 

 /*
Sources:
1) S., superluigi, '2D When player gets hit make invulnerable for a few seconds', 2013. [Online]. Available: https://answers.unity.com/questions/478155/2d-when-player-gets-hit-make-invulnerable-for-a-fe.html [Accessed: 06-Mar-2020].
*/