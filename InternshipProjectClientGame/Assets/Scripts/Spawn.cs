using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    private float screenWidth;
    private float screenHeight;
    private float objectWidth;
    private float objectHeight;

    public void Start()
    {
        screenWidth = Camera.main.orthographicSize * 2f * Screen.width / Screen.height;
        screenHeight = Camera.main.orthographicSize * 2f;

        objectWidth = GetComponent<Renderer>().bounds.size.x;
        objectHeight = GetComponent<Renderer>().bounds.size.y;
    }

    public void Respawn()
    {
        // Random position
        float xDelta = Random.Range(-1, 1);
        float yDelta = Random.Range(-1, 1);

        // To set the new position
        Vector3 newPosition = new Vector3(transform.position.x + xDelta, transform.position.y + yDelta, 0f);
        transform.position = newPosition;
    }
}
