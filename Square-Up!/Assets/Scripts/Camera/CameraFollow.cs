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
		if (target.transform.position.y > 200f/* && target.transform.position.y < 240f*/)
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
	
	}

	public IEnumerator Shake (float duration, float magnitude) {
        
        Vector3 originalPos = transform.position;
    
        float elapsed = 0.0f;

        while (elapsed < duration) {
			// Generate a random x & y value, then add it to the current camera x & y coordinate, creating a shake effect
            float x = (Random.Range(-1f, 1f) * magnitude) + transform.position.x; 
            float y = (Random.Range(-1, 1f) * magnitude) + transform.position.y;

            transform.position = new Vector3(x ,y, originalPos.z); // apply the shake to the current frame

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.position = originalPos; // revert the camera to its original position
    }
/*
1) B., Brackeys, 'Smooth Camera Follow in Unity - Tutorial', 2017. [Online]. Available: https://www.youtube.com/watch?v=MFQhpwc6cKE [27-Jul-2020].
2) B.R., Redpath, 'Only make camera follow player on x axis?', 2017. [Online]. Available:  https://gamedev.stackexchange.com/questions/147526/only-make-camera-follow-player-on-x-axis [Accessed: 27-Jul-2020].
3) B., Brackeys, 'CAMERA SHAKE in Unity', 2018. [Online]. Available: https://www.youtube.com/watch?v=9A9yj8KnM8c [Accessed: 03-Aug-2020].
*/

}//end of class
