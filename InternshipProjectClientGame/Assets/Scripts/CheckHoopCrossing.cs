using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckHoopCrossing : MonoBehaviour
{
    private bool hasEnteredTheHoopFromTheTop = false;
    private LaunchItem launchItem;

    private void Start()
    {
        launchItem = GameObject.FindGameObjectWithTag("LaunchableItem").GetComponent<LaunchItem>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("LaunchableItem"))
        {
            // To check if the launchable item is going through the hoop from the top
            // TODO: to fix the bug when the launchable item collides the hoop right border
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
            // TODO
        }
        // Reset the checking variable
        hasEnteredTheHoopFromTheTop = false;
        // Make the launch finished so as to be able to restart another one
        launchItem.CanLaunch();
    }
}
