﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Controls player movement (running, jumping, wall jumping)*/
public class PlayerInput : MonoBehaviour {

     [SerializeField] private CharacterController2D characterController2D;
     [SerializeField] private Transform groundDetection;
     [SerializeField] private float groundDistance, stepDistance;
     [SerializeField] private Animator animator;
     [SerializeField] private Transform attackPoint;
     [SerializeField] private float attackRange;
     [SerializeField] private float attackRate = 2f;
     [SerializeField] private float nextAttackTime = 0f;
     [SerializeField] private float reset;
     [SerializeField] private LayerMask enemyLayers, projectileLayers;
     [SerializeField] private ParticleSystem dustEffect;
     [SerializeField] private CameraFollow cameraFollow;

     private float horizontalMove = 0f, runSpeed = 200f, rollSpeed = 300f, phaseSpeed = 700f, wallJumpX = 2500f, wallJumpY = 1750f,
          jumpForce = 1800f, doubleJumpForce = 2200f, pulseForce = 2000f, stepForce = 100f, dropSpeed = 1f, wallKickDistance = 1f,
          jumpGroundDetection = 0.1f, pulseJumpTimer, pulseJumpSeconds = 0.85f;


     private bool jump, isGameOver, isGamePaused, isGrounded, isFacingRight, crouch = false, canDoubleJump = true, canAltJump = true,
          canPulseJump = true, escapeKey = true, canPhase = true, rayCast, isFlipped, canShoot = true;

     private PauseMenu pauseMenu;
     private AudioManager audioManager;
     private Player player;
     private PlayerPosition playerPosition;
     private RaycastHit2D wallClingColRight, wallClingColLeft, wallJumpColRight, wallJumpColLeft, groundInfo;
     private Rigidbody2D rigidbody2D;
     private Weapon weapon;
     private string[] comboList = new string[3] {"Attack1", "Attack2", "Attack3"};

     // plays when you hit an enemy
     private string[] swordDamageList = new string[3] {
          "Dagger_Damage_1", "Dagger_Damage_2", "Dagger_Damage_3" }; 
     
     // one of these will play when the player swings the sword
     private string[] swordWhooshList = new string[6] {
          "Sword_Whoosh_1", "Sword_Whoosh_2", "Sword_Whoosh_3", "Sword_Whoosh_4", "Sword_Whoosh_5", "Sword_Whoosh_6" };
     
     // one of these will play when the player takes a step in the run animation
     private string[] groundStepList = new string[5] {
          "Ground_Step0", "Ground_Step1", "Ground_Step2", "Ground_Step3", "Ground_Step4" };

     private string[] jumpSoundList = new string [3] {
          "Forest_ground_jump0", "Forest_ground_jump1", "Forest_ground_jump2" };

     private string[] deflectionSoundList = new string [4] {
          "JBoB_Special_Sound_09", "JBoB_Special_Sound_10", "JBoB_Special_Sound_11", "JBoB_Special_Sound_12" };


/*------------------------------------------------------------------------------------------------------------------------------------------------*/

    // Update is called once per frame
    void Update() {
          characterController2D.highJump(); // Used to add some weight to the player as they're falling
          checkPhysics();
     
          if (escapeKey) // Check if the escape key is enabled. Useful for instance when the player, they shouldn't be able to pause
               checkEscapeKey();
          else
               return; // I don't rhink this is needed but I've kept it here anyway
     }

     // Called at before the first frame
     void Start() {
          pulseJumpTimer = pulseJumpSeconds;
          groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
     }

     /*Also called once per frame like Update but I think it's used for time sensitive variables*/
     void FixedUpdate() {
        characterController2D.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        animator.SetFloat("SpeedX", Mathf.Abs(horizontalMove));
        jump = false; // At the start of each frame, jump is false
    }

     // Used to initialize variables. Can also be done in Start()
     private void Awake() {
          characterController2D = FindObjectOfType<CharacterController2D>();
          pauseMenu = FindObjectOfType<PauseMenu>();
          audioManager = FindObjectOfType<AudioManager>();
          rayCast = Physics2D.queriesStartInColliders = false;
          player = FindObjectOfType<Player>();
          playerPosition = FindObjectOfType<PlayerPosition>();
          rigidbody2D = GetComponent<Rigidbody2D>();
          weapon = GetComponent<Weapon>();
     }

     // This is the only function I'm calling in Update(), so all frame dependent functions are called in here
     void checkPhysics() {
          horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

          if (Time.timeScale != 0.0f) {
               checkJump();
               checkCrouch();
               checkDoubleJump();
               // checkPhase();
               // checkWallCling();
               // checkWallJump();
               // checkPulseJump();
               checkAttack();
          }
     }

     private void checkEscapeKey() {

        if (Input.GetKeyDown(KeyCode.Escape)) {
               if (pauseMenu.getGameIsPaused()) 
                    pauseMenu.ResumeGame();
               else 
                    pauseMenu.PauseGame();
        }
    }

    public void setEscapeKey(bool escapeKey) {
          this.escapeKey = escapeKey;
    }

     void checkJump() {
          groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
          /*If the player jumps...*/
          if (Input.GetButtonDown("Jump") && groundInfo.collider != false) {
               characterController2D.addForce(0, jumpForce);
               playJumpSound();
               animator.SetBool("isGrounded", false);
               CreateDust();
          }

          if (groundInfo.collider == false && rigidbody2D.velocity.y > 1) {
               
               animator.SetFloat("SpeedY", rigidbody2D.velocity.y);
               animator.SetTrigger("JumpUp");
          }
          
          else if (groundInfo.collider == false && rigidbody2D.velocity.y < -1) {
               
               animator.SetTrigger("JumpDown");
               animator.SetFloat("SpeedY", rigidbody2D.velocity.y);
          
          }
          if (groundInfo.collider == true && rigidbody2D.velocity.y > -1 && rigidbody2D.velocity.y < 1) {
               animator.SetFloat("SpeedY", rigidbody2D.velocity.y);
               animator.SetBool("isGrounded", true);
          }
     }

     void checkCrouch() {
          if (Input.GetKey(KeyCode.DownArrow))
               animator.SetBool("isCrouching", true);
          else
               animator.SetBool("isCrouching", false);
     }

     void checkDoubleJump() {
          groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
          if (Input.GetButtonDown("Jump")) {
               if (groundInfo.collider == false && canDoubleJump) { /*Double jump*/

                    playJumpSound();
                    CreateDust();
                    Debug.Log("DoubleJump");
                    characterController2D.addForce(0, doubleJumpForce); /*To upgrade the jump height, check if upgrade is active with a boolean*/
                    canDoubleJump = false;
               }
          }

          if (groundInfo.collider == true/*characterController2D.getGrounded()*/) { // resets double jump when you touch the ground
               // audioManager.Play("PlayerGrounded");
               canDoubleJump = true;
          }
     }

     void checkPhase() { // dash ability for the player
          isFacingRight = characterController2D.getFacingRight();
          groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
          float rollSpeedDropMultiplier = 5f;

               // right dash
               if (Input.GetKeyDown(KeyCode.RightShift) && isFacingRight && groundInfo.collider == false && canPhase) {
                    audioManager.Play("Phase");
                    setPhaseSpeed(700f);
                    rigidbody2D.velocity = new Vector2(getPhaseSpeed(), 0f);

                    canPhase = false; // you can only use the ability once in the air. must touch the ground to reset
               }
               else if (Input.GetKeyDown(KeyCode.RightShift) && !isFacingRight && groundInfo.collider == false && canPhase) {
                    // left dash
                    audioManager.Play("Phase");
                    setPhaseSpeed(-700f);
                    rigidbody2D.velocity = new Vector2(getPhaseSpeed(), 0f);
                    canPhase = false;
               }
               if (groundInfo.collider == true) // touch the ground to reset dash
                    canPhase = true;
     }

     // I'm going to take the wall cling functions out of here and put them into their own function
     void checkWallCling() {
          groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
          wallClingColRight = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallKickDistance);
          wallClingColLeft = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, wallKickDistance);
          bool turnAround = true;

          /*RIGHT WALL CLING*/
          if (groundInfo.collider == false && wallClingColRight.collider != null && Input.GetKey(KeyCode.RightArrow) && rigidbody2D.velocity.y < 0) {
               wallFunction(false, false, 0f, -50f, false, "No Sound");
               canShoot = false;
               // characterController2D.setFacingRight(false);
          }
          /*LEFT WALL CLING*/
          else if (groundInfo.collider == false && wallClingColLeft.collider != null && Input.GetKey(KeyCode.LeftArrow) && rigidbody2D.velocity.y < 0) {
               wallFunction(false, false, 0f, -50f, false, "No Sound");
               canShoot = false;
               // characterController2D.setFacingRight(true);
          }
          else
               canShoot = true;
     }

     void checkWallJump() {
          groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
          wallJumpColRight = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallKickDistance);
          wallJumpColLeft = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, wallKickDistance);

          /*LEFT WALL JUMP, wallJumpX calculation is just some math to retrieve the negative value of WallJumpX, since we'll
          be wall jumping to the left*/
          if (Input.GetKeyDown(KeyCode.LeftArrow) && groundInfo.collider == false && wallJumpColRight.collider != null)
               wallFunction(false, true, wallJumpX - (wallJumpX * 2f), wallJumpY, true, "WallJump");

          /*RIGHT WALL JUMP*/
          else if (Input.GetKeyDown(KeyCode.RightArrow) && groundInfo.collider == false && wallJumpColLeft.collider != null)
               wallFunction(false, true, wallJumpX, wallJumpY, true, "WallJump");
     }

     void wallFunction(bool canDoubleJump, bool canPhase,
          float x, float y, bool isSound, string soundFile) 
     {
          this.canDoubleJump = canDoubleJump;
          this.canPhase = canPhase;
          characterController2D.addForce(x, y);
          
          if (isSound)
               audioManager.Play(soundFile);
          else
               soundFile = null;
     }

     // player must be grounded to perform pulse jump!
     void checkPulseJump() {
          RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
          // if the player holds down the X key, start the countdown
          if (Input.GetKey(KeyCode.X) && canPulseJump) { 
               pulseJumpTimer -= Time.deltaTime;

               // if the player succeeds in holding down X till the countdown reaches 0.0, launch the player upwards, 
               // reset the timer and make the bool false so they can't jump till they land
               if (pulseJumpTimer <= 0.0f) {
                    characterController2D.addForce(0, pulseForce);
                    pulseJumpTimer = pulseJumpSeconds;
                    canPulseJump = false;
               }

               // if the player is in the air and continue to hold X, make sure pulseJump doesn't 
               // decrement
               if (Input.GetKey(KeyCode.X) && groundInfo.collider == false)
                    pulseJumpTimer = pulseJumpSeconds;
          }

          // if the player lets the X key go before the countdown ends, reset the countdown
          if (Input.GetKeyUp(KeyCode.X) && pulseJumpTimer > 0.0f && groundInfo.collider == true)
               pulseJumpTimer = pulseJumpSeconds;
          
          // reset pulse jump upon landing
          if (groundInfo.collider == true && !canPulseJump)
               canPulseJump = true;
          
     }
     
     void checkSceneRestart() {
          if (Input.GetKeyDown(KeyCode.R)) {
               /*If the game isn't over AND the game isn't paused...*/
               if (!isGameOver && !isGamePaused && playerPosition.getCheckPointSwitch()) {
                    playerPosition.applyCheckPoint();
               } else if (!isGameOver && !isGamePaused && !playerPosition.getCheckPointSwitch())
                    playerPosition.applyInitialPoint();
          }
     }

     void checkAttack() {
          if (Input.GetKeyDown(KeyCode.Space)) {
                    if (Time.time > nextAttackTime) {

                         playArrSound(swordWhooshList);
                         playSwordAnimation();
                         nextAttackTime = Time.time + 1f / attackRate; // add attackRate (0.5f) to the current time. If current time exceed 0.5
                         // seconds, you can attack again
                    }
          }
     }
     void registerHit() {

          // detect enemies in range of attack
          // A circle which detects enemies
          Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

          foreach(Collider2D enemiesHit in hitEnemies) {

              enemiesHit.GetComponent<Wisp>().takeDamage(50); // damage the enemy
               StartCoroutine(cameraFollow.Shake(.15f, .15f)); // shake the camera for effect
               playArrSound(swordDamageList); // play a damage sound
          }
     }

     void registerDeflection() {
          Collider2D[] hitProjectiles = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, projectileLayers);

          foreach(Collider2D projectilesHit in hitProjectiles) {

              projectilesHit.GetComponent<Projectile>().takeDamage(100); // damage the projectile to destroy it
               StartCoroutine(cameraFollow.Shake(.1f, .075f)); // shake the camera for effect
               playArrSound(deflectionSoundList); // play a deflection sound
          }
     }

     void playArrSound(string[] soundArray) {
          // plays a sound from any specified array
          int randomSound = Random.Range(0, soundArray.Length);
          audioManager.Play(soundArray[randomSound]);
     }

     void playSteppingSound() {
          int randomSound = Random.Range(0, groundStepList.Length);
          audioManager.Play(groundStepList[randomSound]);
     }

     void playJumpSound() {
          int randomSound = Random.Range(0, jumpSoundList.Length);
          audioManager.Play(jumpSoundList[randomSound]);
     }

     void playSwordAnimation() {
          // play an attack animation
          int randomMove = Random.Range(0, comboList.Length);
          animator.SetTrigger(comboList[randomMove]);
     }

     public void CreateDust() {
          dustEffect.Play();
     }


     void setGravity(float gravity) { rigidbody2D.gravityScale = gravity; }

     public float getRunSpeed() { return runSpeed; }
     public void setRunSpeed(float runSpeed) { this.runSpeed = runSpeed; }

     public float getPhaseSpeed() { return phaseSpeed; }
     public void setPhaseSpeed(float phaseSpeed) { this.phaseSpeed = phaseSpeed; }

     public bool getCanShoot() { return canShoot; }
     public void setCanShoot(bool canShoot) { this.canShoot = canShoot; }

     public float getPulseJumpTimer() { return pulseJumpTimer; }
     public float getPulseJumpSeconds() { return pulseJumpSeconds; }
     public void setPulseJumpSeconds (float pulseJumpSeconds) { this.pulseJumpSeconds = pulseJumpSeconds; }

     public float getWallJumpX() { return wallJumpX; }
     public float getWallJumpY() { return wallJumpY; }

     public float getPulseForce() { return pulseForce; }
     public void setPulseForce(float pulseForce) { this.pulseForce = pulseForce; }
}//end of class


/*
Sources:
1) J.D., Day, 'Full Unity 2D Game Tutorial 2019 – Player Movement', 2019. [Online]. Available: https://www.gamedevelopment.blog/unity-2d-game-tutorial-2019-player-movement/ [Accessed: 10-Nov-2019].
2) S., Screenhog, 'Can I detect if a letter key is pressed', 2012. [Online]. Available: https://answers.unity.com/questions/345826/can-i-detect-if-a-letter-key-is-pressed-1.html [Accessed: 31-Jan-2020].
3) W.U.T., Wabble - Unity Tutorials, '58. Making a 2D Platformer in Unity (C#) - Wall Jump', 2015. [Online]. Available: https://www.youtube.com/watch?v=9QjwHsjbX_A [Accessed: Mar-09-2020].
5) B., Brackeys, '2D Animation in Unity (Tutorial)', 2018. [Online]. Available: https://www.youtube.com/watch?v=hkaysu1Z-N8 [Accessed: 24-Jul-2020].
4) B., Brackeys, 'MELEE COMBAT in Unity', 2019. [Online]. Available: https://www.youtube.com/watch?v=sPiVz1k-fEs [Accessed: Jul-25-2020].
6) P.S., Start, 'Dust Effect when Running & Jumping in Unity [Particle Effect]', 2019. [Online]. Available: https://www.youtube.com/watch?v=1CXVbCbqKyg [Accessed: Jul-28-2020].
*/