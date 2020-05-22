using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShooting : MonoBehaviour {
    
    private float timer;
    private bool isTimerRunning;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    
    void Start() {
        timer = 1.5f;
        isTimerRunning = true;
    }

    void Update() {
        shoot();
    }

    private void shoot() {
        if (isTimerRunning) {
            timer -= timer.deltaTime;
            if (timer == 0.0f) {
                Instantiate(bulletPrefab, firePoint.position, 0);
                timer = 1.5f;
            }
        }
    }

}
