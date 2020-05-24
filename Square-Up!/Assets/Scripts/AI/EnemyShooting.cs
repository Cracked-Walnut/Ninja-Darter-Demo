using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {
    
    private float timer;
    private bool isTimerRunning, shootAgain = true;
    private Quaternion shootRight, shootLeft;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePointLeft, firePointRight;
    [SerializeField] private Transform enemyVisionRight, enemyVisionLeft;
    private Enemy enemy;


    void Awake() {
        enemy = FindObjectOfType<Enemy>();
        shootRight = Quaternion.Euler(0, 0, 0);
        shootLeft = Quaternion.Euler(0, 0, -180);
    }
    
    void Start() {
        timer = 1.5f;
        isTimerRunning = true;
    }

    void Update() {
        shoot();
    }

    private void shoot() {
        RaycastHit2D enemyInfoRight = Physics2D.Raycast(enemyVisionRight.position, Vector2.right, 10.0f);
        RaycastHit2D enemyInfoLeft = Physics2D.Raycast(enemyVisionLeft.position, Vector2.left, 10.0f);
            
        if (enemyInfoRight.collider == true && enemyInfoLeft.collider == true && shootAgain) {
            Instantiate(bulletPrefab, firePointLeft.position, shootRight);
            Instantiate(bulletPrefab, firePointRight.position, shootLeft);
            shootAgain = false;
        }

        if (enemyInfoRight.collider == false && enemyInfoLeft.collider == false)
            shootAgain = true;
    }

    // private void shootLeft_() {
    //     RaycastHit2D enemyInfoLeft = Physics2D.Raycast(enemyVisionLeft.position, Vector2.left, 10.0f);
            
    //     if (enemyInfoLeft.collider == true && shootAgainLeft) {
    //         Instantiate(bulletPrefab, firePointRight.position, shootLeft);
    //         shootAgainLeft = false;
    //     }

    //     if (enemyInfoLeft.collider == false)
    //         shootAgainLeft = true;
    // }
}
