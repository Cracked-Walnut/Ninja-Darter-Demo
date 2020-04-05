using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script will hold characteristics of an item*/

public class Item : Items {

    public string itemName = " ";
    // private int quantity;
    private bool isPickedUp = false;
    private string[] itemCategories;
    private int[] itemQuantity;
    private int itemNumLimit = 10; /*Can be expanded, therefore not const*/
    // private int healthPotionAmount, antiPoisonPotionAmount, smokeBombAmount, flammableDousingPowderAmount, poisonousDousingPowderAmount, shuriken, coins;
    // public int grabHealthPotionAmount = FindObjectOfType<Items>().getHealthPotionAmount();

    void Start() { /* Debug.Log(healthPotionAmount); */ }
    
    void Awake() { 

        /*We're going to use this when making the UI*/
        itemCategories = new string[3] {
            "Consumables",
            "Weapons",
            "Misc"
        };

        // itemQuantity = new int[8] {
        //     healthPotionAmount,
        //     0,
        //     0,
        //     0,
        //     0,
        //     0,
        //     0,
        //     0
        // };
    }

    void OnTriggerEnter2D(Collider2D collider) { 
        /*Make a switch case*/
        /*Checking to see if the name assigned in the editor is the same as one of the names in the array*/
        // if (itemName == itemNames[0]) pickUpItem(itemName);
        if(itemName == "Health Potion")
            pickUpItem(itemName);
        
        
     }

    void pickUpItem(string itemName) { 
        /*All variables in here should be as generic as possible
            Only use specific variables up top when you're calling the method*/
        
        if (!isPickedUp) { /*Always outputs 1 for itemAmount. I think it's because I'm using the same
            script (Item.cs) to check two identical game objects*/
            Destroy(gameObject);
            // itemNum += 1;
            // itemQuantity[0] += 1;
            // itemAmount++;
            // FindObjectOfType<Items>().setHealthPotionAmount(itemNum);
            int increase = getHealthPotionAmount() + 1;
            setHealthPotionAmount(getHealthPotionAmount() + 1);
            isPickedUp = true;
            Debug.Log("Picked up " + itemName + ". \nTotal in inventory: " + increase);
        } 
        if (isPickedUp) return;
    }//end of pickUpItem
}//end of class