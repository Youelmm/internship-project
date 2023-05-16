using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHoopCrossing : MonoBehaviour
{
    private bool hasEnteredTheHoopFromTheTop = false;
    private LaunchItem launchItem;
    private GameObject launchableItem;

    private void Start()
    {
        launchItem = GameObject.FindGameObjectWithTag("LaunchableItem").GetComponent<LaunchItem>();
        launchableItem = launchItem.gameObject;
    }

    private void Update()
    {
        CheckIfTheLaunchableItemIsOut();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LaunchableItem"))
        {
            // To check if the launchable item is going through the hoop from the top
            if (other.transform.position.y > transform.position.y)
            {
                hasEnteredTheHoopFromTheTop = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (hasEnteredTheHoopFromTheTop)
        {
            // TODO: to increase player's points
        }
    }

    private void CheckIfTheLaunchableItemIsOut()
    {
        if (Camera.main.WorldToViewportPoint(launchableItem.transform.position).y < 0)
        {
            // To reset the checking
            hasEnteredTheHoopFromTheTop = false;
            // To make the launch possible
            launchItem.CanLaunch();
        }
    }
}
