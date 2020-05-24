using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    public float speed;
    public float groundDistance, wallDistance;
    private bool movingRight_ground = true, movingRight_wall = true;
    public Transform groundDetection, wallDetection;

    void Update() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        checkGround();
    }

    void checkGround() {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);

        if (groundInfo.collider == false) {
            if (movingRight_ground) {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight_ground = false;
                
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight_ground = true;
            }
        }
    }
}

/*
Sources:
1) B., Blackthornprod, '2D PLATFORMER PATROL AI WITH UNITY AND C# - EASY TUTORIAL', 2018. [Online]. Available: https://www.youtube.com/watch?v=aRxuKoJH9Y0 [Accessed: 19-Apr-2020].
*/
