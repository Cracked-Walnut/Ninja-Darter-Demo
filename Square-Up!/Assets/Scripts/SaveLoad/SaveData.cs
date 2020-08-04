using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {

    private PlayerData playerData;
    private Player player; // HP, int / coins collected, int
    // private Weapon weapon;// Ammo Count, int
    private PlayerInput playerInput;// Pulse Jump, float
    private Timer timer;// Timer, float
    
    // Upgrades, float and int
    // private Defense defense;
    // private Mobility mobility;
    // private Offense offense;
    // private CharacterController2D characterController2D;

    void Start() {
        SaveHP();
    }

    void Awake() {
        playerData = new PlayerData();
        player = FindObjectOfType<Player>();
        // weapon = new Weapon();
        playerInput = new PlayerInput();
        timer = new Timer();
        // defense = new Defense();
        // mobility = new Mobility();
        // offense = new Offense();
        // characterController2D = new CharacterController2D();
    }

/*-----------------------------------------------------------------------------------------------------------------------------------*/

    void SaveHP () {
        playerData.setHealth(player.getCurrentHealth());
        playerData.setPulseJump(playerInput.getPulseJumpSeconds());
        playerData.setTimer(timer.getTotalTime());
        playerData.setTotalCoins(player.getCoinCount());

        string jsonSave = JsonUtility.ToJson(playerData);
        Debug.Log(jsonSave);
        
        try {

            File.WriteAllText("D:\\Github-Repos\\Square-Up\\Square-Up!\\Assets\\" + "DummySaveFile", jsonSave);
            Debug.Log("saved to file");
        } 
        catch (IOException e) {
            Debug.Log("Error occured while saving!");
        }

        string loadJson = File.ReadAllText("D:\\Github-Repos\\Square-Up\\Square-Up!\\Assets\\" + "DummySaveFile");
        PlayerData loadedPlayerData = JsonUtility.FromJson<PlayerData>(jsonSave);
        Debug.Log("Health: " + loadedPlayerData.getHealth());
        Debug.Log("Pulse Jump: " + loadedPlayerData.getPulseJump());
        Debug.Log("Timer: " + loadedPlayerData.getTimer());
        Debug.Log("Total Coins: " + loadedPlayerData.getTotalCoins());

    }



    private class PlayerData {

        // for every variable you wanna save, make a new variable and store your other variable in it
        [SerializeField] private int health;
        [SerializeField] private float pulseJump;
        [SerializeField] private float timer;
        [SerializeField] private int totalCoins;


        public int getHealth() { return health; }
        public void setHealth(int health) { this.health = health; }

        public float getPulseJump() { return pulseJump; }
        public void setPulseJump(float pulseJump) { this.pulseJump = pulseJump; }

        public float getTimer() { return timer; }
        public void setTimer(float timer) { this.timer = timer; }

        public int getTotalCoins() { return totalCoins; }
        public void setTotalCoins(int totalCoins) { this.totalCoins = totalCoins; }

    } // end of inner class
/*-----------------------------------------------------------------------------------------------------------------------------------*/
/*-----------------------------------------------------------------------------------------------------------------------------------*/
/*-----------------------------------------------------------------------------------------------------------------------------------*/

} // end of outer class

/*
Sources:
1) C.D., Code Monkey, 'What is JSON? (Unity Tutorial for Beginners)', 2018. [Online]. Available: https://www.youtube.com/watch?v=4oRVMCRCvN0 [Accessed: 22-Jul-2020].
*/
