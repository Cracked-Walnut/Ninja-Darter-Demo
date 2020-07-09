using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePointer : MonoBehaviour {

    
    [SerializeField] private GameObject crosshair;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private GameObject firePoint;
    
    private float bulletSpeed = 60.0f;
    private Vector3 target;

    // Start is called before the first frame update
    void Start() {
        Cursor.visible = false;
        // Cursor.SetCursor(crosshairPNG, Vector2.zero, CursorMode.ForceSoftware);
    }

    void Update() {

        // target = transform.GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Input.mousePosition.z));
        // target = Input.mousePosition;
        target = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        crosshair.transform.position = new Vector2(target.x, target.y);

        Vector3 difference = target - player.transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // player.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);

        if (Input.GetMouseButtonDown(0)) {
            float distance = difference.magnitude;
            Vector2 direction = difference / distance;
            fireBullet(direction, rotationZ);
        }

    }

    void fireBullet(Vector2 direction, float rotationZ) {
        GameObject b = Instantiate(bulletPrefab) as GameObject;
        b.transform.position = firePoint.transform.position;
        b.transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
        b.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
    }
}

/*
Sources:
1) O. T., OXMOND Tutorials, 'How To Change The Cursor In Unity! [Unity3D 2019 Beginner Tutorial]', 2019. [Online]. Available: https://www.youtube.com/watch?v=W4SE0_cfAqc [Accessed: 05-Jul-2020].
*/