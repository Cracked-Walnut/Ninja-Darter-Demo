using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : MonoBehaviour {

    public Transform firePoint;
    public GameObject bulletPrefab;
    public Text currentAmmoDisplay, maxAmmoDisplay;
    public PlayerInput playerInput;
    private int currentAmmo = 10, maxAmmo = 20;
    private bool canFire = true;

    private void Awake() {
        playerInput = GetComponent<PlayerInput>();
    }
    
    void Update() {
        currentAmmoDisplay.text = currentAmmo.ToString();
        maxAmmoDisplay.text = maxAmmo.ToString();
        checkWeaponFire();
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

    void checkWeaponFire() {

        if (!playerInput.getCanShoot())
            return;
        else {
            if (Input.GetButtonDown("Fire1")) { 
                if (canFire && currentAmmo > 0) { 
                    currentAmmo -= 1;
                    Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

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
}//end of class


/*
Sources:
1) J., JGN Games, 'How To Make A HUD in Unity (4. Ammo Display)', 2018. [Online]. Available: https://www.youtube.com/watch?v=suEV612c7XU [Accessed: 22-Mar-2020].
*/