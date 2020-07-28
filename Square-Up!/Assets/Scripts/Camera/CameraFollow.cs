using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

	[SerializeField] private Transform target;
	[SerializeField] private float smoothSpeed = 0.125f;
	[SerializeField] private Vector3 offset;

	// void FixedUpdate () {

	// 	Vector3 desiredPosition = target.position + offset; //Adding x, y, z position of our target to our offset
	// 	Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
	// 	transform.position = desiredPosition;
	// }

	void FixedUpdate() {

		// starting height
		if (target.transform.position.y > 200f && target.transform.position.y < 240f)
			transform.position = new Vector3(target.transform.position.x, 220f, -10f);

		// 1 chunk below
		else if (target.transform.position.y < 200f && target.transform.position.y > 160f)
			transform.position = new Vector3(target.transform.position.x, 180f, -10f);
		
		// 2 chunks below
		else if (target.transform.position.y < 160f && target.transform.position.y > 120f)
			transform.position = new Vector3(target.transform.position.x, 140f, -10f);
		
		// 3 chunks below
		else if (target.transform.position.y < 120f && target.transform.position.y > 80f)
			transform.position = new Vector3(target.transform.position.x, 100f, -10f);

		// 4 chunks below
		else if (target.transform.position.y < 80f && target.transform.position.y > 40f)
			transform.position = new Vector3(target.transform.position.x, 60f, -10f);

		// 5 chunks below
		else if (target.transform.position.y < 40f && target.transform.position.y > 0f)
			transform.position = new Vector3(target.transform.position.x, 20f, -10f);

		// 1 chunk above
		else
			transform.position = new Vector3(target.transform.position.x, 230f, -10f);
	
		// if(target.transform.position.y < -20f && target.transform.position.y > - 60f)
		// 	transform.position = new Vector3(target.transform.position.x, -40f, -10f);

	}
/*
1) B., Brackeys, 'Smooth Camera Follow in Unity - Tutorial', 2017. [Online]. Available: https://www.youtube.com/watch?v=MFQhpwc6cKE [27-Jul-2020].
2) B.R., Redpath, 'Only make camera follow player on x axis?', 2017. [Online]. Available:  https://gamedev.stackexchange.com/questions/147526/only-make-camera-follow-player-on-x-axis [Accessed: 27-Jul-2020].
*/

}//end of class
