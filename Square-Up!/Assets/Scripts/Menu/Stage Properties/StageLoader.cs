using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageLoader : MonoBehaviour {

    [SerializeField] private string stageName;


    void OnCollisionEnter2D(Collision2D collision) {
		if (collision.gameObject.name == "Player") {
            if (stageName == null || stageName == "")
                stageName = "Stage-Select"; // default to stage select

            SceneManager.LoadScene(stageName);
        }
	}
}
