using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Upgrades: 
+2 max HP
+1 max HP
Temporary invincibility when hit
*/

public class Defense : MonoBehaviour {

    private HealthBar healthBar;
    private const int FIRST_HP_UPGRADE = 2, SECOND_HP_UPGRADE = 1;

    void Awake() {
        healthBar = FindObjectOfType<HealthBar>();
    }

    public int getFirstHPUpgrade() {
        return FIRST_HP_UPGRADE;
    }

    public int getSecondHPUpgrade() {
        return SECOND_HP_UPGRADE;
    }

    public void upgradeMaxHealth(int health) {
        healthBar.setMaxHealth(health);
    }
}
