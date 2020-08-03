using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBow : MonoBehaviour {

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject projectile;
    [SerializeField] private string direction;
    // private float rateOfFire;
    private EnemyArrow enemyArrow;
    private Quaternion accuracy;

    void Awake() {
        enemyArrow = FindObjectOfType<EnemyArrow>();
    }

    void shoot() {

        switch(direction) { // firing direction
            case "left":
                accuracy = Quaternion.Euler(0, 0, 0);
                break;

            case "right":
                accuracy = Quaternion.Euler(0, 0, 180);
                break;
            
            default:
                accuracy = Quaternion.Euler(0, 0, 0);
                break;
        }
            
            Instantiate(projectile, firePoint.position, accuracy);
    }
}
