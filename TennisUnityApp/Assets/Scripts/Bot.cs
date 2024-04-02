using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bot : MonoBehaviour
{
    float speed = 3f;
    float force = 13f;

    Animator animator;

    public Transform ball;
    public Transform aimTarget;

    Vector3 targetPosition;
    Vector3 aimTargetInitialPosition;


    // Start is called before the first frame update
    void Start()
    {
        targetPosition = transform.position;
        animator = GetComponent<Animator>();
        aimTargetInitialPosition = aimTarget.position;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }

    void Move()
    {
        targetPosition.x = ball.position.x;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TennisBall"))
        {
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 6, 0);

            Vector3 ballDir = ball.position - transform.position;

            if (ballDir.x <= 0)
                animator.Play("forehand");
            else
                animator.Play("backhand");
        }

        aimTarget.position = aimTargetInitialPosition;
    }
}
