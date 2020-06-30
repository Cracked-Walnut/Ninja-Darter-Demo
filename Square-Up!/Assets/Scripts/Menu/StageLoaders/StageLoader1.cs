using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader1 : MonoBehaviour {

    [SerializeField] private string stageName;


    void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.name == "Player") {
            if (stageName == null || stageName == "")
                stageName = "Level-1";

            SceneManager.LoadScene(stageName);
        }
	}
}
