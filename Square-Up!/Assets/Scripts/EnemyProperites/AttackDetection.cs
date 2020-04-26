using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackDetection : MonoBehaviour {
    
    private float raycastDistance = 5.0f; /*Enemy sight*/

    private RaycastHit2D raycastHit2D;
    private Player player;

    [SerializeField] private int enemyBulletDmg = 1;
    [SerializeField] private float enemySpeed = 3.0f;
    [SerializeField] private float sightDistance = 3.0f; /*How far the enemy can see*/
    [SerializeField] private float enemyBulletSpeed  = 40f;
    
    [SerializeField] private Transform playerDetection; /*Using this to find the player*/
    [SerializeField] private Rigidbody2D bulletRb;
    [SerializeField] private Transform enemyFirePoint;
    [SerializeField] private GameObject enemyBulletPrefab;
    

    void Update() {
        checkEnemySight();
    }
    
    void Awake() {
        player = GetComponent<Player>();
    }

    void checkEnemySight() {
        raycastHit2D = Physics2D.Raycast(playerDetection.position, Vector2.right, sightDistance);
        // if (raycastHit2D.collider == player)
            // StartCoroutine(fireBullet(2.0f));
    }

    // IEnumerator fireBullet(float bulletCount) {
    //     for (float i = 0; i <= bulletCount; i++) {
    //         yield return new WaitForSeconds(1);
    //         Instantiate(enemyBulletPrefab, enemyFirePoint.position);
    //     }
    // }
}
