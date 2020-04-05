using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Upgrades: 
+10% movement speed [X]
+6% movement speed [X]
+10% jump height
+10% wall jump distance [X]
Ground pound (hold down key)
Gound pound with more force [X]
+10% phase distance [X]
+5% phase distance [X]
Spawn a temporary platform beneath you (2s)
+2s to temporary platform
stick to walls
    - Use boolean, if on, applyforce(0f, 0f) in PlayerInput.cs for wall cling
*/

public class Mobility : MonoBehaviour {

    private CharacterController2D characterController2D;
    private PlayerInput playerInput;
    private const float FIRST_MOVEMENT_SPEED_UPGRADE = 1.1f /*10%*/, 
        SECOND_MOVEMENT_SPEED_UPGRADE = 1.16f; /* 6% after you get the first upgrade, for a total of 16% */

    private const float FIRST_PHASE_UPGRADE = 1.1f, SECOND_PHASE_UPGRADE = 1.15f;

    void Awake() {
        characterController2D = FindObjectOfType<CharacterController2D>();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    public void increaseMovementSpeed(float movementSpeedMultiplier) {
        float newMovementSpeed = playerInput.getRunSpeed() * movementSpeedMultiplier;
        playerInput.setRunSpeed(newMovementSpeed);
    }

    public void increaseJumpDistance(float jumpDistanceMultiplier) {
        /*Being worked on*/
    }

    public void increaseWallJumpDistance(float wallJumpDistanceMultiplier) {

    }

    public void increaseGroundPoundForce(float groundPoundForce) {
        /*Grab the rigidbody2D from PlayerInput and manipulate that
        or grab checkGroundPound() and work with that
        or make a separate setter function to change gravity*/
    }

    public void increaseRightPhaseDistance(float phaseDistance) {
        float newPhaseDistance = playerInput.getPhaseSpeed() * phaseDistance;
        playerInput.setPhaseSpeed(newPhaseDistance);
    }
    
    public void increaseLeftPhaseDistance(float phaseDistance) {
        float newPhaseDistance = playerInput.getNegativePhaseSpeed() * phaseDistance;
        playerInput.setPhaseSpeedNegative(newPhaseDistance);
    }

}
