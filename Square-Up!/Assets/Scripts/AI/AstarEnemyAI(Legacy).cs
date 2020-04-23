using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AstarEnemyAI : MonoBehaviour {

    public Transform target;
    public float speed = 200;
    public float nextWayPointDistance = 3f;

    private Path path;
    private int currentWaypoint = 0;
    private bool reachedEndOfPath = false;

    private Seeker seeker;
    private Rigidbody2D rigidbody2D;

    void Start() {
        seeker = GetComponent<Seeker>();
        rigidbody2D = GetComponent<Rigidbody2D>();

        seeker.StartPath(rigidbody2D.position, target.position, OnPathComplete);
    }

    void OnPathComplete(Path path) {
        if (this.path.error){
            path = this.path;
            currentWaypoint = 0;
        }

    }

    // Update is called once per frame
    void FixedUpdate() {
        if (path == null) 
            return;
        if (currentWaypoint >= path.vectorPath.Count) {
            reachedEndOfPath = true;
            return;
        } else {
            reachedEndOfPath = false;
        }

        Vector2 direction = ((Vector2) path.vectorPath[currentWaypoint] - rigidbody2D.position).normalized;
        Vector2 force = direction * speed * Time.deltaTime;

        rigidbody2D.AddForce(force);

        float distance = Vector2.Distance(rigidbody2D.position, path.vectorPath[currentWaypoint]);

        if (distance < nextWayPointDistance) {
            currentWaypoint++;
        }
    }
}
