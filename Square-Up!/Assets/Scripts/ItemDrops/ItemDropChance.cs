using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropChance : MonoBehaviour {
    /*I'll dedicate a class for each item. Use inheritance to have items inherit generic properties*/

    private float randomDropChanceNum = 0.0f; 
    private const float MIN_DROP_CHANCE = 1.0f, MAX_DROP_CHANCE = 15.0f;

    float generateDropChance(float minimumDropChance, float maximumDropChance) {
        randomDropChanceNum = Random.Range(minimumDropChance, maximumDropChance);
        return randomDropChanceNum;
    }

    void checkDropChance() {
        for (float i = 1.0f; i <= 15.0f; i++) {
            if (randomDropChanceNum == i) {
                Debug.Log("Item Drop");
            }
        }
    }

    void executeItemDropChance() {
        generateDropChance(MIN_DROP_CHANCE, MAX_DROP_CHANCE);
        checkDropChance();
    }
}
