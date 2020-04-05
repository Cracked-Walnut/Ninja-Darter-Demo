using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*This script is called when the game needs to be paused. The float constants are used to freeze and unfreeze the game when appropriate*/

public class PauseMenu : MonoBehaviour {

    public static bool GameIsPaused = false;
    public GameObject pauseMenuUI;
    private const float NORMAL_TIME_FLOW = 1f;
    private const float FROZEN_TIME_FLOW = 0f;

    // Update is called once per frame
    void Update() { }

    public void ResumeGame() {
        pauseMenuUI.SetActive(false);
        Time.timeScale = NORMAL_TIME_FLOW;
        GameIsPaused = false;
    }

    public void PauseGame() {
        pauseMenuUI.SetActive(true);
        Time.timeScale = FROZEN_TIME_FLOW;
        GameIsPaused = true;
    }

    public void LoadMenu() {
        Debug.Log("LoadMenu()");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); // Access the Main Menu Scene
    }

    public void QuitGame() {
        Debug.Log("QuitGame()");
        Application.Quit();
    }

    public float getNormalTime() {
        return NORMAL_TIME_FLOW;
    }

    public float getFrozenTime() {
        return FROZEN_TIME_FLOW;
    }

    public bool getGameIsPaused(){
        return GameIsPaused;
    }
}//end of class

/*
1) B., Brackeys, 'PAUSE MENU in Unity', 2017. [Online]. Available: https://www.youtube.com/watch?v=JivuXdrIHK0 [Accessed: 03-Feb-2020].
*/
