using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBow : MonoBehaviour {

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject arrowPrefab;
    // private float rateOfFire;
    private EnemyArrow enemyArrow;

    void Awake() {
        enemyArrow = FindObjectOfType<EnemyArrow>();
    }

    void shoot() {
            Quaternion accuracy = Quaternion.Euler(0, 0, 0);
            Instantiate(arrowPrefab, firePoint.position, accuracy);
    }
}
