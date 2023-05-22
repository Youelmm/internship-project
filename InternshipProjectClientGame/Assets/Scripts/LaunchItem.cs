using System;
using System.Xml.Serialization;
using UnityEditor;
using UnityEngine;

public class LaunchItem : MonoBehaviour
{
    public float power = 10f; // Launch power
    private Vector2 startPosition; // First-touch position
    private Vector2 endPosition; // Last-touch position
    private Vector2 direction; // Launch direction
    private float distance; // Distance between both positions
    private bool canLaunch = true; // To check if player can launch
    private Rigidbody2D rb; // Item's rigid body
    public float gravityScale = 1f; // Item's gravity scale
    public Vector2 itemInitialPosition; // Item's initial position when it is ready to be launched
    private Vector3 itemLastPosition; // To save item's last position to manage its bounciness
    public float bounceForce = 10;
    public float maximumTouchDistance = 10;
   

    private void Start()
    {
        // To set item's initial position
        itemInitialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        // The item is immobile at each new launch
        rb.gravityScale = 0f;
    }

    private void Update()
    {
        // Only possible with 1 finger
        if (Input.touchCount == 1)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began && canLaunch)
            {
                startPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Moved)
            {
                endPosition = touch.position;
                distance = Vector3.Distance(startPosition, endPosition);
                // To make sure player does not launch too far
                if (distance > maximumTouchDistance)
                {
                    distance = maximumTouchDistance;
                }
                direction = (endPosition - startPosition).normalized;
            }
            else if (touch.phase == TouchPhase.Ended && canLaunch)
            {
                // To give the item its gravity back
                rb.gravityScale = gravityScale;
                canLaunch = false;
                Launch();
            }
        }
    }

    private void Launch()
    {
        rb.velocity = -direction * distance * power;
        //rb.AddForce(-direction * distance * power);
    }

    public void CanLaunch()
    {
        canLaunch = true;
        MakeTheLaunchableItemImmobile();
    }

    private Vector2 GetInitialPosition()
    {
        // TODO
        return new Vector2();
    }

    private void MakeTheLaunchableItemImmobile()
    {
        // To set the launchable item at its new initial position
        transform.position = itemInitialPosition;
        // To make it immobile
        rb.gravityScale = 0f;
        rb.velocity = Vector2.zero;
        rb.angularVelocity = 0f;
        // To reset its rotation values
        transform.rotation = Quaternion.identity;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 direction = transform.position - itemLastPosition;
        direction.Normalize();

        if (other.gameObject.name == "Left Collider" || other.gameObject.name == "Right Collider")
        {
            float speed = rb.velocity.magnitude;
            Vector2 localDirection = Vector2.Reflect(rb.velocity.normalized, other.GetContact(0).normal);

            // If the item is going down and hits a border then bounciness is not applied (otherwise it makes the item go down very fast)
            if (itemLastPosition.y > rb.gameObject.transform.position.y)
            {
                rb.velocity = direction * speed * power;
            }
            else
            {
                rb.velocity = direction * speed * power * bounceForce;
            }
            //rb.AddForce(direction * bounceForce, ForceMode2D.Impulse);
        }
    }
    private void FixedUpdate()
    {
        // To update item's last postition
        itemLastPosition = transform.position;
    }
}
