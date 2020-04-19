﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    public float speed;
    public float groundDistance, wallDistance;

    private bool movingRight = true;

    public Transform groundDetection, wallDetection;
    private RaycastHit2D groundInfo, wallInfo;

    void Awake() {
        // groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
        // wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, wallDistance);
    }

    void Update() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
        checkGround();
        checkWall();
    }

    void checkGround() {
        groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);
        if (groundInfo.collider == null) {
            if (movingRight) {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    void checkWall() {
        wallInfo = Physics2D.Raycast(wallDetection.position, Vector2.right, wallDistance);
        if (wallInfo.collider != null) {
            if (movingRight) {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }
}

/*
Sources:
1) B., Blackthornprod, '2D PLATFORMER PATROL AI WITH UNITY AND C# - EASY TUTORIAL', 2018. [Online]. Available: https://www.youtube.com/watch?v=aRxuKoJH9Y0 [Accessed: 19-Apr-2020].
*/
