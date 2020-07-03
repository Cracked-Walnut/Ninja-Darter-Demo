using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour {

    [SerializeField] private string stageName;

    void Awake() { }

    void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.name == "Player") {
            if (stageName == null || stageName == "")
                stageName = "Level-1"; // default to level-1

            SceneManager.LoadScene(stageName);
        }
	}
}
