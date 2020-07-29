using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcher : Enemy {

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject arrowPrefab;
    private Quaternion fireAngle;

    // Start is called before the first frame update
    void Start() {
        attackRate = 1.0f;
        initAnimations();
    }

    void Awake() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        attack();
    }

    void initAnimations() { 
        animationList = new List<string>();
        animationList.Add("Skeleton Attack");
    }

    void attack() {
        attackRate -= Time.deltaTime;
        fireAngle = Quaternion.Euler(0, 0, 90);

        if (attackRate <= 0.0f) {
            animator.SetBool("isAttacking", true);
            Instantiate(arrowPrefab, firePoint.position, fireAngle);
            attackRate = 1.0f;
        }
    }
}
