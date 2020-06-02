using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour {

    public float speed;
    public float groundDistance;
    private float sightDistance = 5.0f;
    private bool movingRight = true;
    public Transform groundDetection;
    public Transform visionRight, visionLeft;
    private EnemyWeapon enemyWeapon;

    void Update() {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        checkGround();
    }

    void Awake() {
        enemyWeapon = FindObjectOfType<EnemyWeapon>();
    }

    void checkGround() {
        RaycastHit2D groundInfo = Physics2D.Raycast(groundDetection.position, Vector2.down, groundDistance);

        if (groundInfo.collider == false) {
            if (movingRight) {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                
            } else {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
            }
        }
    }

    void checkSight() {
        RaycastHit2D visionRightRay = Physics2D.Raycast(visionRight.position, Vector2.right, sightDistance);
        RaycastHit2D visionLeftRay = Physics2D.Raycast(visionLeft.position, Vector2.left, sightDistance);

        if (visionRight.GetComponent<Collider>() == true) {
            enemyWeapon.shoot(2);
        }
    }
}

/*
Sources:
1) B., Blackthornprod, '2D PLATFORMER PATROL AI WITH UNITY AND C# - EASY TUTORIAL', 2018. [Online]. Available: https://www.youtube.com/watch?v=aRxuKoJH9Y0 [Accessed: 19-Apr-2020].
*/
