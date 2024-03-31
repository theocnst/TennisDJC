using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimTarget : MonoBehaviour
{
    private Vector3 initialTargetPosition; // Initial position of the ball

    public float moveDistance = 2.5f; // Distance to move along the Z-axis
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
            transform.position += new Vector3(0, 0, step);
        }
        else
        {
            transform.position -= new Vector3(0, 0, step);
        }

        // Check if the ball has reached the specified move distance from the initial position
        if (movingForward && transform.position.z >= initialTargetPosition.z + moveDistance)
        {
            movingForward = false; // Change direction
        }
        else if (!movingForward && transform.position.z <= initialTargetPosition.z - moveDistance)
        {
            movingForward = true; // Change direction
        }
    }
}