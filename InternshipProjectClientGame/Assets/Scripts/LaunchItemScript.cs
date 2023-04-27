using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchItemScript : MonoBehaviour
{
    public Vector2? firstTouchPosition, lastTouchPosition;

    void Start()
    {
    }

    void Update()
    {
        GetTouchPositions();
    }

    void GetTouchPositions()
    {
        // Check if the screen is being touched
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (firstTouchPosition == null)
            {
                firstTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            }
            // Get the last-touch's position
            else
            {
                lastTouchPosition = Camera.main.ScreenToWorldPoint(touch.position);
            }

        }
        // Check if the user stopped touching
        else if (firstTouchPosition != null && lastTouchPosition != null)
        {
            // Launch the item
            LaunchItem();
        }
    }

    void LaunchItem()
    {
        // TODO
    }
}
