using UnityEngine;

public class LaunchItem : MonoBehaviour
{
    public float power = 10f; // Launch power
    private Vector2 startPosition; // First-touch position
    private Vector2 endPosition; // Last-touch position
    private Vector2 direction; // Launch direction
    private float distance; // Distance between both positions
    private bool canLaunch = false; // To check if player can launch
    private Rigidbody2D rb; // Item's rigid body
    public float gravityScale = 1f;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        // The item is immobile at each new launch
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        print(gameObject.transform.position);
        // Only with 1 finger
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                startPosition = touch.position;
                canLaunch = true;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                endPosition = touch.position;
                distance = Vector3.Distance(startPosition, endPosition);
                direction = (endPosition - startPosition).normalized;
            }
            else if (touch.phase == TouchPhase.Ended && canLaunch)
            {
                // Give the item its gravity back
                rb.gravityScale = gravityScale;
                canLaunch = false;
                Launch();
            }
        }
    }

    private void Launch()
    {
        rb.AddForce(-direction * distance * power);
    }

}
