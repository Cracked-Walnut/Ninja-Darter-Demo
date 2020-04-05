using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverMenu : MonoBehaviour {

    private PlayerInput playerInput;
    public GameObject gameOverUI;

    private void Awake() {
        playerInput = FindObjectOfType<PlayerInput>();
    }

    public void showGameOverScreen() {
        Debug.Log("showGameOverScreen()");
        playerInput.setEscapeKey(false);
        setActive(true);
        Time.timeScale = 0f;
    }

    public bool setActive(bool isActive) {
        gameOverUI.SetActive(isActive);
        Debug.Log("isActive" + isActive);

        return isActive;
    }
}
