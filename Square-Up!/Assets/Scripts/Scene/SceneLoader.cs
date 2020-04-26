﻿using UnityEngine;
using UnityEngine.SceneManagement;
using System.Threading;
using System.Collections;
using System.Collections.Generic;

public class SceneLoader : MonoBehaviour {
	
	private AudioManager audioManager;
	private GameOverMenu gameOverMenu;
	private PlayerPosition playerPosition;
	private Weapon weapon;
	private PlayerInput playerInput;

	void Awake() {
		audioManager = FindObjectOfType<AudioManager>();
		gameOverMenu = FindObjectOfType<GameOverMenu>();
		playerPosition = FindObjectOfType<PlayerPosition>();
		weapon = FindObjectOfType<Weapon>();
		playerInput = FindObjectOfType<PlayerInput>();
	}

	
	void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.name == "Player") {
			audioManager.Play("Win Level");

			if (Time.timeScale == 1.0f)
				Invoke("levelComplete", 1); /*Wait one second then execute function*/
		}
	}

	public void Restart_Scene() { /*Used when the player dies or player hits the R key*/
		if (playerPosition.getCheckPointSwitch()) {
			handleUI();
			playerPosition.applyCheckPoint();
		} else {
			handleUI();
			playerPosition.applyInitialPoint();
			// SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);/*...restart the scene*/
        }
	}

	private void handleUI() {
		Debug.Log(Time.timeScale);
		Time.timeScale = 1f;
		Debug.Log(Time.timeScale);
		gameOverMenu.setActive(false);
	}

	public void levelComplete() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void showGameComplete() {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
	}

	public void loadStage(int stage) {
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + stage);
	}
}//end of class


/*
Sources:
1) I., ibra, 'Switch Between Scenes in unity', 2019. [Online]. Available: https://www.andruni.com/switch-between-scenes-in-unity/ [Accessed: 14-Dec-2019].
2) S.A., Sam Allen, 'C# Array Examples, String Arrays', 2007. [Online]. Available: https://www.dotnetperls.com/array [Accessed: 28-Jan-2020].
3) B., Brackeys, 'START MENU in Unity', 2017. [Online]. Available: https://www.youtube.com/watch?v=zc8ac_qUXQY [Accessed: 1-Feb-2020].
*/
