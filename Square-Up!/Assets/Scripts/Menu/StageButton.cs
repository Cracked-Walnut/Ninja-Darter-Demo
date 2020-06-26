using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class StageButton : MonoBehaviour {

    [SerializeField] private Button button;

    void Start() {
        IsButtonDisabled(true);
    }

    public void IsButtonDisabled(bool disabled) {
        if (disabled)
            button.interactable = false;
        else
            button.interactable = true;
    }

    public Button GetButton() {
        return button;
    }
}
