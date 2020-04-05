using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*This script will trigger if the player collects a coin*/

public class CoinCollision : MonoBehaviour {

    private int coinCount = 0;
    private bool coinIsPickedUp = false;
    private Transform coinPosition = null;
    private GameObject playerObject;
    private Player player;
    private CapsuleCollider2D capsuleCollider2D;
    private SpriteRenderer spriteRenderer;
    private AudioManager audioManager;
    private Timer timer;

    void Awake() {
        coinPosition = GetComponent<Transform>();
        playerObject = GameObject.FindWithTag("Player");
        player = FindObjectOfType<Player>();
        capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioManager = FindObjectOfType<AudioManager>();
        timer = FindObjectOfType<Timer>();
    }
    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Bullet")
            return;
        else
            checkForCoinCollision();
    }

    void checkForCoinCollision() {
        
        if (!coinIsPickedUp) {
            player.addCoin(1);
            coinIsPickedUp = true;
            capsuleCollider2D.enabled = !capsuleCollider2D.enabled;
            spriteRenderer.enabled = !spriteRenderer.enabled;
            
            if (player.getCoinCount() >= 8) {
                timer.setIsTimerRunning(false);
                audioManager.Play("Win Level");
                Invoke("showGameComplete", 2);
            }
            audioManager.Play("CoinCollect");
        } 
        else if (coinIsPickedUp) 
            return;
    }

    public bool getCoinIsPickedUp() {
        return coinIsPickedUp;
    }

    public Transform getCoinPosition() {
        return coinPosition;
    }

    public int getCoinCount() {
        return coinCount;
    }

    public void showGameComplete() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}