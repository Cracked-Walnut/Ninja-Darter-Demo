using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*Controls player movement (running, jumping, wall jumping)*/
public class PlayerInput : MonoBehaviour {

     [SerializeField] private CharacterController2D characterController2D;
     [SerializeField] private Transform groundDetection;
     [SerializeField] private float groundDistance;

     private float horizontalMove = 0f, 
          runSpeed = 20f,
          phaseSpeed = 700f,
          wallJumpX = 1250f,
          wallJumpY = 1100f,
          pulseForce = 2000f,
          // phaseSpeedNegative = -700f, 
          dropSpeed = 1f,
          wallKickDistance = 0.5f,
          jumpGroundDetection = 0.1f;
          
     private float pulseJumpTimer;
     private float pulseJumpSeconds = 0.85f;
     private bool jump, 
          isGameOver, 
          isGamePaused, 
          isGrounded, 
          isFacingRight, 
          crouch = false, 
          canDoubleJump = true, 
          canAltJump = true,
          canPulseJump = true, 
          escapeKey = true, 
          canPhase = true, 
          rayCast, 
          isFlipped, 
          canShoot = true;

     private PauseMenu pauseMenu;
     private AudioManager audioManager;
     // private GameObject player;
     private Player player;
     private PlayerPosition playerPosition;
     private RaycastHit2D wallClingColRight, wallClingColLeft, wallJumpColRight, wallJumpColLeft, groundInfo;
     private Rigidbody2D rigidbody2D;
     private Weapon weapon;

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
        jump = false; // At the start of each frame, jump is false
    }

     // Used to initialize variables. Can also be done in Start()
     private void Awake() {
          characterController2D = FindObjectOfType<CharacterController2D>();
          pauseMenu = FindObjectOfType<PauseMenu>();
          audioManager = FindObjectOfType<AudioManager>();
          rayCast = Physics2D.queriesStartInColliders = false;
          // player = GameObject.FindWithTag("Player");
          player = FindObjectOfType<Player>();
          playerPosition = FindObjectOfType<PlayerPosition>();
          rigidbody2D = GetComponent<Rigidbody2D>();
          weapon = GetComponent<Weapon>();
     }

     // This is the only function I'm calling in Update(), so all frame dependent functions are called in here
     void checkPhysics() {
          horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

          // if (Time.timeScale != 0.0f) {
               checkJump();
               checkDoubleJump();
               checkPhase();
               checkWallCling();
               checkWallJump();
               // checkGroundPound();
               checkPulseJump();
          // }
          // checkSceneRestart();
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
          if (Input.GetButtonDown("Jump") && groundInfo.collider == true) {
               
               audioManager.Play("Jump");
               Debug.Log("Jump");
               characterController2D.addForce(0, 1000);
          }
     }

     void checkDoubleJump() {
          groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
          if (Input.GetButtonDown("Jump")) {
               if (groundInfo.collider == false && canDoubleJump) { /*Double jump*/

                    audioManager.Play("DoubleJump");
                    Debug.Log("DoubleJump");
                    characterController2D.addForce(0, 1200); /*To upgrade the jump height, check if upgrade is active with a boolean*/
                    canDoubleJump = false;
               }
          }

          if (groundInfo.collider == true/*characterController2D.getGrounded()*/) { // resets double jump when you touch the ground
               audioManager.Play("PlayerGrounded");
               canDoubleJump = true;
          }
     }

     void checkPhase() { // dash ability for the player
          isFacingRight = characterController2D.getFacingRight();
          groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);

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
               wallFunction(false, false, 0f, -100f, false, "No Sound");
               canShoot = false;
               // characterController2D.setFacingRight(false);
          }
          /*LEFT WALL CLING*/
          else if (groundInfo.collider == false && wallClingColLeft.collider != null && Input.GetKey(KeyCode.LeftArrow) && rigidbody2D.velocity.y < 0) {
               wallFunction(false, false, 0f, -100f, false, "No Sound");
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
               wallFunction(true, true, wallJumpX - (wallJumpX * 2f), wallJumpY, true, "WallJump");

          /*RIGHT WALL JUMP*/
          else if (Input.GetKeyDown(KeyCode.RightArrow) && groundInfo.collider == false && wallJumpColLeft.collider != null)
               wallFunction(true, true, wallJumpX, wallJumpY, true, "WallJump");
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

     void setGravity(float gravity) { rigidbody2D.gravityScale = gravity; }

     public float getRunSpeed() { return runSpeed; }
     public void setRunSpeed(float runSpeed) { this.runSpeed = runSpeed; }

     public float getPhaseSpeed() { return phaseSpeed; }
     public void setPhaseSpeed(float phaseSpeed) { this.phaseSpeed = phaseSpeed; }

     // public float getNegativePhaseSpeed() {return phaseSpeedNegative;}
     // public void setPhaseSpeedNegative(float phaseSpeedNegative) {this.phaseSpeedNegative = phaseSpeedNegative;}

     public bool getCanShoot() { return canShoot; }
     public void setCanShoot(bool canShoot) { this.canShoot = canShoot; }

     public float getPulseJumpTimer() { return pulseJumpTimer; }
     public float getPulseJumpSeconds() { return pulseJumpSeconds; }
     public void setPulseJumpSeconds (float pulseJumpSeconds) { this.pulseJumpSeconds = pulseJumpSeconds; }

     public float getWallJumpX() { return wallJumpX; }
     public float getWallJumpY() {return wallJumpY; }

     public float getPulseForce() { return pulseForce; }
     public void setPulseForce(float pulseForce) { this.pulseForce = pulseForce; }
}//end of class


/*
Sources:
1) J.D., Day, 'Full Unity 2D Game Tutorial 2019 – Player Movement', 2019. [Online]. Available: https://www.gamedevelopment.blog/unity-2d-game-tutorial-2019-player-movement/ [Accessed: 10-Nov-2019].
2) S., Screenhog, 'Can I detect if a letter key is pressed', 2012. [Online]. Available: https://answers.unity.com/questions/345826/can-i-detect-if-a-letter-key-is-pressed-1.html [Accessed: 31-Jan-2020].
3) W.U.T., Wabble - Unity Tutorials, '58. Making a 2D Platformer in Unity (C#) - Wall Jump', 2015. [Online]. Available: https://www.youtube.com/watch?v=9QjwHsjbX_A [Accessed: Mar-09-2020].
*/