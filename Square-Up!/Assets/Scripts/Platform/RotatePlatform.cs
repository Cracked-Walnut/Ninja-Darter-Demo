using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*This script will rotate the game object it's attached to*/

public class RotatePlatform : MonoBehaviour {

	public float rotationSpeed = 5f;

    // Update is called once per frame
    void Update() {
        transform.Rotate(0, 0, rotationSpeed);
    }
}
