using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

    [Header("Rate of Fire & Barrel Angles")]
    [SerializeField] private float rateOfFire; 
    [SerializeField] private float leftBarrelAngle, rightBarrelAngle;

    private bool isTimerRunning;
    private Quaternion rightBarrel, leftBarrel;
    private Enemy enemy;

    [Header("Bullet Type, Fire Points & Enemy Vision")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePointLeft, firePointRight;
    [SerializeField] private Transform enemyVisionRight, enemyVisionLeft;


    void Awake() {
        enemy = FindObjectOfType<Enemy>();
        rightBarrel = Quaternion.Euler(0, 0, rightBarrelAngle);
        leftBarrel = Quaternion.Euler(0, 0, leftBarrelAngle);
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
            Instantiate(bulletPrefab, firePointLeft.position, leftBarrel);
            Instantiate(bulletPrefab, firePointRight.position, rightBarrel);
            rateOfFire = 1.0f;
        }
    
    }

    private void shootWithVision() {
        // RaycastHit2D enemyInfoRight = Physics2D.Raycast(enemyVisionRight.position, Vector2.right, 10.0f);
        // RaycastHit2D enemyInfoLeft = Physics2D.Raycast(enemyVisionLeft.position, Vector2.left, 10.0f);
        
        // if (enemyInfoRight.collider == true && enemyInfoLeft.collider == true) {
        //     Instantiate(bulletPrefab, firePointLeft.position, rightBarrel);
        //     Instantiate(bulletPrefab, firePointRight.position, leftBarrel);
        // }

        shoot();
    }
}
