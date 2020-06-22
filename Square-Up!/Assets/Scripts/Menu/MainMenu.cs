using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*This script will load the game and verify the game isn't paused*/

public class MainMenu : MonoBehaviour {

    public void PlayGame() { 
        
        SceneManager.LoadScene("Stage-Selector");
        if (Time.timeScale != 1.0f) 
            Time.timeScale = 1.0f; /*If the game is paused, unpause it*/
    }

    public void QuitGame() { 
        Debug.Log("Quit");
        Application.Quit(); 
    }
}