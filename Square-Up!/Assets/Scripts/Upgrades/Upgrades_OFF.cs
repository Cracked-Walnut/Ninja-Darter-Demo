using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*This class will be responsible for randomly assigning an uprgade to this
upgrade object*/
public class Upgrades_OFF : MonoBehaviour {

    private const int UPGRADE_NUMS = 3;
    private int upgradePick = 1;
    private const float FIRST_MULTIPLIER = 1.1f /* 10% */, 
        SECOND_MULTIPLIER = 1.15f; /* 5% after you get the first upgrade, for a total of 15% */
    private const int TEN_BULLETS_UPGRADE = 30,
        FIVE_BULLETS_UPGRADE = 35;
    
    private Offense offense;

    void Start() {
        upgradePick = Random.Range(1, UPGRADE_NUMS);
    }
    
    void Awake() {
        offense = FindObjectOfType<Offense>();

    }

    void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Player")
            assignUpgrade();
    }

    void assignUpgrade() {
        if (upgradePick == 1) {
            Debug.Log("Bullet Damage Upgrade!");
            offense.upgradeBulletDamage(FIRST_MULTIPLIER);

            /*Write a check to see if bullet damage is 1.1, if so, assign 1.15*/
        } 
        else if (upgradePick == 2) {
            Debug.Log("Bullet Capacity Upgrade!");
            offense.increaseMaxBullets(TEN_BULLETS_UPGRADE);
            /*Write a check to see if bullet damage is 30, if so, assign 35*/
        }
    }
}
