using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenColliderGenerator : MonoBehaviour
{
    public int thickness = 1;

    void Start()
    {
        // To get the screen width and height in world units
        float screenHeight = Camera.main.orthographicSize * 2f;
        float screenWidth = screenHeight * Screen.width / Screen.height;

        // To create the left collider
        GameObject leftCollider = new GameObject("Left Collider");
        leftCollider.transform.parent = transform;
        leftCollider.transform.position = new Vector3(-screenWidth / 2f - thickness / 2f, 0f, 0f);
        leftCollider.AddComponent<BoxCollider2D>().size = new Vector2(thickness, screenHeight + thickness * 2f);

        // To create the right collider
        GameObject rightCollider = new GameObject("Right Collider");
        rightCollider.transform.parent = transform;
        rightCollider.transform.position = new Vector3(screenWidth / 2f + thickness / 2f, 0f, 0f);
        rightCollider.AddComponent<BoxCollider2D>().size = new Vector2(thickness, screenHeight + thickness * 2f);
    }
}
