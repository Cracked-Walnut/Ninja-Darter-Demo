using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour {
   
    private GameMaster gameMaster;
    private PlayerPosition playerPosition;
    void Start() {
        gameMaster = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        playerPosition = FindObjectOfType<PlayerPosition>();
    }

   void OnTriggerEnter2D(Collider2D collider) {
        if (collider.gameObject.name == "Player")
            newCheckPoint();
    }

    void newCheckPoint() {
        gameMaster.lastCheckPointPosition = transform.position;
        playerPosition.setCheckPointSwitch(true);
    }
}


/*
Sources:
1) B., Blackthornprod, 'HOW TO MAKE CHECKPOINTS IN UNITY - EASY TUTORIAL', 2018. [Online]. Available: https://www.youtube.com/watch?v=ofCLJsSUom0 [Accessed: 16-Mar-2020].
*/