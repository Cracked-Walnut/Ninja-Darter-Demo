using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*This script will hold data to be saved about the player. I will be making other scripts for other objects
that need saving*/

[System.Serializable]
public class PlayerData {

    // public int level;
    public int health;
    public float[] position;


    public PlayerData (Player player) {
        health = player.currentHealth;

        position = new float[3];
        position[0] = player.transform.position.x;
        position[1] = player.transform.position.y;
        position[2] = player.transform.position.z;
    }

}
