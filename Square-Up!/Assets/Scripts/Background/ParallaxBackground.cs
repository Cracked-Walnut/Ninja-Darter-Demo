using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    [SerializeField] private Vector2 parallaxEffectMultiplier;
    [SerializeField] private bool infiniteHorizontal;
    [SerializeField] private bool infiniteVertical;
    

    private Transform cameraTransform;
    private Vector3 lastCameraPosition;
    private float textureUnitSizeX;
    private float textureUnitSizeY;

    void Start() {
        cameraTransform = Camera.main.transform;
        lastCameraPosition = cameraTransform.position;
        Sprite sprite = GetComponent<SpriteRenderer>().sprite;
        Texture2D texture = sprite.texture;
        textureUnitSizeX = (texture.width * 5) / sprite.pixelsPerUnit; // make the "5" a variable later
        textureUnitSizeY = (texture.height * 5) / sprite.pixelsPerUnit; 
    }

    void LateUpdate() {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxEffectMultiplier.x, deltaMovement.y * parallaxEffectMultiplier.y);
        lastCameraPosition = cameraTransform.position;


        if (infiniteHorizontal) {
            if (Mathf.Abs(cameraTransform.position.x - transform.position.x) >= textureUnitSizeX) {
                float offsetPositionX = (cameraTransform.position.x - transform.position.x) % textureUnitSizeX;
                transform.position = new Vector3(cameraTransform.position.x + offsetPositionX, transform.position.y);
            }
        }

        if (infiniteVertical) {
            if (Mathf.Abs(cameraTransform.position.y - transform.position.y) >= textureUnitSizeY) {
                float offsetPositionY = (cameraTransform.position.y - transform.position.y) % textureUnitSizeY;
                transform.position = new Vector3(transform.position.x, cameraTransform.position.y + offsetPositionY);
            }
        }
    }
}

/*
Sources:
1) C.M., Code Monkey, 'Parallax Infinite Scrolling Background in Unity', 2019. [Online]. Available: https://www.youtube.com/watch?v=wBol2xzxCOU [Accessed: 20-Jul-2020].
*/
