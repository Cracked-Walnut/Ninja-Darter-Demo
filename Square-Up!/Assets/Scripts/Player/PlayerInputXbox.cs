using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputXbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // void checkJumpController() {

     //      /*Make a CharacterController2D object here instead of two bool objects*/ 
     //      isGrounded = characterController2D.getGrounded();
     //      isFacingRight = characterController2D.getFacingRight();
     //      bool xbox_a = Input.GetButtonDown("XboxA");

     //      /*If the player jumps...*/
     //      if (xbox_a) {
     //                if (isGrounded) { /*Single jump*/
     //                     audioManager.Play("Jump");
     //                     jump = true; /*addForce is being called in CharacterController2D.cs*/
     //                }
     //                if (!isGrounded && canDoubleJump) { /*Double jump*/
     //                     applyForce(0f, 400f);
     //                     canDoubleJump = false;
     //                }
     //                if (isGrounded) /*Resets double jump when you touch the ground*/
     //                     canDoubleJump = true;
     //                if (jump) /*Not sure why I included this...*/
     //                     return;
     //      }
     // }

     // void checkPhaseController() {
     //      isGrounded = characterController2D.getGrounded();
     //      isFacingRight = characterController2D.getFacingRight();
     //      float rt_axis = Input.GetAxis("XboxRightTrigger");

     //      if (rt_axis == 1f && isFacingRight && !isGrounded && canPhase) {
     //           applyForce(2000f, 0f);
     //           canPhase = false;
     //      }
     //      else if (rt_axis == 1f && !isFacingRight && !isGrounded && canPhase) {
     //           applyForce(-2000f, 0f);
     //           canPhase = false;
     //      }    
     //      if (isGrounded)
     //           canPhase = true;
     // }

     // void checkWallJumpController() {
     //      bool xbox_ls = Input.GetButton("XboxLS");
     //      bool xbox_a = Input.GetButtonDown("XboxA");
     //      RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, wallKickDistance);

     //      if (xbox_a && !characterController2D.getGrounded() && 
     //           hit.collider != null && characterController2D.getFacingRight()) {
     //                canDoubleJump = true;
     //                canPhase = true;
     //                applyForce(-1000f, 500f);
     //      }
     //      else if (xbox_a && !characterController2D.getGrounded() && 
     //           hit.collider != null && !characterController2D.getFacingRight()) {
     //                canDoubleJump = true;
     //                canPhase = true;
     //                applyForce(1000f, 500f);
     //      }
     // }
}
