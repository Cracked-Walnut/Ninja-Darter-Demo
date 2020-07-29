using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

    [SerializeField] private int numberOfBarrels;
    
    [Header("Rate of Fire & Barrel Angles")]
    [SerializeField] private float rateOfFire; 
    [SerializeField] private float leftBarrelAngle, rightBarrelAngle, topBarrelAngle, bottomBarrelAngle;
    
    [Header("Bullet Type, Fire Points & Enemy Vision")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePointLeft, firePointRight, firePointTop, firePointBottom;

    private bool isTimerRunning;
    private Quaternion rightBarrel, leftBarrel, topBarrel, bottomBarrel;

    void Awake() {
        initBarrels();
    }

    void Start() {
        rateOfFire = 1.0f;
        isTimerRunning = true;
    }

    void Update() {
        shoot(numberOfBarrels);
    }

    void initBarrels() {
        switch(numberOfBarrels) {
            case 1:
                rightBarrel = Quaternion.Euler(0, 0, rightBarrelAngle);
                firePointLeft = null; firePointTop = null; firePointBottom = null;
                break;
            
            case 2:
                rightBarrel = Quaternion.Euler(0, 0, rightBarrelAngle);
                leftBarrel = Quaternion.Euler(0, 0, leftBarrelAngle);
                firePointTop = null; firePointBottom = null;
                break;
            
            case 3:
                rightBarrel = Quaternion.Euler(0, 0, rightBarrelAngle);
                leftBarrel = Quaternion.Euler(0, 0, leftBarrelAngle);
                topBarrel = Quaternion.Euler(0, 0, topBarrelAngle);
                firePointBottom = null;
                break;

            case 4:
                rightBarrel = Quaternion.Euler(0, 0, rightBarrelAngle);
                leftBarrel = Quaternion.Euler(0, 0, leftBarrelAngle);
                topBarrel = Quaternion.Euler(0, 0, topBarrelAngle);
                bottomBarrel = Quaternion.Euler(0, 0, bottomBarrelAngle);
                break;
        }
    }

    public void shoot(int numberOfBarrels) {
    
        rateOfFire -= Time.deltaTime;

        if (rateOfFire <= 0.0f) {
            switch(numberOfBarrels) {
                case 1:
                    Instantiate(bulletPrefab, firePointRight.position, rightBarrel);
                    rateOfFire = 1.0f;
                    break;

                case 2:
                    Instantiate(bulletPrefab, firePointRight.position, rightBarrel);
                    Instantiate(bulletPrefab, firePointLeft.position, leftBarrel);
                    rateOfFire = 1.0f;
                    break;

                case 3:
                    Instantiate(bulletPrefab, firePointRight.position, rightBarrel);
                    Instantiate(bulletPrefab, firePointLeft.position, leftBarrel);
                    Instantiate(bulletPrefab, firePointTop.position, topBarrel);
                    rateOfFire = 1.0f;
                    break;

                case 4:
                    Instantiate(bulletPrefab, firePointRight.position, rightBarrel);
                    Instantiate(bulletPrefab, firePointLeft.position, leftBarrel);
                    Instantiate(bulletPrefab, firePointTop.position, topBarrel);
                    Instantiate(bulletPrefab, firePointBottom.position, bottomBarrel);
                    rateOfFire = 1.0f;
                    break;
            }
        }
    }

    private void shootWithVision() {
        // RaycastHit2D enemyInfoRight = Physics2D.Raycast(enemyVisionRight.position, Vector2.right, 10.0f);
        // RaycastHit2D enemyInfoLeft = Physics2D.Raycast(enemyVisionLeft.position, Vector2.left, 10.0f);
        
        // if (enemyInfoRight.collider == true && enemyInfoLeft.collider == true) {
        //     Instantiate(bulletPrefab, firePointLeft.position, rightBarrel);
        //     Instantiate(bulletPrefab, firePointRight.position, leftBarrel);
        // }

        shoot(numberOfBarrels);
    }
}
