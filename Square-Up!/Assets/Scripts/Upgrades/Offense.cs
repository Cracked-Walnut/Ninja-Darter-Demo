using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Upgrades: 
+10% bullet damage [X]
+5% bullet damage [X]
+10 max bullets [X]
+5 max bullets [X]
+5% bullet accuracy [X]
+10% bullet accuracy [X]
Automatic fire
    Fire rate
Grenade explodes on contact
Shots pierce their target
Two barrels to fire gun
Shoot in front as well as behind
*/
public class Offense : MonoBehaviour {

    private Bullet bullet;
    private Weapon weapon;
    private bool accuracyBuff = false;
    private bool automaticFire = false;
    
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

    public bool getAutomaticFire() {
        return automaticFire;
    }

    public void setAutomaticFire(bool automaticFire) {
        this.automaticFire = automaticFire;
    }

}
