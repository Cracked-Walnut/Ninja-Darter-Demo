using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
Upgrades: 
+10% bullet damage [X]
+5% bullet damage [X]
+10 max bullets [X]
+5 max bullets [X]
*/
public class Offense : MonoBehaviour {

    private Bullet bullet;
    private Weapon weapon;
    private const float FIRST_MULTIPLIER = 1.1f /* 10% */, 
        SECOND_MULTIPLIER = 1.15f; /* 5% after you get the first upgrade, for a total of 15% */
    
    private const int TEN_BULLETS_UPGRADE = 10, 
        FIVE_BULLETS_UPGRADE = 5;
    
    
    void Awake () {
        bullet = FindObjectOfType<Bullet>();
        weapon = FindObjectOfType<Weapon>();
    }

    public void upgradeBulletDamage(float multiplier) {
        float bulletDamageUpgrade = bullet.getDamage() * multiplier;
        bullet.setDamage(bulletDamageUpgrade);
    }

    public void increaseMaxBullets(int bulletsToAdd) {
        int maxBulletUpgrade = weapon.getMaxAmmo() + bulletsToAdd;
        weapon.setMaxAmmo(maxBulletUpgrade);
    }

}
