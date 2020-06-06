using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnObject : MonoBehaviour {

    private int rand;

    public GameObject[] objects;
    private GameObject instance;
    private RoomType roomType;

    void Awake() {
        roomType = FindObjectOfType<RoomType>();
        rand = Random.Range(0, objects.Length);
    }

    private void Start() {
        if (objects == null || objects.Length == 0)
            return;
        else
            SpawnRoom();
        }

    void SpawnRoom() {
        int rand = Random.Range(0, objects.Length);
        instance = (GameObject) Instantiate(objects[rand], transform.position, Quaternion.identity);
        instance.transform.parent = transform;
    }
}

/*
Sources:
1) B., Blackthornprod, 'ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL - #1', 2018. [Online]. Available: https://www.youtube.com/watch?v=hk6cUanSfXQ
*/