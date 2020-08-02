using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour {

    protected List<string> animationList = new List<string>();
    [SerializeField] private float health;
    protected float attackRate;
    protected bool isAlive;
    private Animator animator;

    /*-----------------------------------------------------*/
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject fireBallPrefab;
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
        // attack();
    }

    void initAnimations() { 
        animationList = new List<string>();
        animationList.Add("Fire Wisp Attack");
    }

    void attack() { // change to fire projectile through animation window
        attackRate -= Time.deltaTime;
        fireAngle = Quaternion.Euler(0, 0, 90);

        if (attackRate <= 0.0f) {
            animator.SetBool("isAttacking", true);
            Instantiate(fireBallPrefab, firePoint.position, fireAngle);
            attackRate = 1.0f;
        }
    }
    /*-----------------------------------------------------*/

    public void takeDamage(float damage) {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            Die();
    }

    public void Die() {
        isAlive = false;
        animator.enabled = !animator.enabled;
        Destroy(gameObject);
    }

    public bool getIsAlive() {
        return isAlive;
    }
}
