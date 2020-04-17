using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour {

     public CharacterController2D controller;
     public float wallKickDistance = 0.5f;

     [Header("Player Property")]
     [SerializeField] private float horizontalMove = 0f, runSpeed = 240f, phaseSpeed = 3000f, phaseSpeedNegative = -3000f, dropSpeed = 1f;
     
     private bool jump, isGameOver, isGamePaused, isGrounded, isFacingRight, 
          crouch = false, canDoubleJump = true, canAltJump = true, escapeKey = true, canPhase = true, rayCast, isFlipped, canShoot;
     
     private CharacterController2D characterController2D;
     private EnemyCollision enemyCollision;
     private PauseMenu pauseMenu;
     private AudioManager audioManager;
     private CoinCollision coinCollision;
     private GameObject player;
     private PlayerPosition playerPosition;
     private RaycastHit2D hitRight, hitLeft;
     private Rigidbody2D rigidbody2D;
     private Weapon weapon;

    // Update is called once per frame
    void Update() {
          characterController2D.highJump();
          // checkXboxPhysics();
          checkPhysics();
     
          if (escapeKey)
               checkEscapeKey();
          else
               return;
     
     }//end of Update()

     private void Awake() {
          characterController2D = FindObjectOfType<CharacterController2D>();
          enemyCollision = FindObjectOfType<EnemyCollision>();
          pauseMenu = FindObjectOfType<PauseMenu>();
          audioManager = FindObjectOfType<AudioManager>();
          rayCast = Physics2D.queriesStartInColliders = false;
          coinCollision = FindObjectOfType<CoinCollision>();
          player = GameObject.FindWithTag("Player");
          playerPosition = FindObjectOfType<PlayerPosition>();
          rigidbody2D = GetComponent<Rigidbody2D>();
          weapon = FindObjectOfType<Weapon>();
     }

     public float getRunSpeed() {
          return runSpeed;
     }

     public void setRunSpeed(float runSpeed) {
          this.runSpeed = runSpeed;
     }

     public float getPhaseSpeed() {
          return phaseSpeed;
     }

     public void setPhaseSpeed(float phaseSpeed) {
          this.phaseSpeed = phaseSpeed;
     }

     public float getNegativePhaseSpeed() {
          return phaseSpeedNegative;
     }

     public void setPhaseSpeedNegative(float phaseSpeedNegative) {
          this.phaseSpeedNegative = phaseSpeedNegative;
     }

     public bool getCanShoot() {
          return canShoot;
     }

     public void setCanShoot(bool canShoot){ 
          this.canShoot = canShoot;
     }

     void checkPhysics() {
          horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;
          checkJump();
          checkAltJump();
          // checkPhase();
          checkWallJump();
          // checkGroundPound();
          
          checkSceneRestart();
     }//end of checkPhysics()

     void checkXboxPhysics() { 
          if (Time.timeScale != 0.0f) {
               // checkJumpController();
               // checkPhaseController();
               // checkWallJumpController();
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

          /*Make a CharacterController2D object here instead of two bool objects*/ 
          isGrounded = characterController2D.getGrounded();

          /*If the player jumps...*/
          if (Input.GetButtonDown("Jump")) {
               if (Time.timeScale != 0.0f) { /*If the game isn't paused...*/
                    if (isGrounded && !checkAltJump()) { /*Single jump*/
                         audioManager.Play("Jump");
                         // characterController2D.highJump();
                         jump = true; /*addForce is being called in CharacterController2D.cs*/
                    }
                    if (!isGrounded && canDoubleJump && !checkAltJump()) { /*Double jump*/
                         audioManager.Play("DoubleJump");
                         applyForce(0f, 800f); /*To upgrade the jump height, check if upgrade is active with a boolean*/
                         canDoubleJump = false;
                    }
                    if (isGrounded) { /*Resets double jump when you touch the ground*/
                         audioManager.Play("PlayerGrounded");
                         canDoubleJump = true;
                    }
                    if (jump) /*Not sure why I included this...*/
                         return;
               }
          }
     }

     private bool checkAltJump() {
          isGrounded = characterController2D.getGrounded();

          if (!isGrounded && canAltJump) {
               if (Input.GetButtonDown("Jump")) {
                    audioManager.Play("DoubleJump");
                    applyForce(0f, 800f);
                    canAltJump = false;
                    return true;
               }
          }
          if (isGrounded)
               canAltJump = true;
               return false;
     }

     void checkPhase() {
          isGrounded = characterController2D.getGrounded();
          isFacingRight = characterController2D.getFacingRight();

          if (Time.timeScale != 0.0f) {
               if (Input.GetKeyDown(KeyCode.RightShift) && isFacingRight && !isGrounded && canPhase) {
                    audioManager.Play("Phase");
                    // applyForce(phaseSpeed, 0f);
                    rigidbody2D.velocity = new Vector2(110f, 0f);
                    canPhase = false;
               }
               else if (Input.GetKeyDown(KeyCode.RightShift) && !isFacingRight && !isGrounded && canPhase) {
                    audioManager.Play("Phase");
                    // applyForce(phaseSpeedNegative, 0f);
                    rigidbody2D.velocity = new Vector2(-110f, 0f);
                    canPhase = false;
               }    
               if (isGrounded)
                    canPhase = true;
          }
     }

     void checkWallJump() {
          hitRight = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallKickDistance);
          hitLeft = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, wallKickDistance);

          /*RIGHT WALL CLING*/
          if (!characterController2D.getGrounded() && hitRight.collider != null && Input.GetKey(KeyCode.RightArrow)) {
               wallJumpFunction(false, false, 0f, -50f, false, "No Sound");
          }
          /*LEFT WALL CLING*/
          else if (!characterController2D.getGrounded() && hitLeft.collider != null && Input.GetKey(KeyCode.LeftArrow)) {
               wallJumpFunction(false, false, 0f, -50f, false, "No Sound");
          }
          
          /*LEFT WALL JUMP*/
          if (Input.GetKeyDown(KeyCode.LeftArrow) && !characterController2D.getGrounded() && hitRight.collider != null)
               wallJumpFunction(true, true, -1250f, 650f, true, "WallJump");
          /*RIGHT WALL JUMP*/
          else if (Input.GetKeyDown(KeyCode.RightArrow) && !characterController2D.getGrounded() && hitLeft.collider != null)
               wallJumpFunction(true, true, 1250f, 650f, true, "WallJump");

          if (hitRight.collider != null || hitLeft.collider != null)
               canShoot = false; /*If you're clinging to a wall, you can't fire your weapon*/
          else
               canShoot = true;
     }

     void wallJumpFunction(bool canDoubleJump, bool canPhase,
                         float x, float y, bool isSound, string soundFile) {

          this.canDoubleJump = canDoubleJump;
          this.canPhase = canPhase;
          applyForce(x, y);
          
          if (isSound)
               audioManager.Play(soundFile);
          else
               soundFile = null;
     }

     void checkGroundPound() {
          if (!characterController2D.getGrounded() && Input.GetKey(KeyCode.DownArrow)) {
               // setGravity(10f);
               rigidbody2D.AddForce(Vector2.down * dropSpeed, ForceMode2D.Impulse);
               Debug.Log(Vector2.down * dropSpeed);
          }
          else if (!characterController2D.getGrounded() && Input.GetKeyUp(KeyCode.DownArrow))
               // setGravity(2f);

          if (characterController2D.getGrounded())
               setGravity(2f);
     }

     void setGravity(float gravity) {
          rigidbody2D.gravityScale = gravity;
     }

     public void applyForce(float phaseValue, float jumpValue) {
          characterController2D.addForce(phaseValue, jumpValue);
          /*Try this:
          rigidbody2d.velocity = new Vector2(dashSpeed, 0)
          This might solve your clipping issue*/
     }
     
     void checkSceneRestart() {
          if (Input.GetKeyDown(KeyCode.R)) {
               if (!isGameOver && !isGamePaused && playerPosition.getCheckPointSwitch()) {/*If the game isn't over AND the game isn't paused...*/
                    playerPosition.applyCheckPoint();
               } else if (!isGameOver && !isGamePaused && !playerPosition.getCheckPointSwitch())
                    playerPosition.applyInitialPoint();
          }
     }

    void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump); //crouch, jump
        jump = false;
    }
}//end of class


/*
Sources:
1) J.D., Day, 'Full Unity 2D Game Tutorial 2019 – Player Movement', 2019. [Online]. Available: https://www.gamedevelopment.blog/unity-2d-game-tutorial-2019-player-movement/ [Accessed: 10-Nov-2019].
2) S., Screenhog, 'Can I detect if a letter key is pressed', 2012. [Online]. Available: https://answers.unity.com/questions/345826/can-i-detect-if-a-letter-key-is-pressed-1.html [Accessed: 31-Jan-2020].
3) W.U.T., Wabble - Unity Tutorials, '58. Making a 2D Platformer in Unity (C#) - Wall Jump', 2015. [Online]. Available: https://www.youtube.com/watch?v=9QjwHsjbX_A [Accessed: Mar-09-2020].
*/