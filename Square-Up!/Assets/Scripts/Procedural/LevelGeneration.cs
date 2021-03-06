﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {

    public Transform[] startingPositions;
    public GameObject[] rooms; /*index 0 -> LR, index 1 -> LRB, index 2 -> LRT, index 3 -> LRTB*/
    
    [SerializeField] private GameObject startRoom; /*The room the player spawns in at the beginning of level generation*/
    [SerializeField] private GameObject lastRoom;
    private Collider2D roomDetection;
    
    public bool stopGeneration = false;

    private int direction;
    [SerializeField] private float moveAmount;

    private float timeBtwRoom;
    [SerializeField] private float startTimeBtwRoom = 0.25f;

    [SerializeField] private float minX;
    [SerializeField] private float maxX;
    [SerializeField] private float minY;

    private int downCounter;

    public LayerMask room;

    private void Start() {
        int randStartingPos = Random.Range(0, startingPositions.Length);
        transform.position = startingPositions[randStartingPos].position;
        Instantiate(startRoom, transform.position, Quaternion.identity);
        direction = Random.Range(1 ,6);
    }

    private void Update() {
        if (timeBtwRoom <= 0 && !stopGeneration) {
            Move();
            timeBtwRoom = startTimeBtwRoom;
        } else {
            timeBtwRoom -= Time.deltaTime;
        }
    }

    private void Move() {
        if (direction == 1 || direction == 2) { // Move Right

            if (transform.position.x < maxX) { /*Only move right if within boundaries*/
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x + moveAmount, transform.position.y);
                transform.position = newPos;

                if (transform.position.x == maxX) /*If we're on the right side of the level, spawn an LRTB room...*/
                    Instantiate(rooms[3], transform.position, Quaternion.identity);
                else { /*... otherwise, spawn a random room*/
                    int rand = Random.Range(0, rooms.Length);
                    Instantiate(rooms[rand], transform.position, Quaternion.identity);
                }

                /*To prevent overlapping of rooms*/
                direction = Random.Range(1, 6);
                if (direction == 3) {
                    direction = 2;
                } else if (direction == 4) {
                    direction = 5;
                }
            } else/*Otherwise, move down*/
                direction = 5;

        } else if (direction == 3 || direction == 4) { // Move Left 
            if (transform.position.x > minX) { /*Only move left if within boundaries*/
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                if (transform.position.x == minX) /*If we're on the left side of the level, spawn an LRTB room...*/
                    Instantiate(rooms[3], transform.position, Quaternion.identity);
                else { /*... otherwise, spawn a random room*/
                    int rand = Random.Range(0, rooms.Length); /*Spawn any room type here*/
                    Instantiate(rooms[rand], transform.position, Quaternion.identity);
                }
                direction = Random.Range(3, 6);

            } else { /*Otherwise, move down*/
                direction = 5;
            }
                
        } else if (direction == 5) { //Move Down

            downCounter++;
            if (transform.position.y > minY) {

                roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type !=3) { /*If the room doesn't have a bottom opening...*/
                    
                    if (downCounter >= 2) {
                        if (roomDetection.GetComponent<RoomType>().type == 3)
                            return; /*No point in destroying the room if it's got openings all on sides*/
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    } else {
                        if (roomDetection.GetComponent<RoomType>().type == 3)
                            return;
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        int randomBottomRoom = Random.Range(1, 4);
                        if (randomBottomRoom == 2) {
                            randomBottomRoom = 1;
                        }
                        Instantiate(rooms[randomBottomRoom], transform.position, Quaternion.identity);
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1 ,6);
            }
            else {
                /*Stop level generation*/
                stopGeneration = true;
            }
        }
        if (transform.position.y == minY && stopGeneration) { /*This is how I detect the last room generated*/
            roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
            roomDetection.GetComponent<RoomType>().RoomDestruction();
            Instantiate(lastRoom, transform.position, Quaternion.identity);
        }
    }
}

/*
Sources:
1) B., Blackthornprod, 'ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL - #1', 2018. [Online]. Available: https://www.youtube.com/watch?v=hk6cUanSfXQ. [Accessed: 23-Mar-2020].
2) B., Blackthornprod, 'ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL- #2', 2018. [Online]. Available: https://www.youtube.com/watch?v=XNQQLr0E9TY [Accessed: 24-Mar-2020].
3) B., Blackthornprod, 'ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL - #3 [END]', 2018. [Online]. Available: https://www.youtube.com/watch?v=G9Wa0XZ2a2o [Accessed: 24-Mar-2020].
*/