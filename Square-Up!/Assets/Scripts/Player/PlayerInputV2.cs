using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputV2 : MonoBehaviour {

    /*This class will:
    - control key/button inputs, such as:
        - Physics:
            - Jump
            - AltJump (running off a platform and then jumping before touching the ground or a wall. Ceilings are ok to touch)
            - Phase (a dash move to cover distance quickly)
            - WallJump
        - Scene Restarts*/

    [SerializeField] private CharacterController2D characterController2D;
    private AudioManager audioManager;
    private RaycastHit2D hitRight, hitLeft;
    private PlayerPosition playerPosition;
    [SerializeField] private float horizontalMove = 0f, 
        wallKickDistance = 0.5f, 
        dropSpeed = 1f, 
        runSpeed = 240f, 
        phaseSpeed = 3000f,
        phaseSpeedNegative = -3000f;

    private bool jump, 

            isGameOver, 
            isGamePaused, 
            isGrounded, 
            isFacingRight,
            isFlipped,

            crouch = false, 
            canDoubleJump = true, 
            canAltJump = true, 
            canShoot,
            canPhase = true, 
            
            escapeKey = true, 
            rayCast;
            
   
    // Start is called before the first frame update
    void Start() {
        
    }

    void FixedUpdate() {
        characterController2D.Move(horizontalMove * Time.fixedDeltaTime, crouch, jump);
        jump = false; // At the start of each frame, jump is false
    }

    void Awake() {
        characterController2D = FindObjectOfType<CharacterController2D>();
        playerPosition = FindObjectOfType<PlayerPosition>();
    }

    // Update is called once per frame
    void Update() {
        characterController2D.highJump(); // Used to add some weight to the player as they're falling
        if (Time.timeScale != 0.0f) { // if the game isn't paused, check the physics
            CheckPhysics();
        }
    }

    void CheckPhysics() {
        Jump();
        DoubleJump();
        AltJump();
        WallJump();
        WallCling();
        // GroundPound();
        CheckSceneRestart();
    }

    void Jump() {
        if (characterController2D.getGrounded() && Input.GetButtonDown("Jump")) {
            audioManager.Play("Jump");
            characterController2D.addForce(0, 600);
            jump = true;
        }
    }

    void DoubleJump() {
        if (!characterController2D.getGrounded() && canDoubleJump && !AltJump()) { /*Double jump*/
            Debug.Log("Double Jump");
            audioManager.Play("DoubleJump");
            characterController2D.addForce(0, 800); /*To upgrade the jump height, check if upgrade is active with a boolean*/
            canDoubleJump = false;
        }

        if (characterController2D.getGrounded()) { /*Resets double jump when you touch the ground*/
            audioManager.Play("PlayerGrounded");
            canDoubleJump = true;
        }
        if (jump)
            return;
    }

    bool AltJump() {
        if (!characterController2D.getGrounded() && canAltJump) {
            if (Input.GetButtonDown("Jump")) {
                Debug.Log("AltJump");
                audioManager.Play("DoubleJump");
                characterController2D.addForce(0, 800); /*This emulates the force of the double jump, which is slightly more powerful than the single jump*/
                canAltJump = false;
                return true;
            }
        }
        if (characterController2D.getGrounded()) /*resets alt jump*/
            canAltJump = true;
            return false;
    }

    void WallJump() {
        /*LEFT WALL JUMP*/
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !characterController2D.getGrounded() && hitRight.collider != null)
            wallJumpFunction(true, true, -1250f, 900f, true, "WallJump");
        /*RIGHT WALL JUMP*/
        else if (Input.GetKeyDown(KeyCode.RightArrow) && !characterController2D.getGrounded() && hitLeft.collider != null)
            wallJumpFunction(true, true, 1250f, 900f, true, "WallJump");

    }

    void WallCling() {
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

        /*Implementing this was easier and less bugs than figuring out how to get shooting to work while wall clinging*/
        if (hitRight.collider != null || hitLeft.collider != null)
            canShoot = false; /*If you're clinging to a wall, you can't fire your weapon*/
        else
            canShoot = true;
    }

    void GroundPound() {
        if (!characterController2D.getGrounded() && Input.GetKey(KeyCode.DownArrow)) {
            // setGravity(10f);
            GetComponent<Rigidbody2D>().AddForce(Vector2.down * dropSpeed, ForceMode2D.Impulse);
            Debug.Log(Vector2.down * dropSpeed);
        }
        else if (!characterController2D.getGrounded() && Input.GetKeyUp(KeyCode.DownArrow))
            // setGravity(2f);

        if (characterController2D.getGrounded())
            setGravity(2f);
    }

    void wallJumpFunction(bool canDoubleJump, bool canPhase,
        float x, float y, bool isSound, string soundFile) {

        this.canDoubleJump = canDoubleJump;
        this.canPhase = canPhase;
        characterController2D.addForce( x, y);
          
        if (isSound)
            audioManager.Play(soundFile);
        else
            soundFile = null;
     }

    void CheckSceneRestart() {
        if (Input.GetKeyDown(KeyCode.R)) {
            /*If the game isn't over AND the game isn't paused...*/
            if (!isGameOver && !isGamePaused && playerPosition.getCheckPointSwitch()) {
                playerPosition.applyCheckPoint();
            } else if (!isGameOver && !isGamePaused && !playerPosition.getCheckPointSwitch())
                playerPosition.applyInitialPoint();
        }
    }

    void setGravity(float gravity) {
          GetComponent<Rigidbody2D>().gravityScale = gravity;
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

}
