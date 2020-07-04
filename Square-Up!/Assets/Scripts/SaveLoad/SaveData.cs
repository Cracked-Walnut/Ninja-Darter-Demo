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

    private void SaveAllPrimitiveNonUpgrades() {

        SaveHP();
        SaveAmmo();
        SavePulseJump();
        SaveStageTime();
        SaveTotalCoins();
    }

    void SaveHP () { PlayerPrefs.SetInt("hp", player.getCurrentHealth()); }
    void SaveAmmo() { PlayerPrefs.SetInt("ammo_count", weapon.getBulletCount()); }
    void SavePulseJump() { PlayerPrefs.SetFloat("pulse_jump_seconds", playerInput.getPulseJumpSeconds()); }
    void SaveStageTime() { PlayerPrefs.SetFloat("level_timer", timer.getTotalTime()); }
    void SaveTotalCoins() { PlayerPrefs.SetInt("coins_collected", coinCollision.getCoinCount()); }

/*-----------------------------------------------------------------------------------------------------------------------------------*/

    private void LoadAllPrimitiveNonUpgrades() {
        
        LoadHP();
        LoadAmmo();
        LoadPulseJump();
        LoadStageTime();
        LoadTotalCoins();
    }

    void LoadHP() { PlayerPrefs.GetInt("hp"); }
    void LoadAmmo() { PlayerPrefs.GetInt("ammo_count"); }
    void LoadPulseJump() { PlayerPrefs.GetFloat("pulse_jump_seconds"); }
    void LoadStageTime() { PlayerPrefs.GetFloat("level_timer"); }
    void LoadTotalCoins() { PlayerPrefs.GetInt("coins_collected"); }

/*-----------------------------------------------------------------------------------------------------------------------------------*/

    private void SaveAllUpgrades() { 

        SaveMaxHP();
        SaveMovementSpeed();
        SaveJumpHeight();
        SaveWallJumpXDistance();
        SaveWallJumpYDistance();
        SaveGroundPoundForce();
        SavePhaseDistance();
    }

    void SaveMaxHP() { PlayerPrefs.GetInt("max_hp", player.getMaxHealth()); }
    void SaveMovementSpeed () { PlayerPrefs.GetFloat("run_speed", playerInput.getRunSpeed()); }
    void SaveJumpHeight () { PlayerPrefs.GetFloat("jump_height", characterController2D.getJumpForce()); }
    void SaveWallJumpXDistance () { PlayerPrefs.GetFloat("wall_jump_x", playerInput.getWallJumpX()); }
    void SaveWallJumpYDistance () { PlayerPrefs.GetFloat("wall_jump_y", playerInput.getWallJumpY()); }
    void SaveGroundPoundForce () {}
    void SavePhaseDistance () { PlayerPrefs.GetFloat("phase_speed", playerInput.getPhaseSpeed()); }

/*-----------------------------------------------------------------------------------------------------------------------------------*/

}
