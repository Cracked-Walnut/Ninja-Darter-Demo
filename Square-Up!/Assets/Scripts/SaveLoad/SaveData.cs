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
    private CharacterController2D characterController2D;

    void Awake() {
        player = new Player();
        weapon = new Weapon();
        playerInput = new PlayerInput();
        timer = new Timer();
        coinCollision = new CoinCollision();
        defense = new Defense();
        mobility = new Mobility();
        offense = new Offense();
        characterController2D = new CharacterController2D();
    }

/*-----------------------------------------------------------------------------------------------------------------------------------*/

    public void SaveAllPrimitiveNonUpgrades() {

        try {
        
            SaveHP();
            SaveAmmo();
            SavePulseJump();
            SaveStageTime();
            SaveTotalCoins();
        
        } catch {
            Debug.Log("Save error occured");
        }

        Debug.Log("Saved PlayerPrefs");
    }

    void SaveHP () { PlayerPrefs.SetInt("hp", player.getCurrentHealth()); }
    void SaveAmmo() { PlayerPrefs.SetInt("ammo_count", weapon.getBulletCount()); }
    void SavePulseJump() { PlayerPrefs.SetFloat("pulse_jump_seconds", playerInput.getPulseJumpSeconds()); }
    void SaveStageTime() { PlayerPrefs.SetFloat("level_timer", timer.getTotalTime()); }
    void SaveTotalCoins() { PlayerPrefs.SetInt("coins_collected", coinCollision.getCoinCount()); }

/*-----------------------------------------------------------------------------------------------------------------------------------*/

    public void LoadAllPrimitiveNonUpgrades() {

        try {    

            Debug.Log(LoadHP());
            Debug.Log(LoadAmmo());
            Debug.Log(LoadPulseJump());
            Debug.Log(LoadStageTime());
            Debug.Log(LoadTotalCoins());
        
        } catch {
            Debug.Log("Load error occured");
        }
        Debug.Log("Loaded PlayerPrefs");
    }

    int LoadHP() { return PlayerPrefs.GetInt("hp"); }
    int LoadAmmo() { return PlayerPrefs.GetInt("ammo_count"); }
    float LoadPulseJump() { return PlayerPrefs.GetFloat("pulse_jump_seconds"); }
    float LoadStageTime() { return PlayerPrefs.GetFloat("level_timer"); }
    int LoadTotalCoins() { return PlayerPrefs.GetInt("coins_collected"); }

/*-----------------------------------------------------------------------------------------------------------------------------------*/

    private void SaveAllUpgrades() { 

        SaveMaxHP();
        SaveMovementSpeed();
        SaveJumpHeight();
        SaveWallJumpXDistance();
        SaveWallJumpYDistance();
        // SaveGroundPoundForce();
        SavePhaseDistance();
    }

    void SaveMaxHP() { PlayerPrefs.SetInt("max_hp", player.getMaxHealth()); }
    void SaveMovementSpeed () { PlayerPrefs.SetFloat("run_speed", playerInput.getRunSpeed()); }
    void SaveJumpHeight () { PlayerPrefs.SetFloat("jump_height", characterController2D.getJumpForce()); }
    void SaveWallJumpXDistance () { PlayerPrefs.SetFloat("wall_jump_x", playerInput.getWallJumpX()); }
    void SaveWallJumpYDistance () { PlayerPrefs.SetFloat("wall_jump_y", playerInput.getWallJumpY()); }
    // void SaveGroundPoundForce () {}
    void SavePhaseDistance () { PlayerPrefs.SetFloat("phase_speed", playerInput.getPhaseSpeed()); }

/*-----------------------------------------------------------------------------------------------------------------------------------*/

    private void LoadAllUpgrades() { 
        LoadMaxHP();
        LoadMovementSpeed();
        LoadJumpHeight();
        LoadWallJumpXDistance();
        LoadWallJumpYDistance();
        LoadPhaseDistance();
    }

    void LoadMaxHP() { PlayerPrefs.GetInt("max_hp"); }
    void LoadMovementSpeed() { PlayerPrefs.GetFloat("run_speed"); }
    void LoadJumpHeight() { PlayerPrefs.GetFloat("jump_height"); }
    void LoadWallJumpXDistance() { PlayerPrefs.GetFloat("wall_jump_x"); }
    void LoadWallJumpYDistance() { PlayerPrefs.GetFloat("wall_jump_y"); }
    void LoadPhaseDistance() { PlayerPrefs.GetFloat("phase_speed"); }

/*-----------------------------------------------------------------------------------------------------------------------------------*/

}
