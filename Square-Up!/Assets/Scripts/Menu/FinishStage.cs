using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishStage : MonoBehaviour {

    private SceneLoader sceneLoader;
    private StageButton stageButton;

    void Awake() {
        sceneLoader = FindObjectOfType<SceneLoader>();
        stageButton = FindObjectOfType<StageButton>();
    }

    void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.name == "Player") {
            sceneLoader.loadStage("Stage_Selector");
            stageButton.IsButtonDisabled(false);
        }
	}
}
