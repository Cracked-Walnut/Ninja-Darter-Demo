using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    private PlayerInput playerInput;
    private CharacterController2D characterController2D;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Text currentAmmoDisplay, maxAmmoDisplay;
    [SerializeField] private int currentAmmo = 10, maxAmmo = 20;
    [SerializeField] private bool canFire = true;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
        characterController2D = GetComponent<CharacterController2D>();
    }
    
    void Update() {
        currentAmmoDisplay.text = currentAmmo.ToString();
        maxAmmoDisplay.text = maxAmmo.ToString();
        fireSingle();
        // automaticFire();
        // StartCoroutine(checkFire());
    }

    void fireSingle() {
        if (!playerInput.getCanShoot())
            return;
        else {

            if (Input.GetButtonDown("Fire1")) {
                if (canFire && currentAmmo > 0) {
                    
                    currentAmmo -= 1;
                    if (characterController2D.getFacingRight()) {
                        // Invoke("automaticFire", 0.3f);
                        accuracy(-10.0f, 10.0f);
                    }
                    else {
                        // Invoke("automaticFireBackwards", 0.3f);
                        accuracy(-170.0f, -190.0f);
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

    // IEnumerator checkFire() {
    //     if (!playerInput.getCanShoot())
    //         yield return null;
    //     else {

    //         if (Input.GetButton("Fire1")) {
    //             if (canFire && currentAmmo > 0) {
                    
    //                 currentAmmo -= 1;
    //                 if (characterController2D.getFacingRight()) {
    //                     // Invoke("automaticFire", 0.3f);
    //                     automaticFire();
    //                     yield return new WaitForSeconds(10);
    //                 }
    //                 else {
    //                     // Invoke("automaticFireBackwards", 0.3f);
    //                     automaticFireBackwards();
    //                     yield return new WaitForSeconds(1);
    //                 }

    //                 if (currentAmmo <= 0) {
    //                     if (currentAmmo < 0)
    //                         currentAmmo = 0;
    //                     canFire = false;
    //                 }
    //             }
    //         }
    //         if (currentAmmo > 0)
    //             canFire = true;
    //         }
    //         // if (Input.GetButtonDown("Fire1")) {
    //         //     if (canFire && currentAmmo > 0) {
                    
    //         //         currentAmmo -= 1;
    //         //         if (characterController2D.getFacingRight())
    //         //             generateAccuracy(float lowAngle, float highAngle);
    //         //         else
    //         //             singleFireBackwards();
                    
    //         //         if (currentAmmo <= 0) {
    //         //             if (currentAmmo < 0)
    //         //                 currentAmmo = 0;
    //         //             canFire = false;
    //         //         }
    //         //     }
    //         // }
    //         // if (currentAmmo > 0)
    //         //     canFire = true; 
    //     }
    

    void generateAccuracy(float lowAngle, float highAngle) {
        accuracy(lowAngle, highAngle);
    }

    public void accuracy(float highAngle, float lowAngle) {
        float randomAccuracyNum = Random.Range(highAngle, lowAngle);
        Quaternion accuracy = Quaternion.Euler(0, 0, randomAccuracyNum);
        Instantiate(bulletPrefab, firePoint.position, accuracy);
    }

    public int getBulletCount() {
        return currentAmmo;
    }

    public void setBulletCount(int currentAmmo) {
        this.currentAmmo = currentAmmo;
    }

    public int getMaxAmmo() {
        return maxAmmo;
    }

    public void setMaxAmmo(int maxAmmo) {
        this.maxAmmo = maxAmmo;
    }
}//end of class


/*
Sources:
1) J., JGN Games, 'How To Make A HUD in Unity (4. Ammo Display)', 2018. [Online]. Available: https://www.youtube.com/watch?v=suEV612c7XU [Accessed: 22-Mar-2020].
*/