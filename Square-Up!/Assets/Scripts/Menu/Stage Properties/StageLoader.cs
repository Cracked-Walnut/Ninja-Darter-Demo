using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour {

    [SerializeField] private string stageName;
    private SaveData saveData;
    private PlayerInput playerInput;

    void Awake() { 
        saveData = new SaveData();
        playerInput = FindObjectOfType<PlayerInput>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.name == "Player") {
            if (stageName == null || stageName == "") {
                stageName = "Level-1"; // default to level-1
                playerInput.setPulseJumpSeconds(0.5f);
                Debug.Log(playerInput.getPulseJumpSeconds());
                saveData.SaveAllPrimitiveNonUpgrades();
            }

            SceneManager.LoadScene(stageName);
            saveData.LoadAllPrimitiveNonUpgrades();
            Debug.Log(playerInput.getPulseJumpSeconds());
        }
	}
}
