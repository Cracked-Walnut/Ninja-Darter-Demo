using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField] private Transform target;
	[SerializeField] private float smoothSpeed = 0.125f;
	[SerializeField] private Vector3 offset;

	void Update() { }

	// void FixedUpdate () {

	// 	Vector3 desiredPosition = target.position + offset; //Adding x, y, z position of our target to our offset
	// 	Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
	// 	transform.position = desiredPosition;
	// }

	void FixedUpdate() {

		if (target.transform.position.y > 200f && target.transform.position.y < 240f) {
			transform.position = new Vector3(target.transform.position.x, 210f, -10f);
		}
		else if (target.transform.position.y < 200f && target.transform.position.y > 160f) {
			transform.position = new Vector3(target.transform.position.x, 180f, -10f);
		} else
			transform.position = new Vector3(target.transform.position.x, 230f, -10f);
	
		// if(target.transform.position.y < -20f && target.transform.position.y > - 60f)
		// 	transform.position = new Vector3(target.transform.position.x, -40f, -10f);

		
	}
/*
https://gamedev.stackexchange.com/questions/147526/only-make-camera-follow-player-on-x-axis
1) B., Brackeys, 'Smooth Camera Follow in Unity - Tutorial', 2017. [Online]. Available: https://www.youtube.com/watch?v=MFQhpwc6cKE
*/

}//end of class
