using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/*This script is for objects which have unique collision traits which 
aren't enemies, or the player*/

public class ObjectCollision : MonoBehaviour {

	private AudioManager audioManager;
	private void Awake() {
		audioManager = FindObjectOfType<AudioManager>();
	}

	void OnCollisionEnter2D(Collision2D collision) { 
		
		audioManager.Play("hit_1");
		if (collision.gameObject.name == "Player") 
			Restart_Scene();
	}

	public void Restart_Scene() { 
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); 
	}

}//end of class

/*
Sources:
1) L.M., Marsden, 'Unity - Detecting Collisions between gameObjects and removing game objects', 2018. [Online]. Available: https://www.youtube.com/watch?v=KVhSCck_5yI [Accessed: 10-Dec-2019].
*/