using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Controls player movement (running, jumping, wall jumping)*/
public class PlayerInput : MonoBehaviour {

     [SerializeField] private CharacterController2D characterController2D;

     [SerializeField] private float horizontalMove = 0f, 
          runSpeed = 240f,
          phaseSpeed = 700f,
          phaseSpeedNegative = -700f, 
          dropSpeed = 1f,
          wallKickDistance = 0.5f;
          
     private float pulseJumpTimer;
     private bool jump, 
          isGameOver, 
          isGamePaused, 
          isGrounded, 
          isFacingRight, 
          crouch = false, 
          canDoubleJump = true, 
          canAltJump = true, 
          escapeKey = true, 
          canPhase = true, 
          rayCast, 
          isFlipped, 
          canShoot;

     private PauseMenu pauseMenu;
     private AudioManager audioManager;
     private GameObject player;
     private PlayerPosition playerPosition;
     private RaycastHit2D wallClingColRight, wallClingColLeft, wallJumpColRight, wallJumpColLeft;
     private Rigidbody2D rigidbody2D;

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
          pulseJumpTimer = 1.0f;
     }

     /*Also called once per frame like Update but I think it's used for time sensitive variables*/
     void FixedUpdate() {
        characterController2D.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false; // At the start of each frame, jump is false
    }

     // Used to initialize variables. Can also be done in Start()
     private void Awake() {
          characterController2D = FindObjectOfType<CharacterController2D>();
          pauseMenu = FindObjectOfType<PauseMenu>();
          audioManager = FindObjectOfType<AudioManager>();
          rayCast = Physics2D.queriesStartInColliders = false;
          player = GameObject.FindWithTag("Player");
          playerPosition = FindObjectOfType<PlayerPosition>();
          rigidbody2D = GetComponent<Rigidbody2D>();
     }

     // This is the only function I'm calling in Update(), so all frame dependent functions are called in here
     void checkPhysics() {
          horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

          checkJump();
          checkDoubleJump();
          checkAltJump();
          checkPhase();
          checkWallCling();
          checkWallJump();
          // checkGroundPound();
          checkPulseJump();
          checkSceneRestart();
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
          /*If the player jumps...*/
          if (Input.GetButtonDown("Jump") && Time.timeScale != 0.0f && 
               characterController2D.getGrounded() && !checkAltJump()) 
          {
               audioManager.Play("Jump");
               // characterController2D.highJump();
               characterController2D.addForce(0, 600);
               jump = true; /*addForce is being called in CharacterController2D.cs*/
                    
               if (jump) /*Not sure why I included this...*/
                    return;
               
          }
     }

     void checkDoubleJump() {
          if (Input.GetButtonDown("Jump")) {
               if (!characterController2D.getGrounded() && canDoubleJump && !checkAltJump()) { /*Double jump*/
                    audioManager.Play("DoubleJump");
                    characterController2D.addForce(0, 800); /*To upgrade the jump height, check if upgrade is active with a boolean*/
                    canDoubleJump = false;
               }
          }

          if (characterController2D.getGrounded()) { /*Resets double jump when you touch the ground*/
               audioManager.Play("PlayerGrounded");
               canDoubleJump = true;
          }
     }

     /*If the player walks off a platform and wants to jump before touching the ground...*/
     private bool checkAltJump() { 
          isGrounded = characterController2D.getGrounded();

          if (Input.GetButtonDown("Jump") && !isGrounded && canAltJump) {
               audioManager.Play("DoubleJump");
               characterController2D.addForce(0, 800); /*This emulates the force of the double jump, which is slightly more powerful than the single jump*/
               canAltJump = false;
               return true;
          }
          if (isGrounded) /*resets alt jump*/
               canAltJump = true;
               return false;
     }

     void checkPhase() { // dash ability for the player
          isGrounded = characterController2D.getGrounded();
          isFacingRight = characterController2D.getFacingRight();

          if (Time.timeScale != 0.0f) { // right dash
               if (Input.GetKeyDown(KeyCode.RightShift) && isFacingRight && !isGrounded && canPhase) {
                    audioManager.Play("Phase");
                    // applyForce(phaseSpeed, 0f);
                    rigidbody2D.velocity = new Vector2(phaseSpeed, 0f);
                    canPhase = false; // you can only use the ability once in the air. must touch the ground to reset
               }
               else if (Input.GetKeyDown(KeyCode.RightShift) && !isFacingRight && !isGrounded && canPhase) {
                    // left dash
                    audioManager.Play("Phase");
                    // applyForce(phaseSpeedNegative, 0f);
                    rigidbody2D.velocity = new Vector2(phaseSpeedNegative, 0f);
                    canPhase = false;
               }    
               if (isGrounded) // touch the ground to reset dash
                    canPhase = true;
          }
     }

     // I'm going to take the wall cling functions out of here and put them into their own function
     void checkWallCling() {
          wallClingColRight = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallKickDistance);
          wallClingColLeft = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, wallKickDistance);

          /*RIGHT WALL CLING*/
          if (!characterController2D.getGrounded() && wallClingColRight.collider != null && Input.GetKey(KeyCode.RightArrow)) {
               wallFunction(false, false, 0f, -50f, false, "No Sound");
          }
          /*LEFT WALL CLING*/
          else if (!characterController2D.getGrounded() && wallClingColLeft.collider != null && Input.GetKey(KeyCode.LeftArrow)) {
               wallFunction(false, false, 0f, -50f, false, "No Sound");
          }

          /*Implementing this was easier and less bugs than figuring out how to get shooting to work while wall clinging*/
          if (wallClingColRight.collider != null || wallClingColLeft.collider != null)
               canShoot = false; /*If you're clinging to a wall, you can't fire your weapon*/
          else
               canShoot = true;
     }

     void checkWallJump() {
          wallJumpColRight = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallKickDistance);
          wallJumpColLeft = Physics2D.Raycast(transform.position, Vector2.left * transform.localScale.x, wallKickDistance);

          /*LEFT WALL JUMP*/
          if (Input.GetKeyDown(KeyCode.LeftArrow) && !characterController2D.getGrounded() && wallJumpColRight.collider != null)
               wallFunction(true, true, -1250f, 900f, true, "WallJump");
          /*RIGHT WALL JUMP*/
          else if (Input.GetKeyDown(KeyCode.RightArrow) && !characterController2D.getGrounded() && wallJumpColLeft.collider != null)
               wallFunction(true, true, 1250f, 900f, true, "WallJump");
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

     // currently not in use. There's a bug where you can clip through the floor if you move fast enough
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

     void checkPulseJump() {
          if (Input.GetKeyDown(KeyCode.X))
               rigidbody2D.AddForce(Vector2.up * 25, ForceMode2D.Impulse);
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

     void setGravity(float gravity) {
          rigidbody2D.gravityScale = gravity;
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

     public void setCanShoot(bool canShoot) { 
          this.canShoot = canShoot;
     }
}//end of class


/*
Sources:
1) J.D., Day, 'Full Unity 2D Game Tutorial 2019 – Player Movement', 2019. [Online]. Available: https://www.gamedevelopment.blog/unity-2d-game-tutorial-2019-player-movement/ [Accessed: 10-Nov-2019].
2) S., Screenhog, 'Can I detect if a letter key is pressed', 2012. [Online]. Available: https://answers.unity.com/questions/345826/can-i-detect-if-a-letter-key-is-pressed-1.html [Accessed: 31-Jan-2020].
3) W.U.T., Wabble - Unity Tutorials, '58. Making a 2D Platformer in Unity (C#) - Wall Jump', 2015. [Online]. Available: https://www.youtube.com/watch?v=9QjwHsjbX_A [Accessed: Mar-09-2020].
*/