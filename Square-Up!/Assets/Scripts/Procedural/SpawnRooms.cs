using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRooms : MonoBehaviour {
    
    public LayerMask whatIsRoom;
    public LevelGeneration levelGeneration;

    // Update is called once per frame
    void Update()
    {
        Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, whatIsRoom);
        if (roomDetection == null && levelGeneration.stopGeneration) {
            spawnRoom();
        }
    }

    void spawnRoom() {
        /*Spawn Random Room*/
            int rand = Random.Range(0, levelGeneration.rooms.Length);
            Instantiate(levelGeneration.rooms[rand], transform.position, Quaternion.identity);
            Debug.Log(levelGeneration.rooms[rand]);
            Destroy(gameObject);
    }
}
