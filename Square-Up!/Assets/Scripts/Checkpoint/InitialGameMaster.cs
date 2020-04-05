using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialGameMaster : MonoBehaviour {
    private static InitialGameMaster initialInstance;
    public Vector2 lastCheckPointPosition;

    void Awake() {
        if (initialInstance == null) {
            initialInstance = this;
            DontDestroyOnLoad(initialInstance);
        } else {
            Destroy(gameObject);
        }
    }
}
