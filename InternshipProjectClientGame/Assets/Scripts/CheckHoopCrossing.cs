using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckHoopCrossing : MonoBehaviour
{
    public bool hasEnteredTheHoopFromTheTop = false;
    private LaunchItem launchItem;
    private GameObject launchableItem;
    public int score = 0;
    public int screenBorderCollisionCount = 0;

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

    private void CheckIfTheLaunchableItemIsOut()
    {
        if (Camera.main.WorldToViewportPoint(launchableItem.transform.position).y < 0)
        {
            if (hasEnteredTheHoopFromTheTop)
            {
                // TODO: to increase player's points and to send it to the server
                score += screenBorderCollisionCount + 1;
            }
            else if (!launchItem.canLaunch)
            {
                // To save the score
                PlayerPrefs.SetString("score", score.ToString());
                // To reset the score
                score = 0;
                // To load the game-over scene
                SceneManager.LoadScene(2);
            }
            // To reset the checking
            hasEnteredTheHoopFromTheTop = false;
            // To make the launch possible
            launchItem.CanLaunch();
            // To reset the screen border collision count when the launch is finished
            screenBorderCollisionCount = 0;
        }
    }
}
