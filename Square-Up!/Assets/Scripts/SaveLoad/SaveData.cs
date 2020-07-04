using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData {

    private Player player; // HP, int
    private Weapon weapon;// Ammo Count, int
    private PlayerInput playerInput;// Pulse Jump, float
    private Timer timer;// Timer, float
    private CoinCollision coinCollision;// Coins Collected, int
    
    // Upgrades, float and int
    private Defense defense;
    private Mobility mobility;
    private Offense offense;

    void Awake() {
        player = new Player();
        weapon = new Weapon();
        playerInput = new PlayerInput();
        timer = new Timer();
        coinCollision = new CoinCollision();
    }

    private void SavePrimitiveNonUpgrades() {

        PlayerPrefs.SetInt("hp", player.getCurrentHealth());
        PlayerPrefs.SetInt("ammo_count", weapon.getBulletCount());
        PlayerPrefs.SetFloat("pulse_jump_seconds", playerInput.getPulseJumpSeconds());
        PlayerPrefs.SetFloat("level_timer", timer.getTotalTime());
        PlayerPrefs.SetInt("coins_collected", coinCollision.getCoinCount());
    }

    private void LoadPrimitiveNonUpgrades() {
        PlayerPrefs.GetInt("hp");
        PlayerPrefs.GetInt("ammo_count");
        PlayerPrefs.GetFloat("pulse_jump_seconds");
        PlayerPrefs.GetFloat("level_timer");
        PlayerPrefs.GetInt("coins_collected");
    }

    private void SaveUpgrades() {


    }
    

}
