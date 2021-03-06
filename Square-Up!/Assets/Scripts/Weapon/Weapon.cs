﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    private PlayerInput playerInput;
    private CharacterController2D characterController2D;
    private CameraShake cameraShake;
    private float timer;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Text currentAmmoDisplay, maxAmmoDisplay, pulseJumpDisplay;
    [SerializeField] private int currentAmmo = 10, maxAmmo = 20;
    [SerializeField] private bool canFire = true;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        characterController2D = GetComponent<CharacterController2D>();
    }
    
    void Update() {
        currentAmmoDisplay.text = currentAmmo.ToString();
        maxAmmoDisplay.text = maxAmmo.ToString();
        pulseJumpDisplay.text = playerInput.getPulseJumpTimer().ToString("0.00");
        fireSingle();
    }

    void Start() {
        timer = 0.2f;
    }

    void fireSingle() {
        if (!playerInput.getCanShoot())
            return;
        else {
            if (Input.GetButtonDown("Fire1")) {
                if (canFire && currentAmmo > 0) {
                    currentAmmo -= 1;
                    timer -= Time.deltaTime;
                    
                    if (characterController2D.getFacingRight()) {
                        fireBullet(-10.0f, 10.0f);
                    }
                    else {
                        fireBullet(-170.0f, -190.0f);
                    }
                    if (currentAmmo <= 0) {
                        if (currentAmmo < 0)
                            currentAmmo = 0;
                        canFire = false;
                    }
                }
            }
                if (currentAmmo > 0)
                    canFire = true;
            }
    }
 
    public void fireBullet(float highAngle, float lowAngle) {
        float randomAccuracyNum = Random.Range(highAngle, lowAngle);
        Quaternion accuracy = Quaternion.Euler(0, 0, randomAccuracyNum);
        Instantiate(bulletPrefab, firePoint.position, accuracy);
    }

    public int getBulletCount() { return currentAmmo; }
    public void setBulletCount(int currentAmmo) { this.currentAmmo = currentAmmo; }
    public int getMaxAmmo() { return maxAmmo; }
    public void setMaxAmmo(int maxAmmo) { this.maxAmmo = maxAmmo; }
    public Transform getFirePoint() { return firePoint; }
}//end of class


/*
Sources:
1) J., JGN Games, 'How To Make A HUD in Unity (4. Ammo Display)', 2018. [Online]. Available: https://www.youtube.com/watch?v=suEV612c7XU [Accessed: 22-Mar-2020].
2) B., Brackeys, '2D Shooting in Unity (Tutorial)', 2018. [Online]. Available: https://www.youtube.com/watch?v=wkKsl1Mfp5M [Accessed: 21-Mar-2020].
*/