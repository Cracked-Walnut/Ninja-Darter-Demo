using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


/*This script holds attributes regarding the player: amount of hit points,
whether the player took damage, and whether the player gained hit point(s)*/

public class Player : MonoBehaviour {
    
    private bool isPickedUp = false, isPlayerInvincible = false;
    private int coinCount = 0;
    [SerializeField] private int maxHealth = 5, currentHealth = 5;
    private Rigidbody2D playerRigidBody;
    private SceneLoader sceneLoader;
    private CharacterController2D characterController2D;
    private EnemyCollision enemyCollision;
    private AudioManager audioManager;
    [SerializeField] private HealthBar healthBar;

    // Start is called before the first frame update
    void Start() {
        // currentHealth = maxHealth;
        // healthBar.setMaxHealth(maxHealth);
    }

    private void Awake() {
        playerRigidBody = GetComponent<Rigidbody2D>();
        characterController2D = FindObjectOfType<CharacterController2D>();
        audioManager = FindObjectOfType<AudioManager>();
        enemyCollision = FindObjectOfType<EnemyCollision>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void takeDamage(int damage, int knockBackHeight) { /*Works with EnemyCollision.cs*/

        // setPlayerInvincible(true);
        audioManager.Play("Damage");

        currentHealth = currentHealth - damage;
        healthBar.setHealth(currentHealth);

        // StartCoroutine(knockBackX(0.02f, knockBackHeight, transform.position));
        // Invoke("resetInvincibilityState", 1.0f); /*Wait one second then execute function*/
        // enemyCollision.checkGameOver();
    }

    void resetInvincibilityState() {
        setPlayerInvincible(false);
    }

    public void gainHealth(int heartPoint) { /*Works with HeartCollision.cs*/

        if (currentHealth < maxHealth) {
            /*Play a sound here*/
            currentHealth = currentHealth + heartPoint; /*Add HP*/
            healthBar.setHealth(currentHealth);
            Debug.Log("HP: " + currentHealth + "/ " + maxHealth);
        }
    }//end of gainHealth()

    public bool healthIsFull() {
        if (currentHealth == maxHealth || currentHealth > maxHealth) {
            
            currentHealth = maxHealth; /*Make it equal to max HP...*/
            healthBar.setMaxHealth(currentHealth); /*..and then update the UI with that same value*/
            Debug.Log("HP Full!: " + currentHealth + "/ " + maxHealth);
            
            return true;
        }
        else
            return false;
    }

    public IEnumerator knockBackX(float knockBackDuration, float knockBackPower, Vector3 knockBackDirection) { /*Works with EnemyCollision.cs*/
        float timer = 0;

        while (knockBackDuration > timer) {
            timer += Time.deltaTime;

            if (characterController2D.getFacingRight())            
                characterController2D.addForce(-1500f, 1100f);
            else 
                characterController2D.addForce(1500f, 1100f);
        }
        yield return 0;
    }

    public bool getPlayerInvincible() { return isPlayerInvincible; }

    public void setPlayerInvincible (bool inv) { isPlayerInvincible = inv; }

    public int getCoinCount() { return coinCount; }

    public void addCoin (int coin) { coinCount += coin; }

    public int getCurrentHealth() { return currentHealth; }

    public int getMaxHealth() { return maxHealth; }

    public void setMaxHealth (int maxHealth) { this.maxHealth = maxHealth; }
} //end of class


/*
Sources:
1) G.D., GucioDevs, 'Unity 5 2D Platformer Tutorial - Part 15 - Spike Knockback / knockup', 2015. [Online]. Available: https://www.youtube.com/watch?v=-dMtWZsjX6g [Accessed: 06-Mar-2020].
*/