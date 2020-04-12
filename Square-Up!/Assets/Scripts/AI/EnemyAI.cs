using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This script will have a game object move back and fourth based off of two other game objects used as coordinates
*/

public class EnemyAI : MonoBehaviour {

	[SerializeField] private Transform startPos, endPos;
	[SerializeField] private bool repeatable = false;
	[SerializeField] private float speed = 1.0f;
	[SerializeField] private float duration  = 3.0f;
	[SerializeField] private float startTime, totalDistance;

    // Start is called before the first frame update
    IEnumerator Start() {
      
    	startTime = Time.time;
    	totalDistance = Vector3.Distance(startPos.position, endPos.position);

    	while (repeatable) {
    		yield return RepeatLerp(startPos.position, endPos.position, duration);
            yield return RepeatLerp(endPos.position, startPos.position, duration);
    	}

    }//end of Start()

    // Update is called once per frame
    void Update() {
      	checkForNonRepeatable();

    }//end of Update()

	void checkForNonRepeatable() {
		if (!repeatable) {
      		float currentDuration = (Time.time - startTime) * speed;
            float journeyFraction = currentDuration / totalDistance;
            this.transform.position = Vector3.Lerp(startPos.position, endPos.position, journeyFraction);
      	}
	}

    public IEnumerator RepeatLerp(Vector3 a, Vector3 b, float time) {
    	float i = 0.0f;
    	float rate = (1.0f / time) * speed;
    	while (i < 1.0f) {
    		i += Time.deltaTime * rate;
    		this.transform.position = Vector3.Lerp(a, b, i);
    		yield return null;
    	}
    }

}//end of class

/*
Sources:
1) G.R., Rigato, 'Simple Timer', 2012. [Online]. Available: https://answers.unity.com/questions/351420/simple-timer-1.html [Accessed: 11-Nov-2019].
2) Bampf, 'How to move a GameObject from his position to a xyz position.', 2009. [Online]. Available: https://answers.unity.com/questions/8291/how-to-move-a-gameobject-from-his-position-to-a-xy.html [Accessed: 11-Nov-2019].
3) Unity Technologies, 'Vector3.Lerp', 2019. [Online]. Available: https://docs.unity3d.com/ScriptReference/Vector3.Lerp.html
4) R.C., Renaissance Coders, 'Unity 3d Lerp GameObjects', 2017. [Available]: https://www.youtube.com/watch?v=fIeQG89OOGs&t=692s [Accessed: 12-Nov-2019].
*/