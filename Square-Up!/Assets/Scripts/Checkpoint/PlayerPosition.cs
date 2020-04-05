using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerPosition : MonoBehaviour {

    private GameMaster gameMaster;
    private InitialGameMaster initialGameMaster;
    private Player player;
    private HealthBar healthBar;
    private bool newCheckPoint;
    private PlayerInput playerInput;

    void Start() {
        player = FindObjectOfType<Player>();
        healthBar = FindObjectOfType<HealthBar>();
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        initialGameMaster = FindObjectOfType<InitialGameMaster>();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    public void applyCheckPoint() {

        transform.position = gameMaster.lastCheckPointPosition;
        newCheckPoint = true;
        applyHealthSettings();
    }

    public void applyInitialPoint() {
        transform.position = initialGameMaster.lastCheckPointPosition;
        applyHealthSettings();
    }

    public void applyHealthSettings() {
        healthBar.setMaxHealth(player.maxHealth);
        player.gainHealth(5);
        player.healthIsFull();
        Debug.Log(player.maxHealth);
    }

    public bool getCheckPointSwitch() {
        return newCheckPoint;
    }

    public void setCheckPointSwitch(bool newCheckPoint) {
        this.newCheckPoint = newCheckPoint;
    }

}
