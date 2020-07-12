using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour {

    private SceneLoader sceneLoader;
    private SaveData saveData;
    private PlayerInput playerInput;

    void Awake() { 
        saveData = new SaveData();
        playerInput = FindObjectOfType<PlayerInput>();
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.name == "Player") {
    
            // playerInput.setPulseJumpSeconds(0.5f);
            // Debug.Log(playerInput.getPulseJumpSeconds());
            // saveData.SaveAllPrimitiveNonUpgrades();
            sceneLoader.LoadNextLevel();
    
            // sceneLoader.LoadNextLevel();
            // SceneManager.LoadScene(stageName);
            // saveData.LoadAllPrimitiveNonUpgrades();
            // Debug.Log(playerInput.getPulseJumpSeconds());
        }
	}
}
