using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour {

    private PlayerData playerData;
    private Player player; // HP, int
    // private Weapon weapon;// Ammo Count, int
    private PlayerInput playerInput;// Pulse Jump, float
    private Timer timer;// Timer, float
    // private CoinCollision coinCollision;// Coins Collected, int
    
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
        // coinCollision = new CoinCollision();
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

        string hpJson = JsonUtility.ToJson(playerData);
        Debug.Log(hpJson);
     }

    private class PlayerData {

        // for every variable you wanna save, make a new variable and store your other variable in it
        [SerializeField] private int health;
        [SerializeField] private float pulseJump;
        [SerializeField] private float timer;


        public int getHealth() { return health; }
        public void setHealth(int health) { this.health = health; }

        public float getPulseJump() { return pulseJump; }
        public void setPulseJump(float pulseJump) { this.pulseJump = pulseJump; }

        public float getTimer() { return timer; }
        public void setTimer(float timer) { this.timer = timer; }

    } // end of inner class
/*-----------------------------------------------------------------------------------------------------------------------------------*/
/*-----------------------------------------------------------------------------------------------------------------------------------*/
/*-----------------------------------------------------------------------------------------------------------------------------------*/

} // end of outer class
