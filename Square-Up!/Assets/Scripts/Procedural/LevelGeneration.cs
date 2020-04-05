using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelGeneration : MonoBehaviour {

    public Transform[] startingPositions;
    public GameObject[] rooms; /*index 0 -> LR, index 1 -> LRB, index 2 -> LRT, index 3 -> LRTB*/
    
    [SerializeField] private GameObject startRoom, 
        altStartRoom;

    /*Make a last rooms array. Modify this script so you call one of these random rooms as the last generated room
        1) I want to be able to grab the last room generated, destroy it, then pick a random room from the last rooms
        array and replace it with that*/
    
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

                int rand = Random.Range(0, rooms.Length); /*Spawn any room type here*/
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                /*To prevent overlapping of rooms*/
                direction = Random.Range(1, 6);
                if (direction == 3) {
                    direction = 2;
                } else if (direction == 4) {
                    direction = 5;
                }
            } else { /*Otherwise, move down*/
                direction = 5;
            }
        } else if (direction == 3 || direction == 4) { // Move Left 
            if (transform.position.x > minX) { /*Only move left if within boundaries*/
                downCounter = 0;
                Vector2 newPos = new Vector2(transform.position.x - moveAmount, transform.position.y);
                transform.position = newPos;

                int rand = Random.Range(0, rooms.Length); /*Spawn any room type here*/
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(3, 6);

            } else { /*Otherwise, move down*/
                direction = 5;
            }
                
        } else if (direction == 5) { //Move Down

            downCounter++;
            if (transform.position.y > minY) {

                Collider2D roomDetection = Physics2D.OverlapCircle(transform.position, 1, room);
                if (roomDetection.GetComponent<RoomType>().type != 1 && roomDetection.GetComponent<RoomType>().type !=3) {
                    
                    if (downCounter >= 2) {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();
                        Instantiate(rooms[3], transform.position, Quaternion.identity);
                    } else {
                        roomDetection.GetComponent<RoomType>().RoomDestruction();

                        // int randomBottomRoom = Random.Range(1, 4);
                        // if (randomBottomRoom == 2) {
                        //     randomBottomRoom = 1;
                        // }
                        Instantiate(altStartRoom, transform.position, Quaternion.identity);
                    }
                }

                Vector2 newPos = new Vector2(transform.position.x, transform.position.y - moveAmount);
                transform.position = newPos;

                int rand = Random.Range(2, 4);
                Instantiate(rooms[rand], transform.position, Quaternion.identity);

                direction = Random.Range(1 ,6);
            } else {
                /*Stop level generation*/
                stopGeneration = true;
            }
        }
    }
}

/*
Sources:
1) B., Blackthornprod, 'ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL - #1', 2018. [Online]. Available: https://www.youtube.com/watch?v=hk6cUanSfXQ. [Accessed: 23-Mar-2020].
2) B., Blackthornprod, 'ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL- #2', 2018. [Online]. Available: https://www.youtube.com/watch?v=XNQQLr0E9TY [Accessed: 24-Mar-2020].
3) B., Blackthornprod, 'ROGUE LIKE RANDOM LEVEL GENERATION - INTERMEDIATE C#/UNITY TUTORIAL - #3 [END]', 2018. [Online]. Available: https://www.youtube.com/watch?v=G9Wa0XZ2a2o [Accessed: 24-Mar-2020].
*/