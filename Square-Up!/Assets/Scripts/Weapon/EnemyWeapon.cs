using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

    [Header("Rate of Fire & Barrel Angles")]
    [SerializeField] private float rateOfFire; 
    [SerializeField] private float leftBarrelAngle, rightBarrelAngle;

    private bool isTimerRunning;
    private Quaternion shootRight, shootLeft;
    private Enemy enemy;

    [Header("Bullet Type, Fire Points & Enemy Vision")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePointLeft, firePointRight;
    [SerializeField] private Transform enemyVisionRight, enemyVisionLeft;


    void Awake() {
        enemy = FindObjectOfType<Enemy>();
        shootRight = Quaternion.Euler(0, 0, rightBarrelAngle);
        shootLeft = Quaternion.Euler(0, 0, leftBarrelAngle);
    }

    void Start() {
        rateOfFire = 1.0f;
        isTimerRunning = true;
    }

    void Update() {
        shoot();
    }

    private void shoot() {
    
        rateOfFire -= Time.deltaTime;

        if (rateOfFire <= 0.0f) {
            Instantiate(bulletPrefab, firePointLeft.position, shootLeft);
            Instantiate(bulletPrefab, firePointRight.position, shootRight);
            rateOfFire = 1.0f;
        }
    
    }

    private void shootWithVision() {
        // RaycastHit2D enemyInfoRight = Physics2D.Raycast(enemyVisionRight.position, Vector2.right, 10.0f);
        // RaycastHit2D enemyInfoLeft = Physics2D.Raycast(enemyVisionLeft.position, Vector2.left, 10.0f);
        
        // if (enemyInfoRight.collider == true && enemyInfoLeft.collider == true) {
        //     Instantiate(bulletPrefab, firePointLeft.position, shootRight);
        //     Instantiate(bulletPrefab, firePointRight.position, shootLeft);
        // }

        shoot();
    }
}
