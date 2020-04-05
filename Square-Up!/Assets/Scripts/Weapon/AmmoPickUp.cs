using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoPickUp : MonoBehaviour {

    private Weapon weapon;
    [SerializeField] private int bulletsToAdd = 3;

    void Awake() {
        weapon = FindObjectOfType<Weapon>();
    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Player") {
            Debug.Log(collider.gameObject.name);
            
            int bulletResult = bulletsToAdd + weapon.getBulletCount();
            if (bulletResult > weapon.getMaxAmmo())
                weapon.setBulletCount(weapon.getMaxAmmo());
            else
                weapon.setBulletCount(bulletResult);
        }
    }
}
