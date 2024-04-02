using UnityEngine;

public class AimTarget : MonoBehaviour
{
    private Vector3 initialTargetPosition; // Initial position of the ball

    public float moveDistance = 2.5f; // Distance to move along the X-axis
    public float moveSpeed = 1.0f; // Speed of the movement
    private bool movingForward = true; // Direction of movement

    void Start()
    {
        initialTargetPosition = transform.position; // Save the initial position of the ball
    }

    void Update()
    {
        // Calculate the current movement direction and distance
        float step = moveSpeed * Time.deltaTime;

        if (movingForward)
        {
            transform.position += new Vector3(step, 0, 0);
        }
        else
        {
            transform.position -= new Vector3(step, 0, 0);
        }

        // Check if the ball has reached the specified move distance from the initial position
        if (movingForward && transform.position.x >= initialTargetPosition.x + moveDistance)
        {
            movingForward = false; // Change direction
        }
        else if (!movingForward && transform.position.x <= initialTargetPosition.x - moveDistance)
        {
            movingForward = true; // Change direction
        }
    }
}