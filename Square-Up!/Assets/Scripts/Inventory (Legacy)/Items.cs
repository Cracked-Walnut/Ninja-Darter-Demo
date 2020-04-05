using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script will hold characteristics of an item*/

public class Items : MonoBehaviour {

    private int healthPotionAmount, antiPoisonPotionAmount, smokeBombAmount, flammableDousingPowderAmount, poisonousDousingPowderAmount, shuriken, coins;

    public int getHealthPotionAmount() {
        return healthPotionAmount;
    }

    public void setHealthPotionAmount(int healthPotionAmount) {
        this.healthPotionAmount = healthPotionAmount;
    }


}//end of class