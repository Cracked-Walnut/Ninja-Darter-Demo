using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {
    
    private float timer;
    private bool isTimerRunning;
    private Quaternion shootRight, shootLeft;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePointLeft, firePointRight;
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
        // if (isTimerRunning) {
            // timer -= Time.deltaTime;
            // if (timer == 0.0f) {
                Instantiate(bulletPrefab, firePointLeft.position, shootRight);
                Instantiate(bulletPrefab, firePointRight.position, shootLeft);
                // timer = 1.5f;
            // }

            /*Stop the timer if the enemy is dead. Might not be necessary since we'll be destroying the game object when the enemy dies anyway*/
            // if (!enemy.getIsAlive())
            //     isTimerRunning = false;
        // }
    }
}
