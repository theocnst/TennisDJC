using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public Transform aimTarget;
    float speed = 3f;
    float force = 13f;

    bool hitting;

    public Transform ball;
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.F))
        {
            hitting = true;
        }
        else if (Input.GetKeyUp(KeyCode.F))
        {
            hitting = false;
        }

        if (hitting)
        {
            aimTarget.Translate(new Vector3(0, 0, h) * speed * 2 * Time.deltaTime);
        }

        if ((h != 0 || v != 0) && !hitting)
        {
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime);
        }

        Vector3 ballDir = ball.position - transform.position;

        Debug.DrawRay(transform.position, ballDir);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("TennisBall"))
        {
            Vector3 dir = aimTarget.position - transform.position;
            other.GetComponent<Rigidbody>().velocity = dir.normalized * force + new Vector3(0, 6, 0);

            Vector3 ballDir = ball.position - transform.position;

            if (ballDir.x >= 0)
                animator.Play("forehand");
            else
                animator.Play("backhand");
        }
    }
}
