using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wisp : MonoBehaviour {

    [SerializeField] private float health;
    // protected float attackRate;
    protected bool isAlive;

    private Animator animator;
    private Quaternion fireAngle;
    // protected List<string> animationList = new List<string>();

    // Start is called before the first frame update
    void Start() {
        // attackRate = 1.0f;
        // initAnimations();
    }

    void Awake() {
        animator = GetComponent<Animator>();
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

    public void Die() {
        Destroy(gameObject);
    }

    public bool getIsAlive() {
        return isAlive;
    }
}
