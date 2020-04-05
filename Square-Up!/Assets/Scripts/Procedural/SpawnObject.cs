using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    public GameObject[] objects;

    private void Start() {
        int rand = Random.Range(0, objects.Length);
        GameObject instance = (GameObject) Instantiate(objects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }
}
/*
Sources:
1) B., Blackthornprod, 'ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL - #1', 2018. [Online]. Available: https://www.youtube.com/watch?v=hk6cUanSfXQ
*/