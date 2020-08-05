using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour {

    [SerializeField] private float health;
    // protected float attackRate;
    protected bool isAlive;

    private Animator animator;
    private Quaternion fireAngle;
    private BoxCollider2D boxCollider2D;
    // protected List<string> animationList = new List<string>();

    // Start is called before the first frame update
    void Start() {
        // attackRate = 1.0f;
        // initAnimations();
    }

    void Awake() {
        animator = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();
    }

    // void initAnimations() { 
    //     animationList = new List<string>();
    //     animationList.Add("Fire Wisp Attack");
    // }

    public void takeDamage(float damage) {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            animator.SetTrigger("Dead");
    }

    void disableCollider() {
        boxCollider2D.enabled = !boxCollider2D.enabled; 
    }

    public void Die() {
        Destroy(gameObject);
    }

    public bool getIsAlive() {
        return isAlive;
    }
}
