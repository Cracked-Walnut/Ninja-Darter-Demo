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
