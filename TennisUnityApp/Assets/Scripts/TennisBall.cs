using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TennisBall : MonoBehaviour
{
    private Vector3 initialBallPosition; // Initial position of the ball

    void Start()
    {
        initialBallPosition = transform.position; // Save the initial position of the ball
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            GetComponent<Rigidbody>().velocity = Vector3.zero;
            transform.position = initialBallPosition;
        }
    }
}
